using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class DevToolsSessionManager : IDevToolsSessionManager
    {
        public DevToolsSession Session { get; private set; }

        public void SetDevSession(IWebDriver driver)
        {
            var session = (driver as IDevTools)?.GetDevToolsSession();
            Session = session;
        }
    }
}
