using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class Div : TextElementBase
    {
        public Div(IWebDriver driver, NetworkAdapterFactory networkAdapterFactory, LoggerHelper loggerHelper)
            : base(driver, networkAdapterFactory, loggerHelper)
        {
        }
    }
}
