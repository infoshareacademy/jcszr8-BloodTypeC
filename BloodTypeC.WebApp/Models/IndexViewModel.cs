using BloodTypeC.DAL;
using BloodTypeC.Logic;

namespace BloodTypeC.WebApp.Models
{
    public class IndexViewModel
    {
        public List<Beer> Beers { get; set; }
        public List<FlavorToSearch>? CheckedListOfFlavors { get; set; }
        public string searchBrewery { get; set; }
        public string searchBeerName { get; set; }
        public double? minAbv { get; set; }
        public double? maxAbv { get; set; }

        public IndexViewModel() 
        {
            var allFlavors = BeerOperations.GetAllFlavors();
            this.Beers = new List<Beer>();
            
            this.CheckedListOfFlavors= new List<FlavorToSearch>();
            foreach (var flavor in allFlavors) 
            {
                CheckedListOfFlavors.Add(new FlavorToSearch() { Name = flavor, IsChecked = false });
            }
            this.searchBrewery = string.Empty;
            this.searchBeerName = string.Empty;
        }
    }

    public class FlavorToSearch 
    {
        public string Name { get; set; }
        public bool IsChecked { get; set;}

        public FlavorToSearch() 
        {
            this.Name = string.Empty;
            this.IsChecked= false;
        }
    }
}
