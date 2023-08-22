using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.PageLoader;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions.ElementsBase
{
    public class InputElementBase : ElementBase
    {
        public InputElementBase(IWebDriver driver, IFiddlerMonitor fiddlerMonitor, LoggerHelper loggerHelper)
            : base(driver, fiddlerMonitor, loggerHelper)
        {
        }

        public void Clear(By by)
        {
            GetElement(by).Clear();
        }

        public void ClearType(By by, string text)
        {
            Clear(by);
            Type(by, text);
        }

        public void Type(By by, string text)
        {
            GetElement(by).SendKeys(text);
            LoggerHelper.Log().Information($"Entered: {text}");
        }

        public void ManualType(By by, string text)
        {
            var elem = GetElement(by);
            Actions(Driver).SendKeys(elem, text).Perform();
        }
    }
}
 