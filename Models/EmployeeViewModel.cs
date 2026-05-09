namespace StudentManagementSystem.Models
{
    public class EmployeeViewModel
    {
        public string? SearchString { get; set; }
        public List<Employee> Employees { get; set; } = new();
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int InactiveEmployees { get; set; }
        public List<string> Departments { get; set; } = new();
        public Dictionary<string, int> DepartmentCounts { get; set; } = new();
    }

    public class EmployeeDashboardViewModel
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int NewEmployeesThisMonth { get; set; }
        public int EmployeesOnLeave { get; set; }
        public List<Employee> RecentEmployees { get; set; } = new();
        public Dictionary<string, int> EmployeesByDepartment { get; set; } = new();
        public Dictionary<string, int> EmployeesByStatus { get; set; } = new();
    }
}
