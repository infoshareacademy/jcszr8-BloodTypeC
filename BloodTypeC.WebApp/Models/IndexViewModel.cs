using BloodTypeC.DAL;

namespace BloodTypeC.WebApp.Models
{
    public class IndexViewModel
    {
        public List<Beer> Beers { get; set; }
        public string searchBrewery { get; set; }
        public string searchBeerName { get; set; }
        public List<string> searchFlavors { get; set; }
        public double? minAbv { get; set; }
        public double? maxAbv { get; set; }

    }
}
