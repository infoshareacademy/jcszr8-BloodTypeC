using BloodTypeC.DAL;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BloodTypeC.WebApp.Models
{
    public class BeerViewModel
    {
        [Required()]
        [MinLength(2)]
        [MaxLength(20)]
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

