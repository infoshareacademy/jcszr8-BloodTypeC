using Microsoft.AspNetCore.Identity;

namespace BloodTypeC.DAL.Models
{
    public class User : IdentityUser<string>
    {
        public ICollection<Beer>? FavoriteBeers { get; set; } = new List<Beer>();
        public ICollection<Beer>? AddedBeers { get; set; } = new List<Beer>();
        public ICollection<UserActivity>? UserActivities { get; set; } = new List<UserActivity>();
    }
}
