using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.PageLoader;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class Checkbox : ClickElementBase
    {
        public Checkbox(IWebDriver driver, IFiddlerMonitor fiddlerMonitor, LoggerHelper loggerHelper)
            : base(driver, fiddlerMonitor, loggerHelper)
        {
        }
    }
}
