using Common.Automation.Common.Helpers.Fiddler;
using Common.Automation.Common.Helpers.PageLoader;
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

            //var proxyPort = FiddlerMonitor.FiddlerPort;
            //var proxy = new Proxy();
            //proxy.Kind = ProxyKind.Manual;
            //proxy.IsAutoDetect = false;
            ////proxy.HttpProxy = $"127.0.0.1:{proxyPort}";
            //proxy.SslProxy = $"localhost:{proxyPort}";

            //options.Proxy = proxy;

            options.AddArgument("--acceptInsecureCerts");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--acceptSslCert");
            options.AddArgument("--disable-extensions");
            //options.AddArgument($"--proxy-server=http://localhost:{proxyPort}");


            return options;
        }

    }
}
