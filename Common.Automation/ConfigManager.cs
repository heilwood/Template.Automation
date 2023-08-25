using Common.Automation.Common.Browser;
using System;

namespace Common.Automation
{
    public class ConfigManager
    {
        public static BrowserName BrowserName = BrowserUtility.ParseBrowserName(Environment.GetEnvironmentVariable("browser"));
        public static string MainUrl = "https://www.if.lv";
        public static string SeleniumHubUrl = "http://10.162.113.68:4443/wd/hub";
    }


}
