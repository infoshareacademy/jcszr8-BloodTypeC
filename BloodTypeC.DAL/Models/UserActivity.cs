﻿using BloodTypeC.DAL.Models.BaseEntity;
using static BloodTypeC.DAL.Models.Enums.Enums;

namespace BloodTypeC.DAL.Models
{
    public class UserActivity: Entity
    {
        public User User { get; set; }
        public UserActions UserAction { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
    }
}
