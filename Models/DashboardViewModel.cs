using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Models
{
    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal PendingFees { get; set; }

        public int MaleStudentsCount { get; set; }
        public int FemaleStudentsCount { get; set; }

        public int PaidChallansCount { get; set; }
        public int UnpaidChallansCount { get; set; }

        public int StaffChildCount { get; set; }
        public int RegularStudentCount { get; set; }

        public List<Student> RecentStudents { get; set; } = new List<Student>();
        public List<FeeChallan> RecentUnpaidChallans { get; set; } = new List<FeeChallan>();
        public List<FeeChallan> UpcomingDeadlines { get; set; } = new List<FeeChallan>();

        // Chart Data
        public List<string> MonthlyAdmissionsLabels { get; set; } = new List<string>();
        public List<int> MonthlyAdmissionsData { get; set; } = new List<int>();

        public List<string> ClassDistributionLabels { get; set; } = new List<string>();
        public List<int> ClassDistributionData { get; set; } = new List<int>();
    }
}
