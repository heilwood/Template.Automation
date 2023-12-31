﻿using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.ElementActions.ElementsBase
{
    public class ClickElementBase : ElementBase
    {
        public ClickElementBase(IWebDriver driver, INetworkAdapter networkAdapter, LoggerHelper loggerHelper) : base(driver, networkAdapter, loggerHelper)
        {
        }

        public void JavaScriptClick(By by)
        {
            var element = GetElement(by);
            JavaScriptClick(element);
        }

        public void JavaScriptClick(IWebElement element)
        {
            NetworkAdapter.Start(Driver);
            var executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript("arguments[0].click();", element);
            WaitUntilRequestsLoaded();
        }

        public void Click(By by)
        {
            var elem = GetElement(by);
            Click(elem);
        }

        public void Click(IWebElement elem)
        {
            WaitUntilVisible(elem);
            elem.Click();
        }

        public void ClickAndWait(By by)
        {
            var elem = GetElement(by);
            ClickAndWait(elem);
        }

        public virtual void ClickAndWait(IWebElement elem)
        {
            NetworkAdapter.Start(Driver);
            Click(elem);
            WaitUntilRequestsLoaded();
        }
    }

}
