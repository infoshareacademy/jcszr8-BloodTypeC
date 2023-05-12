using BloodTypeC.DAL.Models.BaseEntity;
using System.Text.Json.Serialization;

namespace BloodTypeC.DAL.Models
{
    public class Beer : Entity
    {             
        public string? Name { get; set; }

        public string? Brewery { get; set; }

        public string? Style { get; set; }
        public List<string> Flavors { get; set; }

        [JsonPropertyName("abv")]
        public double? AlcoholByVolume { get; set; }
        public double? Score { get; set; }
        public DateTime Added { get; set; }
        public DateTime LastModified { get; set; }

        public List<User> FavoriteUsers { get; set; }
    }
}