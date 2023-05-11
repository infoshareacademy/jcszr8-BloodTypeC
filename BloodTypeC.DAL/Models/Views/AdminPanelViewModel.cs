using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.DAL.Models.Views
{
    public class AdminPanelViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}
