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
            
            options.AddArguments("--ignore-certificate-errors");
            options.AddArguments("--acceptSslCert");
            options.AddArguments("--disable-extensions");
            options.AddArgument("--proxy-server=http://localhost:8888");


            return options;
        }

    }
}
