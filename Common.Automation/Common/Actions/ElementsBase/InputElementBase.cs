using OpenQA.Selenium;

namespace Common.Automation.Common.Actions.ElementsBase
{
    public class InputElementBase : ElementBase
    {
        public InputElementBase(IWebDriver driver) : base(driver)
        {
        }

        public void Clear(By by)
        {
            GetLocatedElement(by).Clear();
        }

        public void ClearType(By by, string text)
        {
            Clear(by);
            Type(by, text);
        }

        public void Type(By by, string text)
        {
            GetLocatedElement(by).SendKeys(text);
            LoggerHelper.Log().Information($"Entered: {text}");
        }
    }
}
