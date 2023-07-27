using Common.Automation.Common.Actions.ElementsBase;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class Href : ClickElementBase
    {
        public Href(IWebDriver driver) : base(driver)
        {
        }

        public string GetCurrentUrl()
        {
            return Driver.Url;
        }
    }
}
