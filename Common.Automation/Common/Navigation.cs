using System.Threading.Tasks;
using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;


namespace Common.Automation.Common
{

    public class Navigation : ElementBase
    {
        public IDevToolsSessionManager DevToolsSessionManager { get; set; }

        public Navigation(IWebDriver driver, NetworkAdapterHelper networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
        }

        public async Task OpenPageAsync(string url)
        {
            await NavigateToUrl(url);

            //if (ConfigManager.BrowserName == Browser.BrowserName.Chrome)
            //{
            //    var setupDevToolsTask = SetupDevToolsSession();
            //    await Task.WhenAll(navigateTask, setupDevToolsTask);
            //    await ListenToNetworkEvents();
            //}

            WaitForPageToLoad();
            //WaitUntilReqRespListsCleaned();
            WaitUntilAllRequestsFinished();
            LoggerHelper.Log().Information($"Url name: {url}");
        }

        private Task NavigateToUrl(string url)
        {
            return Task.Run(() => Driver.Url = url);
        }

        private Task SetupDevToolsSession()
        {
            return Task.Run(() => DevToolsSessionManager.SetDevSession(Driver));
        }


        //TODO: Maybe move to network adapter
        private async Task ListenToNetworkEvents()
        {
            var listenRequestsTask = Task.Run(() => NetworkAdapter.ListenRequests());
            var listenLoadingFinishedTask = Task.Run(() => NetworkAdapter.ListenLoadingFinished());
            var listenLoadingFailedTask = Task.Run(() => NetworkAdapter.ListenLoadingFailed());

            await Task.WhenAll(listenRequestsTask, listenLoadingFinishedTask, listenLoadingFailedTask);
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
