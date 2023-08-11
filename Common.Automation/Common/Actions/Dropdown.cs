﻿using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Common.Automation.Common.Actions
{
    public class Dropdown : ClickElementBase
    {
        private readonly TextElementBase _textElementBase;

        public Dropdown(IWebDriver driver, NetworkAdapter networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
            _textElementBase = new TextElementBase(driver, networkAdapter, loggerHelper);
        }

        public void SelectByOptionText(By dropdown, string valueText)
        {
            var elem = GetElement(dropdown);
            if (_textElementBase.GetText(elem) == valueText) return;

            var select = new SelectElement(elem);
            select.SelectByText(valueText);
            WaitUntilAllRequestsFinished();
        }

        public void SelectByText(By button, By list, string valueText)
        {
            if (_textElementBase.GetText(button) == valueText) return;

            Click(button);
            WaitUntilVisible(list);
            var element = GetChildByText(list, valueText);
            ClickAndWait(element);
        }
    }
}