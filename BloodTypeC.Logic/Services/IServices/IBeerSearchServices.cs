using BloodTypeC.DAL.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IBeerSearchServices
    {
        List<Beer> SearchByName(List<Beer> listToSearch, string name);
        List<Beer> SearchByBrewery(List<Beer> listToSearch, string brewery);
        List<Beer> SearchByStyle(List<Beer> listToSearch, string style);
        List<Beer> SearchByFlavor(List<Beer> listToSearch, string searchflavor);
        List<Beer> SearchByAlcVol(List<Beer> listToSearch, double minAbv, double maxAbv);
        List<string> GetAllFlavors(List<Beer> beers);
    }
}
