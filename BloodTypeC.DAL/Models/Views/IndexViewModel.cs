using BloodTypeC.DAL.Models;

namespace BloodTypeC.WebApp.Models
{
    public class IndexViewModel
    {
        //[ValidateNever]
        public List<Beer> Beers { get; set; }
        //[ValidateNever]
        public List<FlavorToSearch> CheckedListOfFlavors { get; set; }
        //[ValidateNever]
        public string? searchBrewery { get; set; }
        //[ValidateNever]
        public string? searchBeerName { get; set; }
        //[ValidateNever]
        public double? minAbv { get; set; }
        public double? maxAbv { get; set; }
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
