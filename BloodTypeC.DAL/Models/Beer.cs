using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloodTypeC.DAL.Models
{
    public class Beer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("beer_id")]
        public string? Id { get; set; }
        public string? Name { get; set; }

        public string? Brewery { get; set; }

        public string? Style { get; set; }

        public string? Image { get; set; }

        public List<string> Flavors { get; set; }

        [JsonPropertyName("abv")]
        public double? AlcoholByVolume { get; set; }
        public double? Score { get; set; }
        public DateTime Added { get; set; }
        public DateTime LastModified { get; set; }
    }
}