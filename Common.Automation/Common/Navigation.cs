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
        private IDevToolsSessionManager DevToolsSessionManager { get; set; }
        public Navigation(IWebDriver driver, NetworkAdapterFactory strategyFactory, LoggerHelper loggerHelper)
            : base(driver, strategyFactory, loggerHelper)
        {
            DevToolsSessionManager = AutofacConfig.Resolve<IDevToolsSessionManager>();
        }

        public void OpenPage(string url)
        {

                        
            
            Driver.Url = url;

            //TODO: Move to Start and inject driver
            DevToolsSessionManager.SetDevSession(Driver);
            StrategyFactory.CreateStrategy().Start();
            //var navigateTask = Task.Run(() => Driver.Url = url);
            //var strategyTask = Task.Run(() => StrategyFactory.CreateStrategy().Start());

            //await Task.WhenAll(navigateTask, strategyTask);

            WaitForPageToLoad();
            WaitUntilReqRespListsCleaned();
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
