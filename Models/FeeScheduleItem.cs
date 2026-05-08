using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentManagementSystem.Models
{
    public class FeeScheduleItem
    {
        public int Id { get; set; }
        
        public int FeeScheduleId { get; set; }
        
        [ForeignKey("FeeScheduleId")]
        [ValidateNever]
        public FeeSchedule? FeeSchedule { get; set; }
        
        [Required]
        public string Frequency { get; set; } = "Monthly"; // Monthly, One-Time
        
        [Required]
        [Display(Name = "Fee Type")]
        public string FeeType { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Amount")]
        public decimal FeeAmount { get; set; }
    }
}
