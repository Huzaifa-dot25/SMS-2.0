using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class FeeSchedule
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Schedule Name")]
        public string ScheduleName { get; set; } = string.Empty;
        
        [Display(Name = "Class")]
        public string? ClassID { get; set; }
        
        [Display(Name = "Surcharge %")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SurchargePercent { get; set; }
        
        public List<FeeScheduleItem> Items { get; set; } = new List<FeeScheduleItem>();
    }
}
