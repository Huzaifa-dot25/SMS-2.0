using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentManagementSystem.Models
{
    public class Transport
    {
        [Key, ForeignKey("Student")]
        public int StudentID { get; set; }

        [Display(Name = "Bus Number")]
        public string? BusNumber { get; set; }

        [Display(Name = "Bus Zone")]
        public string? Zone { get; set; }

        [Display(Name = "Trip Type")]
        public string? TripType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [ValidateNever]
        public virtual Student? Student { get; set; }
    }
}
