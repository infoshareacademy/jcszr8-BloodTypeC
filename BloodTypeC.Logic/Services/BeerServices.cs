using AutoMapper;
using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Enums;
using BloodTypeC.DAL.Repository;
using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BloodTypeC.DAL.Models.Enums.Enums;

namespace BloodTypeC.Logic.Services
{
    public class BeerServices : IBeerServices
    {
        private readonly IRepository<Beer> _repository;
        private readonly IMapper _mapper;

        public BeerServices(IRepository<Beer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddFromView(BeerViewModel beerFromView, User user)
        {
            var beerToAdd = _mapper.Map<Beer>(beerFromView);
            beerToAdd.AddedByUser = user;
            await _repository.Insert(beerToAdd);
        }

        public async Task<IEnumerable<Beer>> GetAll()
        {
            return await _repository.GetAll(x=>x.FavoriteUsers);
        }

        public async Task<Beer> GetById(string id)
        {
            return await _repository.GetById(id, x=> x.AddedByUser);
        }

        public async Task EditFromView(BeerViewModel beerFromView)
        {
            var beerToEdit = _mapper.Map(beerFromView, await _repository.GetById(beerFromView.Id));
            await _repository.Update(beerToEdit);
        }

        public async Task Delete(string id)
        {
            await _repository.Delete(await GetById(id));
        }
    }

}
