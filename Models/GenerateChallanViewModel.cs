using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class GenerateChallanViewModel
    {
        [Required]
        public string? Session { get; set; }

        [Required]
        public string? Class { get; set; }

        [Required]
        public string? Section { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Month")]
        public DateTime StartMonth { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Month")]
        public DateTime EndMonth { get; set; } = DateTime.Today;

        [Display(Name = "Months Range")]
        public string? MonthsRange { get; set; }

        [Display(Name = "Number of Months")]
        public int NumberOfMonths { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(10);

        [Display(Name = "Expire Previous Unpaid Challans?")]
        public bool ExpirePreviousUnpaid { get; set; }
    }
}
