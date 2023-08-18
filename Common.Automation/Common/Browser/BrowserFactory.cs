using System;
using System.IO;
using System.Reflection;
using Common.Automation.Common.Browser.Settings;
using Common.Automation.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;


namespace Common.Automation.Common.Browser
{
    public class BrowserFactory
    {
        private readonly IBrowserSettingsProvider _settingsProvider;
        private readonly string _assemblyDirectory;

        public BrowserFactory(IBrowserSettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
            _assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public IWebDriver LocalDriver(BrowserName browser)
        {
            var options = _settingsProvider.GetBrowserSettings(browser).GetBrowserSettings();
            return browser switch
            {
                BrowserName.Firefox => new FirefoxDriver(_assemblyDirectory, (FirefoxOptions)options),
                BrowserName.Chrome => new ChromeDriver(_assemblyDirectory, (ChromeOptions)options),
                _ => throw new UnsupportedBrowserException($"Unknown browser name {browser}")
            };
        }

        public IWebDriver RemoteDriver(BrowserName browser, string serverUrl)
        {
            var cap = _settingsProvider.GetRemoteBrowserSettings(browser);
            return new RemoteWebDriver(new Uri(serverUrl), cap);
        }
    }
}
