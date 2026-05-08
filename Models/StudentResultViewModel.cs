namespace StudentManagementSystem.Models
{
    public class StudentResultViewModel
    {
        public Student Student { get; set; } = null!;
        public List<StudentResult> Results { get; set; } = new();
        public string? SelectedSession { get; set; }
        public List<string> AvailableSessions { get; set; } = new();

        // Computed helpers
        public decimal TotalObtained => Results.Where(r => r.Status == "Present").Sum(r => r.ObtainedMarks);
        public decimal TotalMarks => Results.Where(r => r.Status == "Present").Sum(r => r.TotalMarks);
        public decimal Percentage => TotalMarks > 0 ? Math.Round((TotalObtained / TotalMarks) * 100, 2) : 0;
        public string Grade => Percentage >= 90 ? "A+" : Percentage >= 80 ? "A" : Percentage >= 70 ? "B" : Percentage >= 60 ? "C" : Percentage >= 50 ? "D" : "F";
    }
}
