using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Common.Automation.Common.Actions.ElementsBase
{
    public class ElementBase
    {
        protected static IWebDriver Driver;
        public readonly LoggerHelper LoggerHelper;
        protected readonly NetworkAdapter NetworkAdapter;


        public ElementBase(IWebDriver driver, NetworkAdapter networkAdapter, LoggerHelper loggerHelper)
        {
            NetworkAdapter = networkAdapter ?? throw new ArgumentNullException(nameof(networkAdapter));
            LoggerHelper = loggerHelper ?? throw new ArgumentNullException(nameof(loggerHelper));
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public WebDriverWait Wait(IWebDriver driver, string errorText, int seconds = 15)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds))
            {
                Message = errorText
            };
        }

        public OpenQA.Selenium.Interactions.Actions Actions(IWebDriver driver)
        {
            return new OpenQA.Selenium.Interactions.Actions(driver);
        }

        public void ScrollIntoView(IWebElement elem)
        {
            var js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView({ block: 'center' })", elem);
        }

        public IWebElement GetElement(By by)
        {
            WaitUntilElemPresent(by);
            var elem = Driver.FindElement(by);
            ScrollIntoView(elem);
            return elem;
        }

        public bool IsPresent(By by)
        {
            return Driver.FindElements(by).Count > 0;
        }

        public void WaitUntilVisible(IWebElement elem, int seconds = 15)
        {
            var errorTxt = "Element is not visible";
            Wait(Driver, errorTxt, seconds).Until(driver => elem.Displayed);
        }

        public void WaitUntilVisible(By by, int seconds = 15)
        {
            var elem = GetElement(by);
            WaitUntilVisible(elem, seconds);
        }


        public bool IsDisplayed(By by)
        {
            try
            {
                return Driver.FindElement(by).Displayed;
            }
            catch
            {
                return false;
            }

        }

        public void WaitForPageToLoad()
        {
            var errorTxt = "Page is not loaded";
            Wait(Driver, errorTxt, 50)
                .Until(d => ((IJavaScriptExecutor)Driver)
                    .ExecuteScript("return document.readyState").ToString()!.Equals("complete"));
        }


        public void WaitUntilElemPresent(By by)
        {
            var errorTxt = $"Element not found with by: {by}";
            Wait(Driver, errorTxt).Until(d => IsPresent(by));
        }

        public IReadOnlyCollection<IWebElement> GetLocatedElements(By by)
        {
            WaitUntilElemPresent(by);
            var elements = Driver.FindElements(by);
            if (elements.Count > 0) ScrollIntoView(elements[0]);

            return elements;
        }

        public bool IsRequestFinished()
        {
            var stopwatch = Stopwatch.StartNew();
            var timeout = TimeSpan.FromSeconds(30);
            var pendingReqIds = NetworkAdapter.GetPendingRequests();

            while (true)
            {
                lock (pendingReqIds)
                {
                    if (!pendingReqIds.Any())
                    {
                        Thread.Sleep(500);
                        if (!pendingReqIds.Any())
                        {
                            return true;
                        }
                    }
                }

                if (stopwatch.Elapsed > timeout)
                {
                    return false;
                }

                Thread.Sleep(500);
            }

        }

        public void WaitUntilAllRequestsFinished()
        {
            var errorTxt = "Some requests have been stuck";
            try
            {
                Wait(Driver, errorTxt, 50).Until(d => IsRequestFinished());
            }
            catch (WebDriverTimeoutException ex)
            {
                NetworkAdapter.PrintStuckRequests();
                throw new WebDriverTimeoutException(errorTxt, ex);
            }
        }

        public bool IsRespReqListsCleaned()
        {
            if (NetworkAdapter.GetPendingRequests().Count == 0)
            {
                return true;
            }

            NetworkAdapter.ResetPendingRequests();
            return false;
        }

        public void WaitUntilReqRespListsCleaned()
        {
            var errorTxt = "Can't clean request and response lists from DevTools";
            Wait(Driver, errorTxt).Until(d => IsRespReqListsCleaned());
        }
    }
}
