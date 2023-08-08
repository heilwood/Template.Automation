using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class Href : ClickElementBase
    {
        public Href(IWebDriver driver, NetworkAdapter networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
        }

        public string GetCurrentUrl()
        {
            return Driver.Url;
        }
    }
}
