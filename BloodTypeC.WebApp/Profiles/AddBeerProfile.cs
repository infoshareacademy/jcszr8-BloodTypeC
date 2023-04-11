using AutoMapper;
using BloodTypeC.DAL.Models;
using BloodTypeC.WebApp.Models;

namespace BloodTypeC.WebApp.Profiles
{
    public class AddBeerProfile : Profile
    {
        public AddBeerProfile()
        {
            CreateMap<Beer, BeerViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Beer, BeerViewModel>()
                .ForMember(dest => dest.FlavorString, opt => opt.MapFrom(src => src.Flavors.Aggregate((a, b) => a + " " + b)));

            CreateMap<BeerViewModel, Beer>()
                .ForMember(dest => dest.Flavors, opt => opt.MapFrom(src => src.FlavorString.Split(" ",StringSplitOptions.RemoveEmptyEntries)));
        }
    }
}
