using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class TeacherAssignment
    {
        [Key]
        public int AssignmentID { get; set; }

        [Required]
        [Display(Name = "Teacher ID")]
        public string TeacherID { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Class")]
        public string Class { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Section")]
        public string Section { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Assignment Type")]
        public string AssignmentType { get; set; } = "Subject Teacher"; // Class Teacher, Subject Teacher

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Active"; // Active, Inactive

        [Display(Name = "Academic Year")]
        public string? AcademicYear { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        // Computed Properties
        public string ClassSection => $"{Class} - {Section}";
        public string Initials => GetInitials(TeacherName);
        public string BadgeColor => GetBadgeColor(AssignmentType);

        private string GetInitials(string name)
        {
            if (string.IsNullOrEmpty(name)) return "NA";
            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[1][0]}".ToUpper();
            return parts[0].Length >= 2 ? parts[0].Substring(0, 2).ToUpper() : parts[0].ToUpper();
        }

        private string GetBadgeColor(string assignmentType)
        {
            return assignmentType.ToLower() switch
            {
                "class teacher" => "warning",
                "subject teacher" => "info",
                _ => "secondary"
            };
        }
    }
}
