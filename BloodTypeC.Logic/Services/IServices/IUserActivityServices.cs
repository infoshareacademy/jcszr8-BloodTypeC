﻿using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Enums;
using BloodTypeC.DAL.Models.Views;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IUserActivityServices
    {
        Task LogUserActivityAsync(UserActivity userActivity);
        Task<List<UserActivity>> GetAllUserActivitiesAsync();
        Task<UserActivity> CreateUserActivityAsync(UserActivity userActivityTemplate, string user, Enums.UserActions activity, string? objectName = null);
        Task<UserActivity> GetLastUserActivityAsync(string userName);
        Task<int> CountUserLogInsAsync(string userName);
        Task<int> CountUserLogOutsAsync(string userName);
        Task<AdminReportsOptions> GetAdminReportsOptionsAsync(string adminUsername);
        Task SaveAdminReportsOptionsAsync(AdminReportsOptions options);
        Task SendUserActivityToEmail(string mailBody, string userAddress);
        Task<List<UserActivity>> FilterActivities(ActivityReportViewModel model);
    }
}
