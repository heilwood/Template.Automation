using OpenQA.Selenium;

namespace Common.Automation.Common.Browser.Settings
{
    public interface IBrowserSettingsProvider
    {
        IBrowserSettings GetBrowserSettings(BrowserName browser);
        ICapabilities GetRemoteBrowserSettings(BrowserName browser);
    }

}
