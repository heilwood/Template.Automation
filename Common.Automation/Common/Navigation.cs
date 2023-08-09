using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common
{

    public class Navigation : ElementBase
    {
        public IDevToolsSessionManager DevToolsSessionManager { get; set; }

        public Navigation(IWebDriver driver, NetworkAdapter networkAdapter, LoggerHelper loggerHelper)
            : base(driver, networkAdapter, loggerHelper)
        {
        }

        public void OpenPage(string url)
        {
            Driver.Url = url;
            DevToolsSessionManager.SetDevSession(Driver);
            NetworkAdapter.ListenRequests();
            NetworkAdapter.ListenLoadingFinished();
            NetworkAdapter.ListenLoadingFailed();

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
