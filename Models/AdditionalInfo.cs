using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentManagementSystem.Models
{
    public class AdditionalInfo
    {
        [Key, ForeignKey("Student")]
        public int StudentID { get; set; }

        // Tax Information
        [Display(Name = "Tax Payer Name")]
        public string? TaxPayerName { get; set; }

        public string? Business { get; set; }

        public string? NIC { get; set; }

        public string? NTN { get; set; }

        [Display(Name = "Tax Payer Address")]
        public string? TaxPayerAddress { get; set; }

        [Display(Name = "Tax Payer City")]
        public string? TaxPayerCity { get; set; }

        [Display(Name = "Active Tax Payer")]
        public string? ActiveTaxPayer { get; set; } // Dropdown: Yes/No

        // Guardian Information
        [Display(Name = "Guardian Name")]
        public string? GuardianName { get; set; }

        [Display(Name = "Guardian Relation")]
        public string? GuardianRelation { get; set; }

        [Display(Name = "Guardian Mobile No")]
        public string? GuardianMobileNo { get; set; }

        [EmailAddress]
        [Display(Name = "Guardian Email")]
        public string? GuardianEmail { get; set; }

        [Display(Name = "Guardian Address")]
        public string? GuardianAddress { get; set; } // Textarea

        // School Leaving Information
        [Display(Name = "School Leaving Certificate No")]
        public string? SchoolLeavingCertificateNo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "School Leaving Date")]
        public DateTime? SchoolLeavingDate { get; set; }

        public string? Performance { get; set; }

        public string? Behaviour { get; set; }

        public string? Certificates { get; set; } // Dropdown

        [Display(Name = "Reason For Leaving")]
        public string? ReasonForLeaving { get; set; } // Textarea

        [ValidateNever]
        public virtual Student? Student { get; set; }
    }
}
