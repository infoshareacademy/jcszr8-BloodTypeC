using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.DAL.Models
{
    public class User : IdentityUser<string>
    {
        public ICollection<Beer>? FavoriteBeers { get; set; } = new List<Beer>();
    }
}
