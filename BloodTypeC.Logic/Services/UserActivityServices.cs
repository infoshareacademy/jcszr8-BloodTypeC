﻿using BloodTypeC.DAL.Models;
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

        public UserActivityServices(IRepository<UserActivity> userActivityRepository, UserManager<User> userManager)
        {
            _userActivityRepository = userActivityRepository;
            _userManager = userManager;
        }
        public async Task AddUserActivityAsync(UserActivity userActivity)
        {
            await _userActivityRepository.Insert(userActivity);
        }
        public async Task<UserActivity> CreateUserActivity(UserActivity userActivityTemplate, string user, Enums.UserActions activity, string objectName)
        {
            var result = userActivityTemplate;

            result.User = await _userManager.FindByNameAsync(user);
            result.UserAction = activity;
            result.ObjectName = objectName;
        
            return result;
        }
    }
}
