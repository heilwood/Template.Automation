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

        public async Task OpenPageAsync(string url)
        {
            var loadUrlTask = Task.Run(() => Driver.Url = url);
            var startNetworkAdapterTask = Task.Run(() => NetworkAdapterFactory.CreateNetworkAdapter().Start(Driver));

            await Task.WhenAll(loadUrlTask, startNetworkAdapterTask);

            WaitForPageToLoad();
            SynchronizePendingRequests();
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
