namespace BloodTypeC.DAL.Models.Views
{
    public class ActivityReportViewModel
    {
        public string? TargetUserName { get; set; }
        public DateTime TargetDate { get; set; } = DateTime.Now;
        public bool CustomDate { get; set; }
        public AdminReportsOptions? ReportsOptions { get; set; }
        public List<UserActivity> UserActivities { get; set; } = new List<UserActivity>();
    }
}
