using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;

namespace Common.Automation.Common.Helpers.DevTools
{
    public interface IDevToolsSessionManager
    {
        DevToolsSession Session { get; }
        void SetDevSession(IWebDriver driver);
    }
}
