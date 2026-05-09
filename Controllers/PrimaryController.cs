using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class PrimaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrimaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(ManageResult));
        }

        public async Task<IActionResult> ManageResult(string session, string className, string section, string term, string examType)
        {
            ViewBag.Sessions = await _context.Admissions.Select(a => a.Session).Distinct().ToListAsync();
            ViewBag.Classes  = await _context.Admissions.Select(a => a.Class).Distinct().ToListAsync();
            ViewBag.Sections = await _context.Admissions.Select(a => a.Section).Distinct().ToListAsync();

            var students = new List<Student>();
            if (!string.IsNullOrEmpty(className))
            {
                students = await _context.Students
                    .Include(s => s.Admission)
                    .Where(s => s.Admission != null && s.Admission.Class == className &&
                                (string.IsNullOrEmpty(section) || s.Admission.Section == section))
                    .ToListAsync();
            }

            return View(students);
        }

        public async Task<IActionResult> ResultsGrid(string? session, string? className, string? section, string? term)
        {
            var allResults = await _context.StudentResults
                .Include(r => r.Student).ThenInclude(s => s!.Admission)
                .Include(r => r.Student).ThenInclude(s => s!.Parent)
                .ToListAsync();

            var sessionList  = allResults.Select(r => r.Session).Distinct().OrderByDescending(s => s).ToList();
            var classList    = allResults.Select(r => r.Class).Distinct().OrderBy(c => c).ToList();
            var sectionList  = allResults.Select(r => r.Section).Distinct().OrderBy(s => s).ToList();
            var termList     = allResults.Select(r => r.Term).Distinct().OrderBy(t => t).ToList();

            if (!sessionList.Any())
                sessionList = await _context.Admissions.Where(a => !string.IsNullOrEmpty(a.Session)).Select(a => a.Session!).Distinct().OrderByDescending(s => s).ToListAsync();
            if (!classList.Any())
                classList = await _context.Admissions.Where(a => !string.IsNullOrEmpty(a.Class)).Select(a => a.Class!).Distinct().OrderBy(c => c).ToListAsync();
            if (!sectionList.Any())
                sectionList = await _context.Admissions.Where(a => !string.IsNullOrEmpty(a.Section)).Select(a => a.Section!).Distinct().OrderBy(s => s).ToListAsync();
            if (!termList.Any())
                termList = new List<string> { "Term 1", "Term 2", "Term 3" };

            ViewBag.Sessions = sessionList;
            ViewBag.Classes  = classList;
            ViewBag.Sections = sectionList;
            ViewBag.Terms    = termList;

            var vm = new ResultsGridViewModel
            {
                SelectedSession = session,
                SelectedClass   = className,
                SelectedSection = section,
                SelectedTerm    = term
            };

            bool anyFilter = !string.IsNullOrEmpty(session) || !string.IsNullOrEmpty(className) ||
                             !string.IsNullOrEmpty(section) || !string.IsNullOrEmpty(term);

            if (anyFilter)
            {
                var results = allResults
                    .Where(r =>
                        (string.IsNullOrEmpty(session)   || r.Session == session)   &&
                        (string.IsNullOrEmpty(className) || r.Class   == className) &&
                        (string.IsNullOrEmpty(section)   || r.Section == section)   &&
                        (string.IsNullOrEmpty(term)      || r.Term    == term))
                    .ToList();

                vm.ReportCards = results
                    .GroupBy(r => r.StudentID)
                    .Select(g =>
                    {
                        var first   = g.First();
                        var student = first.Student;
                        return new StudentReportCard
                        {
                            StudentID  = g.Key,
                            Name       = student?.Name ?? "Unknown",
                            RollNo     = student?.Admission?.RollNo,
                            Class      = first.Class,
                            Section    = first.Section,
                            Session    = first.Session,
                            Term       = first.Term,
                            FatherName = student?.Parent?.FatherName,
                            PhotoPath  = student?.PhotoPath,
                            Subjects   = g.OrderBy(r => r.Subject).ToList()
                        };
                    })
                    .OrderBy(c => c.RollNo)
                    .ToList();
            }

            return View(vm);
        }

        public async Task<IActionResult> PrintResult(string? session, string? className, string? section, string? term, string? type)
        {
            ViewBag.Sessions = await _context.StudentResults.Select(r => r.Session).Distinct().OrderByDescending(s => s).ToListAsync();
            ViewBag.Classes  = await _context.Admissions.Where(a => !string.IsNullOrEmpty(a.Class)).Select(a => a.Class!).Distinct().OrderBy(c => c).ToListAsync();
            ViewBag.Sections = await _context.Admissions.Where(a => !string.IsNullOrEmpty(a.Section)).Select(a => a.Section!).Distinct().OrderBy(s => s).ToListAsync();
            ViewBag.Terms    = await _context.StudentResults.Select(r => r.Term).Distinct().OrderBy(t => t).ToListAsync();
            ViewBag.Type     = string.IsNullOrEmpty(type) ? "EndOfYear" : type;

            return View();
        }

        // GET: TeacherAssignment
        public async Task<IActionResult> TeacherAssignment(string? searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var assignments = _context.TeacherAssignments.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                assignments = assignments.Where(t => t.TeacherName.Contains(searchString) 
                                                  || t.TeacherID.Contains(searchString)
                                                  || t.Class.Contains(searchString)
                                                  || t.Section.Contains(searchString)
                                                  || t.Subject.Contains(searchString));
            }

            assignments = assignments.OrderBy(t => t.Class).ThenBy(t => t.Section).ThenBy(t => t.Subject);

            return View(await assignments.ToListAsync());
        }

        // GET: TeacherAssignment/Details/5
        public async Task<IActionResult> TeacherAssignmentDetails(int? id)
        {
            if (id == null) return NotFound();

            var assignment = await _context.TeacherAssignments
                .FirstOrDefaultAsync(m => m.AssignmentID == id);

            if (assignment == null) return NotFound();

            return View(assignment);
        }

        // GET: TeacherAssignment/Create
        public async Task<IActionResult> TeacherAssignmentCreate()
        {
            await PopulateDropdowns();
            return View();
        }

        // POST: TeacherAssignment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeacherAssignmentCreate(TeacherAssignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Teacher assignment created successfully!";
                return RedirectToAction(nameof(TeacherAssignment));
            }
            await PopulateDropdowns();
            return View(assignment);
        }

        // GET: TeacherAssignment/Edit/5
        public async Task<IActionResult> TeacherAssignmentEdit(int? id)
        {
            if (id == null) return NotFound();

            await PopulateDropdowns();
            var assignment = await _context.TeacherAssignments.FindAsync(id);
            if (assignment == null) return NotFound();

            return View(assignment);
        }

        // POST: TeacherAssignment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeacherAssignmentEdit(int id, TeacherAssignment assignment)
        {
            if (id != assignment.AssignmentID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    assignment.ModifiedDate = DateTime.Now;
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Teacher assignment updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherAssignmentExists(assignment.AssignmentID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(TeacherAssignment));
            }
            await PopulateDropdowns();
            return View(assignment);
        }

        // GET: TeacherAssignment/Delete/5
        public async Task<IActionResult> TeacherAssignmentDelete(int? id)
        {
            if (id == null) return NotFound();

            var assignment = await _context.TeacherAssignments
                .FirstOrDefaultAsync(m => m.AssignmentID == id);
            if (assignment == null) return NotFound();

            return View(assignment);
        }

        // POST: TeacherAssignment/Delete/5
        [HttpPost, ActionName("TeacherAssignmentDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeacherAssignmentDeleteConfirmed(int id)
        {
            var assignment = await _context.TeacherAssignments.FindAsync(id);
            if (assignment != null)
            {
                _context.TeacherAssignments.Remove(assignment);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Teacher assignment deleted successfully!";
            return RedirectToAction(nameof(TeacherAssignment));
        }

        private bool TeacherAssignmentExists(int id)
        {
            return _context.TeacherAssignments.Any(e => e.AssignmentID == id);
        }

        private async Task PopulateDropdowns()
        {
            // Classes
            var classes = await _context.Admissions
                .Where(a => !string.IsNullOrEmpty(a.Class))
                .Select(a => a.Class!)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            if (!classes.Any())
                classes = new List<string> { "Class 1", "Class 2", "Class 3", "Class 4", "Class 5" };

            ViewBag.Classes = classes;

            // Sections
            var sections = await _context.Admissions
                .Where(a => !string.IsNullOrEmpty(a.Section))
                .Select(a => a.Section!)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            if (!sections.Any())
                sections = new List<string> { "A", "B", "C", "D" };

            ViewBag.Sections = sections;

            // Subjects (Primary level subjects)
            ViewBag.Subjects = new List<string> 
            { 
                "English", "Mathematics", "General Science", "Social Studies", 
                "Urdu", "Islamic Studies", "Computer Studies", "Art & Craft",
                "Physical Education", "Music", "Library"
            };

            // Assignment Types
            ViewBag.AssignmentTypes = new List<string> { "Class Teacher", "Subject Teacher" };

            // Status
            ViewBag.Statuses = new List<string> { "Active", "Inactive" };

            // Academic Years
            ViewBag.AcademicYears = new List<string> 
            { 
                "2024-2025", "2025-2026", "2026-2027", "2027-2028" 
            };
        }
        // GET: ManageStatements
        public async Task<IActionResult> ManageStatements(string? searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var statements = _context.Statements.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                statements = statements.Where(s => s.StatementText.Contains(searchString) 
                                                  || s.Category.Contains(searchString)
                                                  || s.Subcategory != null && s.Subcategory.Contains(searchString));
            }

            statements = statements.OrderBy(s => s.Category).ThenBy(s => s.StatementType);

            return View(await statements.ToListAsync());
        }

        // GET: ManageStatements/Details/5
        public async Task<IActionResult> StatementDetails(int? id)
        {
            if (id == null) return NotFound();

            var statement = await _context.Statements
                .FirstOrDefaultAsync(m => m.StatementID == id);

            if (statement == null) return NotFound();

            return View(statement);
        }

        // GET: ManageStatements/Create
        public async Task<IActionResult> StatementCreate()
        {
            await PopulateStatementDropdowns();
            return View();
        }

        // POST: ManageStatements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StatementCreate(Statement statement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statement);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Statement created successfully!";
                return RedirectToAction(nameof(ManageStatements));
            }
            await PopulateStatementDropdowns();
            return View(statement);
        }

        // GET: ManageStatements/Edit/5
        public async Task<IActionResult> StatementEdit(int? id)
        {
            if (id == null) return NotFound();

            await PopulateStatementDropdowns();
            var statement = await _context.Statements.FindAsync(id);
            if (statement == null) return NotFound();

            return View(statement);
        }

        // POST: ManageStatements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StatementEdit(int id, Statement statement)
        {
            if (id != statement.StatementID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    statement.ModifiedDate = DateTime.Now;
                    _context.Update(statement);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Statement updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatementExists(statement.StatementID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(ManageStatements));
            }
            await PopulateStatementDropdowns();
            return View(statement);
        }

        // GET: ManageStatements/Delete/5
        public async Task<IActionResult> StatementDelete(int? id)
        {
            if (id == null) return NotFound();

            var statement = await _context.Statements
                .FirstOrDefaultAsync(m => m.StatementID == id);
            if (statement == null) return NotFound();

            return View(statement);
        }

        // POST: ManageStatements/Delete/5
        [HttpPost, ActionName("StatementDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StatementDeleteConfirmed(int id)
        {
            var statement = await _context.Statements.FindAsync(id);
            if (statement != null)
            {
                _context.Statements.Remove(statement);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Statement deleted successfully!";
            return RedirectToAction(nameof(ManageStatements));
        }

        private bool StatementExists(int id)
        {
            return _context.Statements.Any(e => e.StatementID == id);
        }

        private async Task PopulateStatementDropdowns()
        {
            // Categories
            ViewBag.Categories = new List<string> 
            { 
                "Academic Performance", "Behavioral Conduct", "Extracurricular", 
                "Attendance", "Homework", "Participation", "Social Skills"
            };

            // Subcategories
            ViewBag.Subcategories = new List<string> 
            { 
                "Mathematics", "English", "Science", "Social Studies", "Art", "Music", "Physical Education",
                "Leadership", "Teamwork", "Responsibility", "Creativity", "Problem Solving"
            };

            // Statement Types
            ViewBag.StatementTypes = new List<string> { "Positive", "Needs Improvement", "Concern" };

            // Classes
            var classes = await _context.Admissions
                .Where(a => !string.IsNullOrEmpty(a.Class))
                .Select(a => a.Class!)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            if (!classes.Any())
                classes = new List<string> { "Class 1", "Class 2", "Class 3", "Class 4", "Class 5" };

            ViewBag.Classes = classes;

            // Sections
            var sections = await _context.Admissions
                .Where(a => !string.IsNullOrEmpty(a.Section))
                .Select(a => a.Section!)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            if (!sections.Any())
                sections = new List<string> { "A", "B", "C", "D" };

            ViewBag.Sections = sections;

            // Subjects
            ViewBag.Subjects = new List<string> 
            { 
                "English", "Mathematics", "General Science", "Social Studies", 
                "Urdu", "Islamic Studies", "Computer Studies", "Art & Craft"
            };
        }
        public IActionResult ViewStatementsByClass() => View();

        [HttpPost]
        public async Task<IActionResult> SaveResults(List<StudentResult> results)
        {
            if (results != null && results.Any())
            {
                foreach (var result in results)
                {
                    var existing = await _context.StudentResults.FirstOrDefaultAsync(r =>
                        r.StudentID == result.StudentID &&
                        r.Session   == result.Session   &&
                        r.Class     == result.Class     &&
                        r.Section   == result.Section   &&
                        r.Term      == result.Term      &&
                        r.ExamType  == result.ExamType  &&
                        r.Subject   == result.Subject);

                    if (existing != null)
                    {
                        existing.ObtainedMarks   = result.ObtainedMarks;
                        existing.Status          = result.Status;
                        existing.ExamDate        = result.ExamDate;
                        existing.DeclarationDate = result.DeclarationDate;
                        existing.TotalMarks      = result.TotalMarks;
                        existing.IsAnnounced     = result.IsAnnounced;
                    }
                    else
                    {
                        _context.StudentResults.Add(result);
                    }
                }
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Results saved successfully!";
            }
            return RedirectToAction(nameof(ManageResult));
        }
    }
}
