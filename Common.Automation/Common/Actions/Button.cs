﻿using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.PageLoader;
using OpenQA.Selenium;

namespace Common.Automation.Common.Actions
{
    public class Button : ClickElementBase
    {
        public Button(IWebDriver driver, IFiddlerMonitor fiddlerMonitor, LoggerHelper loggerHelper)
            : base(driver, fiddlerMonitor, loggerHelper)
        {
        }
    }
}
