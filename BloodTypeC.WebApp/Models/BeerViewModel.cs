using BloodTypeC.DAL;
using System.ComponentModel;

namespace BloodTypeC.WebApp.Models
{
    public class BeerViewModel : Beer
    {
        [DisplayName("Flavors")]
        public string? FlavorString { get; set; }
    }
}
