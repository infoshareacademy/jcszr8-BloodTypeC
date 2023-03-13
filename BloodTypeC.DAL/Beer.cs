using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloodTypeC.DAL
{
    public class Beer
    {
        [JsonPropertyName("beer_id")]
        public string Id { get; set; }

        [Required()]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }
        
        public string Brewery { get; set; }
        
        public string Style { get; set; }
        
        public List<string> Flavors { get; set; } = new List<string>();
        [DisplayName("Flavors")]
        public string FlavorsString { get; set; }
        [JsonPropertyName("abv")]
        [DisplayName("Alcohol by volume")]
        public double AlcoholByVolume { get; set; }
        public double Score { get; set; }
        public DateTime Added { get; set; }
        public DateTime LastModified { get; set; }
    }
}