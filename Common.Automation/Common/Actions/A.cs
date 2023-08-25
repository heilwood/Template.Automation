using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.PageLoader;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class A : TextElementBase
    {
        public A(IWebDriver driver, RequestStrategyFactory strategyFactory, LoggerHelper loggerHelper)
            : base(driver, strategyFactory, loggerHelper)
        {
        }

        public string Href(By by)
        {
            return GetAttributeText(by, "href");
        }

    }
}
