﻿using System.Text.Json;
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
        
        public List<string> Flavors { get; set; } = new List<string>();
        [JsonPropertyName("abv")]
        public double AlcoholByVolume { get; set; }
        public double Score { get; set; }
        public DateTime Added { get; set; }
        public DateTime LastModified { get; set; }
        public List<int> FavoritesOf { get; set; } = new List<int> { 0 };
    }
}