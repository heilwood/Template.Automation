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
        protected OpenQA.Selenium.Interactions.Actions Action;
        public readonly WebDriverWait Wait;
        public readonly LoggerHelper LoggerHelper;
        protected readonly NetworkAdapter NetworkAdapter;


        public ElementBase(IWebDriver driver)
        {
            NetworkAdapter = ContainerHolder.Resolve<NetworkAdapter>();
            LoggerHelper = new LoggerHelper();
            Driver = driver;
            Action = new OpenQA.Selenium.Interactions.Actions(driver);
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(ConfigManager.WaitTime));
        }


        public void ScrollIntoView(IWebElement elem)
        {
            var js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView({ block: 'center' })", elem);
        }

        public IWebElement GetLocatedElement(By by)
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
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            try
            {
                wait.Until(driver => elem.Displayed);
            }
            catch
            {
                throw new Exception("Element is not visible");
            }
        }

        public void WaitUntilVisible(By by, int seconds = 15)
        {
            var elem = GetLocatedElement(by);
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
            new WebDriverWait(Driver,
                TimeSpan.FromSeconds(ConfigManager.PageLoadWaitTime))
            {
                Message = "page not loaded"
            }
                .Until(
                    d =>
                        ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").ToString()!.Equals("complete"));

        }


        public void WaitUntilElemPresent(By by)
        {
            try
            {
                Wait.Until(d => IsPresent(by));
            }
            catch (Exception)
            {
                throw new Exception($"Element not found with by: {by}");
            }

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
            try
            {
                Wait.Until(d => IsRequestFinished());
            }
            catch (WebDriverTimeoutException)
            {
                NetworkAdapter.PrintStuckRequests();
                throw new WebDriverTimeoutException($"Some requests has been stuck");
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
            try
            {
                Wait.Until(d => IsRespReqListsCleaned());
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Can't clean request and response lists from DevTools");
            }
        }
    }
}
