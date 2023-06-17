using System.ComponentModel;

namespace BloodTypeC.DAL.Models.Views
{
    public class SimpleLogViewModel
    {
        [DisplayName("User E-mail")]
        public string TargetUser { get; set; }
        [DisplayName("Logged in times")]
        public int UserLogIns { get; set; }
        [DisplayName("Logged out times")]
        public int UserLogOuts { get; set; }
        [DisplayName("Last user activity")]
        public string LastUserActivity { get; set; }
        public DateTime LastUserActivityTime { get; set; }
        public string LastUserActivityObject { get; set; }

    }
}
