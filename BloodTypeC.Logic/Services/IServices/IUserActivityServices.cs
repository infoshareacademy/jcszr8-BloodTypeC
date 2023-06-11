using BloodTypeC.DAL.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IUserActivityServices
    {
        Task AddUserActivityAsync(UserActivity userActivity);
    }
}
