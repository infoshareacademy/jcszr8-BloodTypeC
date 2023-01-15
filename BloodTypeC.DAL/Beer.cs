using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloodTypeC.DAL
{
    public class Beer : IBeer
    {

        [JsonPropertyName("beer_id")]
        public string Id { get; set; }
        [JsonPropertyName("name")] // todo; remove
        public string? Name { get; set; }
        [JsonPropertyName("brewery")]
        public string? Brewery { get; set; }
        [JsonPropertyName("style")]
        public string? Style { get; set; }
        [JsonPropertyName("flavors")]
        public List<string> Flavors { get; set; }
        [JsonPropertyName("abv")]
        public double AlcoholByVolume { get; set; }
        [JsonPropertyName("score")]
        public double Score { get; set; }
        public DateTime? Added { get; set; }
        public DateTime? LastModified { get; set; }

        public void Add(string id, string? name, string? brewery, string? style, string flavor, double alcoholByVolume, double score)
        {
            this.Id = id;
            this.Name = name;
            this.Brewery = brewery;
            this.Style = style;
            this.Flavors.Add(flavor);
            this.AlcoholByVolume = alcoholByVolume;
            this.Score = score;
            DateTime dateTimeNow = new DateTime().Date;
            this.Added = dateTimeNow;
        }
        public static List<Beer> AllBeers { get; set; }
    }
}