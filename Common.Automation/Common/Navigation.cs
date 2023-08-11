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
            var navigateTask = Task.Run(() => Driver.Url = url);
            var setDevSessionTask = Task.Run(() => DevToolsSessionManager.SetDevSession(Driver));

            await Task.WhenAll(navigateTask, setDevSessionTask);

            var listenRequestsTask = Task.Run(() => NetworkAdapter.ListenRequests());
            var listenLoadingFinishedTask = Task.Run(() => NetworkAdapter.ListenLoadingFinished());
            var listenLoadingFailedTask = Task.Run(() => NetworkAdapter.ListenLoadingFailed());

            await Task.WhenAll(listenRequestsTask, listenLoadingFinishedTask, listenLoadingFailedTask);

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
