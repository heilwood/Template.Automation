using System;
using Common.Automation.Common.Helpers.Fiddler;
using Common.Automation.Common.Helpers.PageLoader;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Common.Automation.Common.Browser.Settings
{
    public class FirefoxSettings : IBrowserSettings
    {
        private readonly FiddlerPort _fiddlerPort;
        public FirefoxSettings(FiddlerPort fiddlerPort)
        {
            _fiddlerPort = fiddlerPort;
        }

        public DriverOptions GetBrowserSettings()
        {
            var options = new FirefoxOptions();
            var profile = new FirefoxProfile();

            var proxyPort = _fiddlerPort.Port;
            var proxy = new Proxy();
            proxy.Kind = ProxyKind.Manual;
            proxy.IsAutoDetect = false;
            //proxy.HttpProxy = $"127.0.0.1:{proxyPort}";
            proxy.SslProxy = $"localhost:{proxyPort}";

            options.Proxy = proxy;


            profile.SetPreference("media.mediasource.enabled", true);
            profile.SetPreference("media.mediasource.mp4.enabled", true);
            options.AcceptInsecureCertificates = true;
            options.Profile = profile;
            return options;
        }
    }
}
