using Microsoft.Extensions.Localization;
using System.Reflection;

namespace BloodTypeC.WebApp.Language
{
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResources);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResources", assemblyName.Name);
        }

        public LocalizedString Getkey(string key)
        { 
            return _localizer[key];
        }
    }
}
