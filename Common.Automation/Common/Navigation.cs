using System.Threading.Tasks;
using Common.Automation.Common.ElementActions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;


namespace Common.Automation.Common
{
    
    public class Navigation : ElementBase
    {
        public Navigation(IWebDriver driver, INetworkAdapter networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
        }

        public async Task OpenPageAsync(string url)
        {
            var loadUrlTask = Task.Run(() => Driver.Url = url);
            var startNetworkAdapterTask = Task.Run(() => NetworkAdapter.Start(Driver));

            await Task.WhenAll(loadUrlTask, startNetworkAdapterTask);

            WaitForPageToLoad();
            WaitUntilRequestsLoaded();

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
