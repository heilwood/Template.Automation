using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.PageLoader;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Common.Automation.Common.Actions.ElementsBase
{
    public class ElementBase
    {
        protected IWebDriver Driver;
        protected readonly LoggerHelper LoggerHelper;
        protected readonly IFiddlerMonitor FiddlerMonitor;
        private DateTime _lastRequestTimestamp = DateTime.MinValue;

        public ElementBase(IWebDriver driver, IFiddlerMonitor fiddlerMonitor, LoggerHelper loggerHelper)
        {
            FiddlerMonitor = fiddlerMonitor ?? throw new ArgumentNullException(nameof(fiddlerMonitor));
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

        private bool ResourceLoadingFinished()
        {
            var pendingRequests = FiddlerMonitor.GetPendingRequests();

            if (pendingRequests.Any())
            {
                _lastRequestTimestamp = DateTime.Now;
                return false;
            }

            var coolingPeriod = TimeSpan.FromMilliseconds(500);
            return DateTime.Now - _lastRequestTimestamp > coolingPeriod;
        }

        public void WaitUntilAllRequestsFinished()
        {
            try
            {
                Wait(Driver, 30).Until((d) => ResourceLoadingFinished());
            }
            catch
            {
                var pendingRequests = FiddlerMonitor.GetPendingRequests();
                var pendingRequestsMessage = string.Join(", ", pendingRequests);
                throw new Exception($"Requests not loaded, you can add _requestUrlsToSkip in RequestTracker.cs: {pendingRequestsMessage}");
            }
        }
    }
}
