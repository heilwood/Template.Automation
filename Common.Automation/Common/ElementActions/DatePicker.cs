using Common.Automation.Common.ElementActions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.ElementActions
{
    public class DatePicker : ClickElementBase
    {
        public DatePicker(IWebDriver driver, INetworkAdapter networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
        }

        public void SelectCurrentMonthDay(By datePickerIcon, By day)
        {
            var icon = GetElement(datePickerIcon);
            ClickAndWait(icon);
            var dayToSelect = GetElement(day);
            Click(dayToSelect);
        }
    }
}
