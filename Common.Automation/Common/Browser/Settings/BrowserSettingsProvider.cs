using System;
using Common.Automation.Exceptions;
using OpenQA.Selenium;

namespace Common.Automation.Common.Browser.Settings
{
    public class BrowserSettingsProvider : IBrowserSettingsProvider
    {
        private readonly ChromeSettings _chromeSettings;
        private readonly FirefoxSettings _firefoxSettings;

        public BrowserSettingsProvider(ChromeSettings chromeSettings, FirefoxSettings firefoxSettings)
        {
            _chromeSettings = chromeSettings ?? throw new ArgumentNullException(nameof(chromeSettings));
            _firefoxSettings = firefoxSettings ?? throw new ArgumentNullException(nameof(firefoxSettings));
        }

        public IBrowserSettings GetBrowserSettings(BrowserName browser)
        {
            return browser switch
            {
                BrowserName.Firefox => _firefoxSettings,
                BrowserName.Chrome => _chromeSettings,
                _ => throw new UnsupportedBrowserException($"Unknown browser name {browser}")
            };
        }

        public ICapabilities GetRemoteBrowserSettings(BrowserName browser)
        {
            var cap = GetBrowserSettings(browser);
            return cap.GetBrowserSettings().ToCapabilities();
        }
    }
}
