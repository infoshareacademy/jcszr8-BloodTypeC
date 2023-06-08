using AutoMapper;
using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Repository;
using BloodTypeC.Logic.Services.IServices;
using BloodTypeC.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloodTypeC.Logic.Services
{
    public class BeerServices : IBeerServices
    {
        private readonly IRepository<Beer> _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public BeerServices(IRepository<Beer> repository, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task AddFromView(BeerViewModel beerFromView, string userName)
        {
            var beerToAdd = _mapper.Map<Beer>(beerFromView);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
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
