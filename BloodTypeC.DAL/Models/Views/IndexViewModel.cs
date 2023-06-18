using BloodTypeC.DAL.Models;

namespace BloodTypeC.WebApp.Models
{
    public class IndexViewModel
    {
        public List<Beer> Beers { get; set; }
        public List<FlavorToSearch> CheckedListOfFlavors { get; set; }
        public string? SearchBrewery { get; set; }
        public string? SearchBeerName { get; set; }
        public double? MinAbv { get; set; }
        public double? MaxAbv { get; set; }
    }

    public class FlavorToSearch
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public FlavorToSearch()
        {
            this.Name = string.Empty;
            this.IsChecked = false;
        }
    }
}
