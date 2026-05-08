using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Today;

        [Display(Name = "Mobile No")]
        public string? MobileNo { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-10);

        [Display(Name = "Land Line")]
        public string? LandLine { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Display(Name = "International/Local")]
        public string InternationalLocal { get; set; } = "Local";

        public string Status { get; set; } = "Active";

        [Required]
        [Display(Name = "Application Status")]
        public string ApplicationStatus { get; set; } = "Applied";

        public string? Religion { get; set; }

        public string? Child { get; set; } // Select: Child or not?

        [Display(Name = "Transfer From")]
        public string? TransferFrom { get; set; }

        [Display(Name = "Staff Child")]
        public string? StaffChild { get; set; }

        [Display(Name = "Locker Exists")]
        public string? LockerExists { get; set; }

        [Display(Name = "Locker ID")]
        public string? LockerID { get; set; }

        public string? Active { get; set; } // Select status in second col

        public string? Address { get; set; }

        public string? PhotoPath { get; set; }

        // Navigation Properties
        public virtual Parent? Parent { get; set; }
        public virtual AdditionalInfo? AdditionalInfo { get; set; }
        public virtual Admission? Admission { get; set; }
        public virtual Transport? Transport { get; set; }
        public virtual InternationalDetail? InternationalDetail { get; set; }
        public virtual MedicalDetail? MedicalDetail { get; set; }
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

        public string FullName => Name; // For backward compatibility if needed in views
    }
}
