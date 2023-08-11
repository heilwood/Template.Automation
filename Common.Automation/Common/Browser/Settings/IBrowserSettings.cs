using OpenQA.Selenium;

namespace Common.Automation.Common.Browser.Settings
{
    public interface IBrowserSettings
    {
        DriverOptions GetBrowserSettings();
    }
}
