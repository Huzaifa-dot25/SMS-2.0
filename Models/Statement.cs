using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Statement
    {
        [Key]
        public int StatementID { get; set; }

        [Required]
        [Display(Name = "Statement Text")]
        public string StatementText { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; } = string.Empty;

        [Display(Name = "Subcategory")]
        public string? Subcategory { get; set; }

        [Display(Name = "Statement Type")]
        public string StatementType { get; set; } = "Positive"; // Positive, Needs Improvement, Concern

        [Display(Name = "Target Class")]
        public string? TargetClass { get; set; } // For specific classes or null for all

        [Display(Name = "Target Section")]
        public string? TargetSection { get; set; } // For specific sections or null for all

        [Display(Name = "Subject Specific")]
        public string? SubjectSpecific { get; set; } // For specific subjects

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Usage Count")]
        public int UsageCount { get; set; } = 0;

        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        // Computed Properties
        public string CategoryBadge => GetCategoryBadge(Category);
        public string TypeBadge => GetTypeBadge(StatementType);
        public string TruncatedText => StatementText.Length > 100 ? StatementText.Substring(0, 100) + "..." : StatementText;

        private string GetCategoryBadge(string category)
        {
            return category.ToLower() switch
            {
                "academic performance" => "success",
                "behavioral conduct" => "info",
                "extracurricular" => "warning",
                "attendance" => "primary",
                "homework" => "secondary",
                "participation" => "dark",
                _ => "light"
            };
        }

        private string GetTypeBadge(string type)
        {
            return type.ToLower() switch
            {
                "positive" => "success",
                "needs improvement" => "warning",
                "concern" => "danger",
                _ => "secondary"
            };
        }
    }
}
