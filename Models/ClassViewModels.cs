namespace StudentManagementSystem.Models
{
    public class ClassSummaryViewModel
    {
        public string ClassName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
    }
}
