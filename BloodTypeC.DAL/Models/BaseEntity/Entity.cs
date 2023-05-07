using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BloodTypeC.DAL.Models.BaseEntity
{
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("beer_id")]
        public string Id { get; set; }
    }
}
