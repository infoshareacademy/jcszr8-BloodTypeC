using AutoMapper;
using BloodTypeC.DAL;
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
        }
    }
}
