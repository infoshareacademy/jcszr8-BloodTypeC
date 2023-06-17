namespace BloodTypeC.DAL.Models.Views
{
    public class ActivityReportViewModel
    {
        public string? TargetUserName { get; set; }
        public DateTime TargetDate { get; set; } = DateTime.Now;
        public bool CustomDate { get; set; }
        public ReportsOptions? ReportsOptions { get; set; }
        public List<UserActivity> UserActivities { get; set; } = new List<UserActivity>();
    }

    public class UserActivityReport
    {
        public User User { get; set; }
        public int LogIns { get; set; }
        public int LogOuts { get; set;}
        public UserActivity LastActivity { get; set; }
    }
}
