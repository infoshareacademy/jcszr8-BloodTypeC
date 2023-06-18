namespace BloodTypeC.DAL.Models.Views
{
    public class FavoriteBeersViewModel
    {
        public ICollection<Beer> FavoriteBeers { get; set; } = new List<Beer>();
    }
}
