﻿using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class Radio : ClickElementBase
    {
        public Radio(IWebDriver driver, NetworkAdapterHelper networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
        }
    }
}
