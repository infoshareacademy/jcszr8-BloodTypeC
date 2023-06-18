using System.ComponentModel.DataAnnotations.Schema;

namespace BloodTypeC.DAL.Models.BaseEntity
{
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
    }
}
