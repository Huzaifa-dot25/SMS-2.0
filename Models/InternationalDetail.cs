using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentManagementSystem.Models
{
    public class InternationalDetail
    {
        [Key, ForeignKey("Student")]
        public int StudentID { get; set; }

        public string? Nationality { get; set; }

        [Display(Name = "Passport Number")]
        public string? PassportNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Passport Expiry")]
        public DateTime? PassportExpiryDate { get; set; }

        [Display(Name = "Visa Number")]
        public string? VisaNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Visa Expiry")]
        public DateTime? VisaExpiryDate { get; set; }

        [ValidateNever]
        public virtual Student? Student { get; set; }
    }
}
