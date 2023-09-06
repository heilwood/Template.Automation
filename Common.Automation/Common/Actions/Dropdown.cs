using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Common.Automation.Common.Actions
{
    public class Dropdown : ClickElementBase
    {
        private readonly TextElementBase _textElementBase;

        public Dropdown(IWebDriver driver, NetworkAdapterFactory networkAdapterFactory, LoggerHelper loggerHelper)
            : base(driver, networkAdapterFactory, loggerHelper)
        {
            _textElementBase = new TextElementBase(driver, networkAdapterFactory, loggerHelper);
        }

        public void SelectByOptionText(By dropdown, string valueText)
        {
            var elem = GetElement(dropdown);
            if (_textElementBase.GetText(elem) == valueText) return;

            var select = new SelectElement(elem);
            select.SelectByText(valueText);
            WaitUntilAllRequestsFinished();
        }

        public void SelectByText(By button, By valuesList, string valueText)
        {
            if (_textElementBase.GetText(button) == valueText) return;

            Click(button);
            WaitUntilVisible(valuesList);
            var value = GetChildByText(valuesList, valueText);
            ClickAndWait(value);
        }
    }
}
