using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Common.Automation.Common.Browser.Settings
{
    public class FirefoxSettings : IBrowserSettings
    {
        public DriverOptions GetBrowserSettings()
        {
            var options = new FirefoxOptions();
            return options;
        }
    }
}
