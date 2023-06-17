using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Enums;
using BloodTypeC.DAL.Repository;
using BloodTypeC.Logic.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace BloodTypeC.Logic.Services
{
    public class UserActivityServices : IUserActivityServices
    {
        private readonly IRepository<UserActivity> _userActivityRepository;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<AdminReportsOptions> _adminReportsRepository;

        public UserActivityServices(IRepository<UserActivity> userActivityRepository, UserManager<User> userManager, IRepository<AdminReportsOptions> adminReportsRepository)
        {
            _userActivityRepository = userActivityRepository;
            _userManager = userManager;
            _adminReportsRepository = adminReportsRepository;
        }
        public async Task LogUserActivityAsync(UserActivity userActivity)
        {
            await _userActivityRepository.Insert(userActivity);
        }

        public async Task<List<UserActivity>> GetAllUserActivitiesAsync()
        {
            return await _userActivityRepository.GetAll(x=>x.User);
        }

        public async Task<UserActivity> CreateUserActivityAsync(UserActivity userActivityTemplate, string user, Enums.UserActions activity, string? objectName = null)
        {
            var result = userActivityTemplate;

            result.User = await _userManager.FindByNameAsync(user);
            result.UserAction = activity;
            result.ObjectName = objectName ?? string.Empty;

            return result;
        }

        public async Task<UserActivity> GetLastUserActivityAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = new UserActivity()
            {
                Time = DateTime.MinValue
            };
            var logs = await _userActivityRepository.GetAll(x => x.User);
            if (logs.Any(x=>x.User == user))
            {
                var userLog = logs.Where(x => x.User.UserName == userName).ToList();
                var lastActivityDate = userLog.OrderByDescending(x=>x.Time).First().Time;
                var lastActivityAction = userLog.FirstOrDefault(x => x.Time == lastActivityDate).UserAction;
                var lastActivityObject = userLog.FirstOrDefault(x => x.Time == lastActivityDate).ObjectName;

                result.Time = lastActivityDate;
                result.UserAction = lastActivityAction;
                result.ObjectName = lastActivityObject;
            }
            return result;
        }

        public async Task<int> CountUserLogInsAsync(string userName)
        {
            var userActivity = await _userActivityRepository.GetAll(x => x.User);
            var result = userActivity.Where(x => x.User.UserName == userName).Count(x => x.UserAction == Enums.UserActions.LogIn);
            return result;
        }

        public async Task<int> CountUserLogOutsAsync(string userName)
        {
            var userActivity = await _userActivityRepository.GetAll(x => x.User);
            var result = userActivity.Where(x=>x.User.UserName == userName).Count(x => x.UserAction == Enums.UserActions.LogOut);
            return result;
        }

        public async Task<AdminReportsOptions> GetAdminReportsOptionsAsync(string adminUserName)
        {
            var options = await _adminReportsRepository.GetAll();
            return options.FirstOrDefault(x => x.AdminUserName == adminUserName);
        }

        public async Task SaveAdminReportsOptionsAsync(AdminReportsOptions options)
        {
            await _adminReportsRepository.Update(options);
        }
    }
}
