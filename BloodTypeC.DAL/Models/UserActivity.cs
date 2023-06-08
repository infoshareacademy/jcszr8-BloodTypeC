using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodTypeC.DAL.Models.BaseEntity;

namespace BloodTypeC.DAL.Models
{
    public class UserActivity: Entity
    {
        public User User { get; set; }
        public string Action { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public string IPAddress { get; set; }
        public string ClientConnectionInfo { get; set; }

    }
}
