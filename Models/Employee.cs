using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Employee ID")]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Department")]
        public string Department { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; } = string.Empty;

        [Display(Name = "Employment Type")]
        public string EmploymentType { get; set; } = "Full-time";

        [Display(Name = "Salary")]
        [DataType(DataType.Currency)]
        public decimal? Salary { get; set; }

        [Display(Name = "Work Location")]
        public string? WorkLocation { get; set; }

        [Display(Name = "Manager ID")]
        public int? ManagerID { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "Active";

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "Emergency Contact")]
        public string? EmergencyContact { get; set; }

        [Display(Name = "Emergency Phone")]
        public string? EmergencyPhone { get; set; }

        [Display(Name = "Photo Path")]
        public string? PhotoPath { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        // Navigation Properties
        public virtual Employee? Manager { get; set; }
        public virtual ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
        public virtual ICollection<EmployeeDocument> Documents { get; set; } = new List<EmployeeDocument>();

        // Computed Properties
        public string FullName => $"{FirstName} {LastName}";
        public string Age => DateOfBirth.HasValue ? $"{DateTime.Today.Year - DateOfBirth.Value.Year} years" : "N/A";
        public string YearsOfService => $"{DateTime.Today.Year - JoiningDate.Year} years";
    }

    public class EmployeeDocument
    {
        [Key]
        public int DocumentID { get; set; }
        public int EmployeeID { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.Now;

        // Navigation Property
        public virtual Employee Employee { get; set; } = null!;
    }
}
