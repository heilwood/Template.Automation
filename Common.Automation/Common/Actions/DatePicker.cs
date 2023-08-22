using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.PageLoader;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class DatePicker : ClickElementBase
    {
        public DatePicker(IWebDriver driver, IFiddlerMonitor fiddlerMonitor, LoggerHelper loggerHelper)
            : base(driver, fiddlerMonitor, loggerHelper)
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
