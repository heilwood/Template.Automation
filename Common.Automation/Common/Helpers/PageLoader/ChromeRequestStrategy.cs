using System.Collections.Generic;
using System.Linq;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common.Helpers.PageLoader
{
    public class ChromeRequestStrategy : IRequestStrategy
    {
        private readonly NetworkAdapterHelper _networkAdapterHelper;
        private readonly IDevToolsSessionManager _devToolsSessionManager;
        private readonly IWebDriver _driver;

        public ChromeRequestStrategy(NetworkAdapterHelper networkAdapterHelper, IDevToolsSessionManager devToolsSessionManager, IWebDriver driver)
        {
            _networkAdapterHelper = networkAdapterHelper;
            _devToolsSessionManager = devToolsSessionManager;
            _driver = driver;
        }

        public List<string> GetPendingRequests()
        {
            return _networkAdapterHelper.GetPendingRequests().ToList();
        }

        public void Start()
        {
            _devToolsSessionManager.SetDevSession(_driver);
            _networkAdapterHelper.Start();
        }
    }
}
