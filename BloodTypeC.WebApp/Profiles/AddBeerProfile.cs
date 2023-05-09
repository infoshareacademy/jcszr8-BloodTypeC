using AutoMapper;
using BloodTypeC.DAL.Models;
using BloodTypeC.Logic;
using BloodTypeC.Logic.Extensions;
using BloodTypeC.WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BloodTypeC.WebApp.Profiles
{
    public class AddBeerProfile : Profile
    {
        public AddBeerProfile()
        {
            var beer = new BeerViewModel();
            var maxScore = Convert.ToDouble(beer.GetAttributeFrom<RangeAttribute>(nameof(beer.Score)).Maximum);
            var maxAbv = Convert.ToDouble(beer.GetAttributeFrom<RangeAttribute>(nameof(beer.AlcoholByVolume)).Maximum);

            CreateMap<Beer, BeerViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Beer, BeerViewModel>()
                .ForMember(dest => dest.FlavorString, opt => opt.MapFrom(src => src.Flavors.Any() ? 
                src.Flavors.Aggregate((a, b) => a + " " + b) : string.Empty));
            CreateMap<BeerViewModel, Beer>()
                .ForMember(dest => dest.Flavors, opt => opt.MapFrom(src => Formatters.AsTags(src.FlavorString)))
                .ForMember(dest => dest.Style, opt => opt.MapFrom(src => Formatters.AsNameOrTitle(src.Style, Formatters.CapitalsOptions.EachWord, true)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Formatters.AsNameOrTitle(src.Name, Formatters.CapitalsOptions.None, false)))
                .ForMember(dest => dest.Brewery, opt => opt.MapFrom(src => Formatters.AsNameOrTitle(src.Brewery, Formatters.CapitalsOptions.FirstWord, false)))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => Formatters.AsScoreOrABV(src.Score.ToString(), maxScore)))
                .ForMember(dest => dest.AlcoholByVolume, opt => opt.MapFrom(src => Formatters.AsScoreOrABV(src.AlcoholByVolume.ToString(), maxAbv)))
                .ForMember(dest => dest.Added, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
