using Common.Automation.Common.Actions.ElementsBase;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Common.Automation.Common.Actions
{
    public class Select : ClickElementBase
    {
        private readonly TextElementBase _textElementBase;

        public Select(IWebDriver driver) : base(driver)
        {
            _textElementBase = new TextElementBase(driver);
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
