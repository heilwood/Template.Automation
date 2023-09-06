using Common.Automation.Common.ElementActions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.ElementActions
{
    public class Window : TextElementBase
    {
        public Window(IWebDriver driver, INetworkAdapter networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
        }

        public string GetCurrentUrl()
        {
            return Driver.Url;
        }
    }
}
