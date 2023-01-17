using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloodTypeC.DAL
{
    public class Beer
    {
        [JsonPropertyName("beer_id")]
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Brewery { get; set; }
        
        public string Style { get; set; }
        
        public List<string> Flavors { get; set; }
        [JsonPropertyName("abv")]
        public double AlcoholByVolume { get; set; }
        public double Score { get; set; }
        public DateTime Added { get; set; }
        public DateTime LastModified { get; set; }

        public void Add()
        {
            this.Id = (DB.AllBeers.Max(x => int.Parse(x.Id)) + 1).ToString();
            DateTime dateTimeNow = DateTime.Now;
            this.Added = dateTimeNow;

            DB.AllBeers.Add(this);
            Console.WriteLine($"Successfully added {this.Name}!");
        }
    }
}