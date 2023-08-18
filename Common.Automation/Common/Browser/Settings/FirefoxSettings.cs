using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Common.Automation.Common.Browser.Settings
{
    public class FirefoxSettings : IBrowserSettings
    {
        public DriverOptions GetBrowserSettings()
        {
            var options = new FirefoxOptions();
            var profile = new FirefoxProfile();

            options.EnableDevToolsProtocol = true;

            options.SetPreference("devtools.jsonview.enabled", false);
            options.SetPreference("devtools.netmonitor.enabled", true);
            options.SetPreference("devtools.netmonitor.har.enableAutoExportToFile", true);
            options.SetPreference("devtools.netmonitor.har.forceExport", true);
            options.SetPreference("devtools.netmonitor.har.pageLoadedTimeout", 10000);
            profile.SetPreference("media.mediasource.enabled", true);
            profile.SetPreference("media.mediasource.mp4.enabled", true);
            options.Profile = profile;
            return options;
        }
    }
}
