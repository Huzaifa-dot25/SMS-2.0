using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Document
    {
        public int DocumentID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        [Display(Name = "Document Name")]
        public string DocumentName { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadDate { get; set; } = DateTime.Now;

        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; } = null!;
    }
}
