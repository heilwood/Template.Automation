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
            profile.SetPreference("media.mediasource.enabled", true);
            profile.SetPreference("media.mediasource.mp4.enabled", true);
            options.AcceptInsecureCertificates = true;
            options.Profile = profile;
            return options;
        }
    }
}
