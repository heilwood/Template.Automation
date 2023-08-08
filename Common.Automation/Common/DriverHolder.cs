using System;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common
{
    public class DriverHolder
    {
        private readonly IWebDriver _driver;
        private readonly NetworkAdapter _networkAdapter;
        private readonly LoggerHelper _loggerHelper;
        private readonly IDevToolsSessionManager _devToolsSessionManager;

        public DriverHolder(
            IWebDriver driver,
            NetworkAdapter networkAdapter,
            LoggerHelper loggerHelper,
            IDevToolsSessionManager devToolsSessionManager)
        {
            _driver = driver;
            _networkAdapter = networkAdapter;
            _loggerHelper = loggerHelper;
            _devToolsSessionManager = devToolsSessionManager;
        }

        public T CreateObject<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), _driver, _networkAdapter, _loggerHelper, _devToolsSessionManager);
        }
    }
}