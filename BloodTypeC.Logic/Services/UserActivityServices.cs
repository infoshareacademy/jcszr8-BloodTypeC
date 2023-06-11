using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Repository;
using BloodTypeC.Logic.Services.IServices;

namespace BloodTypeC.Logic.Services
{
    public class UserActivityServices : IUserActivityServices
    {
        private readonly IRepository<UserActivity> _userActivityRepository;

        public UserActivityServices(IRepository<UserActivity> userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }
        public async Task AddUserActivityAsync(UserActivity userActivity)
        {
            await _userActivityRepository.Insert(userActivity);
        }
    }
}
