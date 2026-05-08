using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentManagementSystem.Models
{
    public class Parent
    {
        [Key, ForeignKey("Student")]
        public int StudentID { get; set; }

        [Required]
        [Display(Name = "Father's Name")]
        public string FatherName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Father's Profession")]
        public string FatherProfession { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Father's Mobile No")]
        public string FatherMobile { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Father's NIC")]
        public string FatherNIC { get; set; } = string.Empty;

        [Display(Name = "Father's Email")]
        [EmailAddress]
        public string? FatherEmail { get; set; }

        [Display(Name = "Mother's Name")]
        public string? MotherName { get; set; }

        [Display(Name = "Mother's Profession")]
        public string? MotherProfession { get; set; }

        [Display(Name = "Mother's Mobile")]
        public string? MotherMobile { get; set; }

        [Display(Name = "Mother's NIC")]
        public string? MotherNIC { get; set; }

        [Display(Name = "Mother's Email")]
        [EmailAddress]
        public string? MotherEmail { get; set; }

        [ValidateNever]
        public virtual Student? Student { get; set; }
    }
}
