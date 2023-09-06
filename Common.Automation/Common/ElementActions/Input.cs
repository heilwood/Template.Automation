using Common.Automation.Common.ElementActions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.ElementActions
{
    public class Input : InputElementBase
    {
        public Input(IWebDriver driver, INetworkAdapter networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
        }
    }
}
