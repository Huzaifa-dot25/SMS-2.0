using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class FeeChallan
    {
        [Key]
        public string ChallanID { get; set; } = string.Empty;

        [Required]
        public int StudentID { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student? Student { get; set; }

        [Required]
        public string ClassName { get; set; } = string.Empty;

        [Required]
        public string Month { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Arrears { get; set; } = 0;

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public string Status { get; set; } = "Unpaid"; // Paid, Unpaid
    }
}
