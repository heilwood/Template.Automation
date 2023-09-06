using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class Input : InputElementBase
    {
        public Input(IWebDriver driver, NetworkAdapterFactory networkAdapterFactory, LoggerHelper loggerHelper)
            : base(driver, networkAdapterFactory, loggerHelper)
        {
        }
    }
}
