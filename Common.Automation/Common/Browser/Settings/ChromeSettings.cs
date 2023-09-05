
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Common.Automation.Common.Browser.Settings
{
    public class ChromeSettings : IBrowserSettings
    {
        public DriverOptions GetBrowserSettings()
        {
            var options = new ChromeOptions();
            //options.AddArguments("--headless", "--window-size=1920,1200");
            options.AddArgument("--acceptInsecureCerts");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--acceptSslCert");
            options.AddArgument("--disable-extensions");
            return options;
        }

    }
}
