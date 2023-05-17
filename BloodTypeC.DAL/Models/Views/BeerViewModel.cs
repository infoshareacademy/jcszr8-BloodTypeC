using BloodTypeC.DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BloodTypeC.WebApp.Models
{
    public class BeerViewModel
    {
        public string? Id { get; set; }
        [Required()]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "The name has to be between 2 and 20 characters long")]
        public string Name { get; set; }
        public string? Image { get; set; }
        [StringLength(35, ErrorMessage = "The brewery name cannot exceed 35 characters.")]
        public string? Brewery { get; set; }
        
        public string? Style { get; set; }

        [DisplayName("Alcohol by volume")]
        [Range(0, 95, ErrorMessage = "This value has to be between 0 and 95.")]
        public double? AlcoholByVolume { get; set; }
        [Range(0, 10, ErrorMessage = "Please enter a value between 0 and 10.")]
        public double? Score { get; set; }
        public DateTime Added { get; set; }
        public DateTime LastModified { get; set; }
        [DisplayName("Flavors")]
        public string? FlavorString { get; set; }
        public ICollection<User>? FavoriteUsers { get; set; }
    }
}

