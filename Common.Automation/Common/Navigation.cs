using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;


namespace Common.Automation.Common
{
    
    public class Navigation : ElementBase
    {
        public Navigation(IWebDriver driver, NetworkAdapterFactory networkAdapterFactory, LoggerHelper loggerHelper)
            : base(driver, networkAdapterFactory, loggerHelper)
        {
        }

        public void OpenPage(string url)
        {
            Driver.Url = url;
            networkAdapterFactory.CreateNetworkAdapter().Start(Driver);

            WaitForPageToLoad();
            WaitUntilAllRequestsFinished();
            LoggerHelper.Log().Information($"Url name: {url}");
        }

        public void Refresh()
        {
            Driver.Navigate().Refresh();
        }

        public void NavigateBack()
        {
            Driver.Navigate().Back();
        }
    }
}
