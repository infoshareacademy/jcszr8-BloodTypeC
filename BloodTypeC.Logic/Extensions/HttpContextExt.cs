using Microsoft.AspNetCore.Http;

namespace BloodTypeC.Logic.Extensions
{
    public static class HttpContextExt
    {
        public static string[] GetPath(this HttpRequest request)
        {
            var host = request.Host.ToUriComponent();
            var referer = request.Headers.Referer.ToString() ?? string.Empty;
            var trimmedUrl = referer.Substring(referer.IndexOf(host) + host.Length + 1);
            string[] path = trimmedUrl.Split('/');
            return path;
        }
        public static string GetController(this HttpRequest request)
        {
            var controller = GetPath(request).ElementAt(1);
            return controller;
        }
        public static string GetAction(this HttpRequest request)
        {
            var action = GetPath(request).ElementAt(0);
            return action;
        }
    }
}

