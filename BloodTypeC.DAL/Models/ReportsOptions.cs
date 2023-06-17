using BloodTypeC.DAL.Models.BaseEntity;

namespace BloodTypeC.DAL.Models
{
    public class ReportsOptions : Entity
    {
        public string UserID { get; set; }
        public int SendInterval { get; set; } = 1;
        public string SendTargetEmail { get; set; }
    }
}
