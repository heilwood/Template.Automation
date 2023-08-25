using Common.Automation.Common.Browser;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.Fiddler;
using OpenQA.Selenium;

namespace Common.Automation.Common.Helpers.PageLoader
{
    public class RequestStrategyFactory
    {
        private readonly IWebDriver _driver;
        private readonly NetworkAdapterHelper _networkAdapterHelper;
        private readonly IFiddlerMonitor _fiddlerMonitor;
        private readonly IDevToolsSessionManager _devToolsSessionManager;

        public RequestStrategyFactory(IWebDriver driver, NetworkAdapterHelper networkAdapterHelper, IDevToolsSessionManager devToolsSessionManager, IFiddlerMonitor fiddlerMonitor)
        {
            _driver = driver;
            _networkAdapterHelper = networkAdapterHelper;
            _fiddlerMonitor = fiddlerMonitor;
            _devToolsSessionManager = devToolsSessionManager;
        }

        public IRequestStrategy CreateStrategy()
        {
            if (ConfigManager.BrowserName == BrowserName.Chrome)
            {
                return new ChromeRequestStrategy(_networkAdapterHelper, _devToolsSessionManager, _driver);
            }
            // Add other browser checks as needed

            return new FiddlerRequestStrategy(_fiddlerMonitor);
        }
    }
}