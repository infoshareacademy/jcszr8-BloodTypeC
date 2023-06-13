using BloodTypeC.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypeC.WebApp.WebExtensions
{
    public static class ControllerExtension
    {
        public static UserActivity CreateUserActivityWithUserConnectionInfo(this Controller controller)
        {
            var userActivity = new UserActivity()
            {
                IPAddress = controller.HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = controller.Request.Headers.UserAgent.ToString(),
            };
            return userActivity;
        }
    }
}
