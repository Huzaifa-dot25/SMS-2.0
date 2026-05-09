using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EmployeeController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        private async Task PopulateDropdowns()
        {
            // Departments
            var departments = await _context.Employees
                .Where(e => !string.IsNullOrEmpty(e.Department))
                .Select(e => e.Department!)
                .Distinct()
                .OrderBy(d => d)
                .ToListAsync();

            if (!departments.Any())
                departments = new List<string> { "Administration", "Academics", "Finance", "HR", "IT", "Maintenance", "Security", "Transport" };

            ViewBag.Departments = departments;

            // Job Titles
            var jobTitles = await _context.Employees
                .Where(e => !string.IsNullOrEmpty(e.JobTitle))
                .Select(e => e.JobTitle!)
                .Distinct()
                .OrderBy(j => j)
                .ToListAsync();

            if (!jobTitles.Any())
                jobTitles = new List<string> { "Principal", "Vice Principal", "Teacher", "Accountant", "Administrator", "IT Manager", "Security Guard", "Driver" };

            ViewBag.JobTitles = jobTitles;

            // Managers
            var managers = await _context.Employees
                .Where(e => e.Status == "Active")
                .Select(e => new { e.EmployeeID, FullName = e.FirstName + " " + e.LastName })
                .OrderBy(m => m.FullName)
                .ToListAsync();

            ViewBag.Managers = managers;

            // Employment Types
            ViewBag.EmploymentTypes = new List<string> { "Full-time", "Part-time", "Contract", "Temporary" };
        }

        // GET: Employee
        public async Task<IActionResult> Index(string? searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var employees = _context.Employees
                .Include(e => e.Manager)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.FirstName.Contains(searchString) 
                                             || e.LastName.Contains(searchString)
                                             || e.EmployeeCode.Contains(searchString)
                                             || e.Email.Contains(searchString)
                                             || e.Department.Contains(searchString));
            }

            employees = employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName);

            return View(await employees.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees
                .Include(e => e.Manager)
                .Include(e => e.Subordinates)
                .Include(e => e.Documents)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);

            if (employee == null) return NotFound();

            return View(employee);
        }

        // GET: Employee/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            var employee = new Employee();
            return View(employee);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, IFormFile? photoFile, List<IFormFile> documentFiles)
        {
            if (ModelState.IsValid)
            {
                // Handle Photo Upload
                if (photoFile != null)
                {
                    employee.PhotoPath = await SaveFile(photoFile, "employee-photos");
                }

                // Generate unique Employee Code if not provided
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    employee.EmployeeCode = await GenerateEmployeeCode();
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();

                // Handle Multiple Documents Upload
                if (documentFiles != null && documentFiles.Count > 0)
                {
                    foreach (var file in documentFiles)
                    {
                        var path = await SaveFile(file, "employee-documents");
                        _context.EmployeeDocuments.Add(new EmployeeDocument
                        {
                            EmployeeID = employee.EmployeeID,
                            DocumentName = file.FileName,
                            FilePath = path,
                            DocumentType = Path.GetExtension(file.FileName).TrimStart('.')
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns();
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            await PopulateDropdowns();
            var employee = await _context.Employees
                .Include(e => e.Documents)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);

            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee, IFormFile? photoFile, List<IFormFile> documentFiles)
        {
            if (id != employee.EmployeeID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEmployee = await _context.Employees
                        .Include(e => e.Documents)
                        .FirstOrDefaultAsync(e => e.EmployeeID == id);

                    if (existingEmployee == null) return NotFound();

                    // Update Employee properties
                    _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                    existingEmployee.ModifiedDate = DateTime.Now;

                    // Handle Photo Update
                    if (photoFile != null)
                    {
                        existingEmployee.PhotoPath = await SaveFile(photoFile, "employee-photos");
                    }
                    else
                    {
                        _context.Entry(existingEmployee).Property(x => x.PhotoPath).IsModified = false;
                    }

                    await _context.SaveChangesAsync();

                    // Handle Additional Documents
                    if (documentFiles != null && documentFiles.Count > 0)
                    {
                        foreach (var file in documentFiles)
                        {
                            var path = await SaveFile(file, "employee-documents");
                            _context.EmployeeDocuments.Add(new EmployeeDocument
                            {
                                EmployeeID = existingEmployee.EmployeeID,
                                DocumentName = file.FileName,
                                FilePath = path,
                                DocumentType = Path.GetExtension(file.FileName).TrimStart('.')
                            });
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns();
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees
                .Include(e => e.Manager)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }

        private async Task<string> GenerateEmployeeCode()
        {
            var lastEmployee = await _context.Employees
                .OrderByDescending(e => e.EmployeeID)
                .FirstOrDefaultAsync();

            if (lastEmployee != null && int.TryParse(lastEmployee.EmployeeCode, out int lastCode))
            {
                return (lastCode + 1).ToString("D4");
            }

            return "0001";
        }

        private async Task<string> SaveFile(IFormFile file, string subFolder)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(wwwRootPath, "uploads", subFolder, fileName);
            
            // Ensure directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return "/uploads/" + subFolder + "/" + fileName;
        }
    }
}
