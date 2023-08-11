using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class DatePicker : ClickElementBase
    {
        public DatePicker(IWebDriver driver, NetworkAdapterHelper networkAdapter, LoggerHelper loggerHelper)
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
