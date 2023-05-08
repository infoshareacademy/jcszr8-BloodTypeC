using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BloodTypeC.WebApp.Models
{
    public class BeerViewModel
    {
        public string? Id { get; set; }
        [Required()]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 20 characters long")]
        public string Name { get; set; }

        public string? Brewery { get; set; }

        public string? Style { get; set; }

        [DisplayName("Alcohol by volume")]
        public double? AlcoholByVolume { get; set; }
        public double? Score { get; set; }
        public DateTime Added { get; set; }
        public DateTime LastModified { get; set; }
        [DisplayName("Flavors")]
        public string? FlavorString { get; set; }
    }
}

