using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentManagementSystem.Models
{
    public class Admission
    {
        [Key, ForeignKey("Student")]
        public int StudentID { get; set; }

        // Admission Info
        [Display(Name = "Test Date")]
        [DataType(DataType.Date)]
        public DateTime? TestDate { get; set; }

        [Display(Name = "Test Cleared")]
        public string? TestCleared { get; set; }

        [Display(Name = "Conducted By")]
        public string? ConductedBy { get; set; }

        public string? Remarks { get; set; }

        // Assign Class
        public string? Session { get; set; }

        [Display(Name = "Requested Class")]
        public string? RequestedClass { get; set; }

        public string? Class { get; set; }

        public string? Section { get; set; }

        [Display(Name = "Active In Class")]
        public string? ActiveInClass { get; set; }

        [Display(Name = "DB Status")]
        public string? DbStatus { get; set; }

        [Display(Name = "Roll No")]
        public string? RollNo { get; set; }

        [ValidateNever]
        public virtual Student? Student { get; set; }
    }
}
