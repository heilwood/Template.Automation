﻿using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class A : TextElementBase
    {
        public A(IWebDriver driver, NetworkAdapterHelper networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
        }

        public string Href(By by)
        {
            return GetAttributeText(by, "href");
        }

    }
}