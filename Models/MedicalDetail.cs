using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
  
namespace StudentManagementSystem.Models
{
    public class MedicalDetail
    {
        [Key, ForeignKey("Student")]
        public int StudentID { get; set; }

        [Display(Name = "Blood Group")]
        public string? BloodGroup { get; set; }

        [Display(Name = "Chronic Diseases")]
        public string? ChronicDiseases { get; set; }

        public string? Allergies { get; set; }

        public string? Remarks { get; set; }

        [ValidateNever]
        public virtual Student? Student { get; set; }
        
    }
}
