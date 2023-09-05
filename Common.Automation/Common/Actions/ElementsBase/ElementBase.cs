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
        protected IWebDriver Driver;
        protected readonly LoggerHelper LoggerHelper;

        private DateTime _lastRequestTimestamp = DateTime.MinValue;
        protected readonly NetworkAdapterFactory StrategyFactory;

        public ElementBase(IWebDriver driver, NetworkAdapterFactory strategyFactory, LoggerHelper loggerHelper)
        {
            StrategyFactory = strategyFactory ?? throw new ArgumentNullException(nameof(strategyFactory));
            LoggerHelper = loggerHelper ?? throw new ArgumentNullException(nameof(loggerHelper));
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public WebDriverWait Wait(IWebDriver driver, int seconds = 15)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
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
            WaitUntilPresent(by);
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
            Wait(Driver, seconds).Until(driver => elem.Displayed);
        }

        public void WaitUntilVisible(By by, int seconds = 15)
        {
            try
            {
                var elem = GetElement(by);
                WaitUntilVisible(elem, seconds);
            }
            catch (WebDriverTimeoutException)
            {
                throw new NoSuchElementException($"Element is not visible: {by}");
            }
        }

        public bool IsDisplayed(By by)
        {
            var elements = Driver.FindElements(by);
            return elements.Count > 0 && elements[0].Displayed;
        }

        public void WaitForPageToLoad()
        {
            try
            {
                Wait(Driver, 50)
                    .Until(d => ((IJavaScriptExecutor)Driver)
                        .ExecuteScript("return document.readyState").ToString()!.Equals("complete"));
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("Could not load current page");
            }
        }

        public void WaitUntilPresent(By by)
        {
            try
            {
                Wait(Driver).Until(d => IsPresent(by));
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Element not found with by: {by}");
            }
        }

        public IReadOnlyCollection<IWebElement> GetElements(By by)
        {
            WaitUntilPresent(by);
            var elements = Driver.FindElements(by);
            ScrollIntoView(elements[0]);

            return elements;
        }


        public IWebElement GetChildByText(By parent, string childTxt)
        {
            var parentElement = GetElement(parent);
            var elem = parentElement.FindElement(ByChildContainsTxt(childTxt));
 
            return elem;
        }

        public By ByChildContainsTxt(string text)
        {
            var by = By.XPath($".//*[contains(text(),'{text}')]");
            return by;
        }

        public bool IsRespReqListsCleaned()
        {
            if (StrategyFactory.CreateStrategy().GetPendingRequests().Count == 0)
            {
                if (StrategyFactory.CreateStrategy().GetPendingRequests().Count == 0)
                {
                    Thread.Sleep(250);
                    return true;
                }
            }

            StrategyFactory.CreateStrategy().ResetPendingRequests();
            return false;
        }

        public void WaitUntilReqRespListsCleaned()
        {
            try
            {
                Wait(Driver, 30).Until(d => IsRespReqListsCleaned());
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Can't clean request and response lists from DevTools");
            }
        }

        private bool ResourceLoadingFinished()
        {
            var coolingPeriod = TimeSpan.FromMilliseconds(250);
            var requestStrategy = StrategyFactory.CreateStrategy();
            var pendingRequests = requestStrategy.GetPendingRequests();

            //if (!pendingRequests.Any())
            //{
            //    Thread.Sleep(coolingPeriod);
            //    pendingRequests = StrategyFactory.CreateStrategy().GetPendingRequests();
            //}

            if (!pendingRequests.Any()) return DateTime.Now - _lastRequestTimestamp > coolingPeriod;
            _lastRequestTimestamp = DateTime.Now;
            return false;


        }

        public void WaitUntilAllRequestsFinished()
        {
            try
            {
                Wait(Driver, 30).Until((d) => IsRequestFinished());
            }
            catch
            {
                var stuckRequests = StrategyFactory.CreateStrategy().GetStuckRequests();
                throw new Exception($"Requests stuck, you can add _requestUrlsToSkip in RequestTracker.cs and NetworkAdapterHelper.cs: {stuckRequests}");
            }
        }


        public bool IsRequestFinished()
        {
            var stopwatch = Stopwatch.StartNew();

            var timeout = TimeSpan.FromSeconds(30);
            var pendingReqIds = StrategyFactory.CreateStrategy().GetPendingRequests();
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

        //public void WaitUntilAllRequestsFinished()
        //{
        //    try
        //    {
        //        Wait(Driver).Until(d => IsRequestFinished());
        //        //Console.WriteLine(DevToolsHelper.PendingRequestIds.Count);
        //    }
        //    catch (WebDriverTimeoutException)
        //    {
        //        var stuckResRequests = StrategyFactory.CreateStrategy().GetStuckRequests();
        //        throw new WebDriverTimeoutException($"Some requests has been stuck: {stuckResRequests}");
        //    }
        //}
    }
}
