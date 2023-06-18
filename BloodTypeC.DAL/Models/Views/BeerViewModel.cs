using BloodTypeC.DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BloodTypeC.WebApp.Models
{
    public class BeerViewModel
    {
        public string? Id { get; set; }
        [Required()]
        [StringLength(Consts.nameMaxLength, MinimumLength = Consts.nameMinLength,
            ErrorMessage =
                $"The name has to be between {Consts.nameMinLengthString} and {Consts.nameMaxLengthString} characters long")]
        public string Name { get; set; }
        public string? Image { get; set; }
        [StringLength(Consts.breweryMaxLength, ErrorMessage = $"The brewery name cannot exceed {Consts.breweryMaxLengthString} characters.")]
        public string? Brewery { get; set; }
        
        public string? Style { get; set; }

        [DisplayName("Alcohol by volume")]
        [Range(0, Consts.maxAbv, ErrorMessage = $"This value has to be between 0 and {Consts.maxAbvString}.")]
        public double? AlcoholByVolume { get; set; }
        [Range(0, Consts.maxScore, ErrorMessage = $"Please enter a value between 0 and {Consts.maxScoreString}.")]
        public double? Score { get; set; }
        public DateTime Added { get; set; }
        public DateTime LastModified { get; set; }
        [DisplayName("Flavors")]
        public string? FlavorString { get; set; }
        public ICollection<User>? FavoriteUsers { get; set; }
        public User? AddedByUser { get; set; }
        public string? AddedByUserString { get; set; }
    }
}
