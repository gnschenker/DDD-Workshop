using System;
using System.Configuration;

namespace DDD_Sample.Infrastructure.Configuration
{
    public class SettingsBase
    {
        protected static string FromConnectionStrings(string name)
        {
            var setting = ConfigurationManager.ConnectionStrings[name];
            return setting == null ? string.Empty : setting.ConnectionString;
        }

        protected static string FromAppSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName];
        }

        protected static bool TryGetBoolAppSetting(string settingName, bool defaultValue = false)
        {
            try
            {
                var appSetting = FromAppSetting(settingName);
                return appSetting != null ? Boolean.Parse(appSetting) : defaultValue;
            }
            catch { return defaultValue; }
        }

        protected static int TryGetIntAppSetting(string settingName, int defaultValue = 0)
        {
            try
            {
                var appSetting = FromAppSetting(settingName);
                return appSetting != null ? Int32.Parse(appSetting) : defaultValue;
            }
            catch { return defaultValue; }
        } 
    }
}