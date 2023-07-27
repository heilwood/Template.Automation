using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V114.Page;
using EnableCommandSettings = OpenQA.Selenium.DevTools.V114.Accessibility.EnableCommandSettings;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class DevToolsSessionManager : IDevToolsSessionManager
    {
        public DevToolsSession Session { get; private set; }

        public void SetDevSession(IWebDriver driver)
        {
            var session = (driver as IDevTools)?.GetDevToolsSession();
            session?.SendCommand(new EnableCommandSettings()).GetAwaiter().GetResult();
            session?.SendCommand(new SetLifecycleEventsEnabledCommandSettings { Enabled = true }).GetAwaiter().GetResult();
            Session = session;
        }
    }
}
