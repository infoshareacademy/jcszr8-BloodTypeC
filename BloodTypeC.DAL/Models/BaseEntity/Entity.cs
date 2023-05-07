using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BloodTypeC.DAL.Models.BaseEntity
{
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
    }
}
