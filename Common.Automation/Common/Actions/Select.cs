using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Common.Automation.Common.Actions
{
    public class Select : ClickElementBase
    {
        private readonly TextElementBase _textElementBase;

        public Select(IWebDriver driver, NetworkAdapter networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
            _textElementBase = new TextElementBase(driver, networkAdapter, loggerHelper);
        }

        public void SelectByOptionText(By dropdown, string valueText)
        {
            var elem = GetLocatedElement(dropdown);
            if (_textElementBase.GetText(elem) == valueText) return;

            var select = new SelectElement(elem);
            select.SelectByText(valueText);
            WaitUntilAllRequestsFinished();
        }
    }
}
