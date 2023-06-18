using BloodTypeC.DAL.Models.BaseEntity;

namespace BloodTypeC.DAL.Models
{
    public class AdminReportsOptions : Entity
    {
        public string AdminUserName { get; set; }
        public int SendInterval { get; set; }
        public string? SendTargetEmail { get; set; }
    }
}
