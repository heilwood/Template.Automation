using System;
using System.Collections.Generic;
using System.Linq;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Common.Automation.Common.ElementActions.ElementsBase
{
    public class ElementBase
    {
        protected IWebDriver Driver;
        protected readonly LoggerHelper LoggerHelper;
        private DateTime _lastRequestTimestamp = DateTime.MinValue;
        protected readonly INetworkAdapter NetworkAdapter;

        public ElementBase(IWebDriver driver, INetworkAdapter networkAdapter, LoggerHelper loggerHelper)
        {
            NetworkAdapter = networkAdapter ?? throw new ArgumentNullException(nameof(networkAdapter));
            LoggerHelper = loggerHelper ?? throw new ArgumentNullException(nameof(loggerHelper));
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public WebDriverWait Wait(IWebDriver driver, int seconds = 15)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        }

        public Actions Actions(IWebDriver driver)
        {
            return new Actions(driver);
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
            Wait(Driver, seconds).Until(_ => elem.Displayed);
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

        //TODO: Move to CommonLocatorBase
        public By ByChildContainsTxt(string text)
        {
            var by = By.XPath($".//*[contains(text(),'{text}')]");
            return by;
        }

        private bool ResourceLoadingFinished()
        {
            var coolingPeriod = TimeSpan.FromMilliseconds(800);
            var pendingRequests = NetworkAdapter.GetPendingRequests();

            if (!pendingRequests.Any()) return DateTime.Now - _lastRequestTimestamp > coolingPeriod;
            _lastRequestTimestamp = DateTime.Now;
            return false;
        }

        public void WaitUntilAllRequestsFinished()
        {
            try
            {
                Wait(Driver, 30).Until(_ => ResourceLoadingFinished());
            }
            catch
            {
                var stuckRequests = NetworkAdapter.GetStuckRequests();
                throw new Exception($"Requests stuck, you can add _requestUrlsToSkip in NetworkAdapterBase.cs: {stuckRequests}");
            }
        }

        public bool IsPendingRequestsEmpty()
        {
            var isFinishedRequests = ResourceLoadingFinished();
            if (isFinishedRequests) return true;

            NetworkAdapter.ResetPendingRequests();
            return false;
        }

        public void SynchronizePendingRequests()
        {
            try
            {
                Wait(Driver, 30).Until(_ => IsPendingRequestsEmpty());
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Can't synchronize requests, some requests still in PendingRequests object in NetworkAdapterBase.cs");
            }
        }
    }
}
