using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class StudentResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentID { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student? Student { get; set; }

        public string Session { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty;
        public string ExamType { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string SubSubject { get; set; } = string.Empty;

        public DateTime ExamDate { get; set; }
        public DateTime DeclarationDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalMarks { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ObtainedMarks { get; set; }

        public string Status { get; set; } = "Pending"; // Present, Absent, Pending, Ignore
        public bool IsAnnounced { get; set; }
    }
}
