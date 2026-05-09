using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeedController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int totalStudentsAdded = 0;
            string[] classes = { "Class 1", "Class 2", "Class 3", "Class 4", "Class 5", "Class 6", "Class 7", "Class 8", "Class 9", "Class 10" };
            string[] sections = { "A", "B" };
            string[] firstNames = { 
                "James", "Mary", "Robert", "Patricia", "John", "Jennifer", "Michael", "Linda", "David", "Elizabeth", 
                "William", "Barbara", "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Sarah", "Charles", "Karen",
                "Christopher", "Nancy", "Daniel", "Lisa", "Matthew", "Betty", "Anthony", "Margaret", "Mark", "Sandra",
                "Donald", "Ashley", "Steven", "Kimberly", "Paul", "Emily", "Andrew", "Donna", "Joshua", "Michelle",
                "Kenneth", "Dorothy", "Kevin", "Carol", "Brian", "Amanda", "George", "Melissa", "Timothy", "Deborah",
                "Ronald", "Stephanie", "Edward", "Rebecca", "Jason", "Sharon", "Jeffrey", "Laura", "Gary", "Cynthia",
                "Jacob", "Kathleen", "Nicholas", "Amy", "Gary", "Angela", "Eric", "Shirley", "Stephen", "Anna",
                "Jonathan", "Brenda", "Larry", "Pamela", "Justin", "Emma", "Scott", "Nicole", "Brandon", "Helen",
                "Benjamin", "Samantha", "Samuel", "Katherine", "Gregory", "Christine", "Alexander", "Debra", "Patrick", "Rachel"
            };
            string[] lastNames = { 
                "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", 
                "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
                "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
                "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
                "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts",
                "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes",
                "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper",
                "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson"
            };
            
            Random rnd = new Random();
            HashSet<string> generatedNames = new HashSet<string>();

            foreach (var className in classes)
            {
                // Ensure we have 10 students for this class
                var currentStudents = await _context.Admissions.Where(a => a.Class == className).CountAsync();
                int studentsToAdd = 10 - currentStudents;

                for (int i = 1; i <= studentsToAdd; i++)
                {
                    string studentName;
                    string firstName = "";
                    string lastName = "";
                    do {
                        firstName = firstNames[rnd.Next(firstNames.Length)];
                        lastName = lastNames[rnd.Next(lastNames.Length)];
                        studentName = $"{firstName} {lastName}";
                    } while (generatedNames.Contains(studentName));
                    
                    generatedNames.Add(studentName);
                    
                    // Generate a specific ID (RollNo) like SMS-2026-C1-001
                    string classShort = className.Replace("Class ", "C");
                    // Check highest existing roll number to ensure uniqueness
                    string rollNo = $"SMS-2026-{classShort}-{(currentStudents + i):D3}";

                    var student = new Student
                    {
                        Name = studentName,
                        RegistrationDate = DateTime.Today.AddDays(-rnd.Next(100)),
                        DateOfBirth = DateTime.Today.AddYears(-rnd.Next(6, 18)),
                        Gender = rnd.Next(2) == 0 ? "Male" : "Female",
                        Status = "Active",
                        ApplicationStatus = "Applied",
                        MobileNo = $"03{rnd.Next(100, 999)}{rnd.Next(1000000, 9999999)}",
                        Address = $"{rnd.Next(1, 999)} Street, {className} Area",
                        Parent = new Parent
                        {
                            FatherName = $"{lastName} Sr.",
                            FatherProfession = "Businessman",
                            FatherMobile = $"03{rnd.Next(100, 999)}{rnd.Next(1000000, 9999999)}",
                            FatherNIC = $"{rnd.Next(10000, 99999)}-{rnd.Next(1000000, 9999999)}-{rnd.Next(1, 9)}",
                            MotherName = $"Mrs. {lastName}"
                        },
                        Admission = new Admission
                        {
                            Session = "2024-2025",
                            Class = className,
                            Section = sections[rnd.Next(sections.Length)],
                            RollNo = rollNo,
                            ActiveInClass = "Yes",
                            DbStatus = "Active"
                        }
                    };

                    _context.Students.Add(student);
                    totalStudentsAdded++;
                }
            }

            if (totalStudentsAdded > 0)
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Successfully added {totalStudentsAdded} students across all classes.";
            }
            else
            {
                TempData["Info"] = "Students already exist for these classes.";
            }

            return RedirectToAction("Index", "Students");
        }

        public async Task<IActionResult> SeedResults()
        {
            int totalResultsAdded = 0;
            string[] subjects = { "English", "Mathematics", "Science", "Social Studies", "Urdu", "Islamic Studies", "Computer" };
            string[] terms = { "Term 1", "Term 2", "Term 3" };
            string session = "2024-2025";

            Random rnd = new Random();

            // Get all students with their admission info
            var students = await _context.Students
                .Include(s => s.Admission)
                .Where(s => s.Admission != null)
                .ToListAsync();

            foreach (var student in students)
            {
                var admission = student.Admission;
                if (admission == null) continue;

                // Check if results already exist for this student
                var existingResults = await _context.StudentResults
                    .Where(r => r.StudentID == student.StudentID && r.Session == session)
                    .CountAsync();

                if (existingResults > 0) continue; // Skip if results already exist

                // Create results for each term and subject
                foreach (var term in terms)
                {
                    foreach (var subject in subjects)
                    {
                        var result = new StudentResult
                        {
                            StudentID = student.StudentID,
                            Session = session,
                            Class = admission.Class,
                            Section = admission.Section,
                            Term = term,
                            ExamType = "Mid Term",
                            Subject = subject,
                            SubSubject = "",
                            ExamDate = DateTime.Today.AddDays(-rnd.Next(1, 30)),
                            DeclarationDate = DateTime.Today.AddDays(-rnd.Next(1, 15)),
                            TotalMarks = 100,
                            ObtainedMarks = (decimal)rnd.Next(40, 100),
                            Status = "Present",
                            IsAnnounced = true
                        };

                        _context.StudentResults.Add(result);
                        totalResultsAdded++;
                    }
                }
            }

            if (totalResultsAdded > 0)
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Successfully added {totalResultsAdded} result records for {students.Count} students.";
            }
            else
            {
                TempData["Info"] = "Results already exist for all students.";
            }

            return RedirectToAction("ResultsGrid", "Secondary");
        }
    }
}
