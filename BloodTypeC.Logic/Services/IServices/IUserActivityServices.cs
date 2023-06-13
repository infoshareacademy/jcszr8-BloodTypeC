using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Enums;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IUserActivityServices
    {
        Task LogUserActivityAsync(UserActivity userActivity);
        Task<UserActivity> CreateUserActivity(UserActivity userActivityTemplate, string user, Enums.UserActions activity, string objectName);
    }
}
