namespace StudentManagementSystem.Models
{
    public class ResultsGridViewModel
    {
        public string? SelectedSession { get; set; }
        public string? SelectedClass   { get; set; }
        public string? SelectedSection { get; set; }
        public string? SelectedTerm    { get; set; }

        public List<StudentReportCard> ReportCards { get; set; } = new();
    }

    public class StudentReportCard
    {
        public int     StudentID  { get; set; }
        public string  Name       { get; set; } = string.Empty;
        public string? RollNo     { get; set; }
        public string  Class      { get; set; } = string.Empty;
        public string  Section    { get; set; } = string.Empty;
        public string  Session    { get; set; } = string.Empty;
        public string  Term       { get; set; } = string.Empty;
        public string? FatherName { get; set; }
        public string? PhotoPath  { get; set; }

        public List<StudentResult> Subjects { get; set; } = new();

        // Computed
        public decimal TotalObtained => Subjects.Where(s => s.Status == "Present").Sum(s => s.ObtainedMarks);
        public decimal TotalMarks    => Subjects.Where(s => s.Status == "Present").Sum(s => s.TotalMarks);
        public decimal Percentage    => TotalMarks > 0 ? Math.Round(TotalObtained / TotalMarks * 100, 1) : 0;
        public string  Grade         => Percentage >= 90 ? "A+" : Percentage >= 80 ? "A" : Percentage >= 70 ? "B"
                                      : Percentage >= 60 ? "C"  : Percentage >= 50 ? "D" : "F";
        public string  GradeColor    => Grade switch {
            "A+" => "success", "A" => "primary", "B" => "info",
            "C"  => "warning", "D" => "danger",  _   => "secondary"
        };
    }
}
