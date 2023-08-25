using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.PageLoader;
using OpenQA.Selenium;


namespace Common.Automation.Common
{

    public class Navigation : ElementBase
    {
        public Navigation(IWebDriver driver, RequestStrategyFactory strategyFactory, LoggerHelper loggerHelper)
            : base(driver, strategyFactory, loggerHelper)
        {
        }

        public async Task OpenPageAsync(string url)
        {
            var navigateTask = Task.Run(() => Driver.Url = url);
            var strategyTask = Task.Run(() => StrategyFactory.CreateStrategy().Start());

            await Task.WhenAll(navigateTask, strategyTask);

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
