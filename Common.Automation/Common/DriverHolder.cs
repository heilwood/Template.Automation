using System;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using OpenQA.Selenium;

namespace Common.Automation.Common
{
    public class DriverHolder
    {
        public T CreateObject<T>()
        {
            var driver = AutofacConfig.Resolve<IWebDriver>();
            var networkAdapter = AutofacConfig.Resolve<NetworkAdapter>();
            var loggerHelper = AutofacConfig.Resolve<LoggerHelper>();
            var devToolsSessionManager = AutofacConfig.Resolve<IDevToolsSessionManager>();

            T obj = (T)Activator.CreateInstance(typeof(T), driver, networkAdapter, loggerHelper, devToolsSessionManager);
            return obj;
        }
    }
}