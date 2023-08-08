using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Common.Automation.Common.Browser
{
    public class BrowserFactory
    {
        private readonly ChromeSettings _chromeSettings;
        private readonly FirefoxSettings _firefoxSettings;

        public BrowserFactory()
        {
            _chromeSettings = new ChromeSettings();
            _firefoxSettings = new FirefoxSettings();
        }

        public IWebDriver LocalDriver(BrowserName browser)
        {
            var options = GetLocalBrowserSettings(browser).GetBrowserSettings();
            switch (browser)
            {
                case BrowserName.Firefox:
                    return new FirefoxDriver((FirefoxOptions) options);
                case BrowserName.Chrome:
                    var ass = Assembly.GetExecutingAssembly();
                    var path = Path.GetDirectoryName(ass.Location);
                    return new ChromeDriver(path, (ChromeOptions) options);
            }
            throw new Exception($"Unknown browser name {browser}");
        }

        public IWebDriver RemoteDriver(BrowserName browser, string serverUrl)
        {
            var cap = GetRemoteBrowseSettings(browser);
            var driver = new RemoteWebDriver(new Uri(serverUrl), cap);
            return driver;
        }

        private IBrowserSettings GetLocalBrowserSettings(BrowserName browser)
        {
            switch (browser)
            {
                case BrowserName.Firefox:
                    return _firefoxSettings;
                case BrowserName.Chrome:
                    return _chromeSettings;
            }
            throw new Exception($"Unknown browser name {browser}");
        }

        public ICapabilities GetRemoteBrowseSettings(BrowserName browser)
        {
            var cap = GetLocalBrowserSettings(browser);
            return cap.GetBrowserSettings().ToCapabilities();
        }
    }
}
