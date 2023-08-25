using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Automation.Common.Browser;
using Common.Automation.Common.Helpers.Fiddler;
using Common.Automation.Common.Helpers.PageLoader;
using Common.Automation.Common.Helpers.ScreenShot;
using Fiddler;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace Common.Automation
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver;
        private readonly BrowserFactory _browserFactory;
        private readonly ScenarioContext _scenarioContext;
        private readonly IFiddlerMonitor _fiddlerMonitor;

        protected Hooks(ScenarioContext scenarioContext) {
            _browserFactory = AutofacConfig.Resolve<BrowserFactory>();
            _fiddlerMonitor = AutofacConfig.Resolve<IFiddlerMonitor>();
            _scenarioContext = scenarioContext;
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
#if DEBUG
            _driver = _browserFactory.RemoteDriver(ConfigManager.BrowserName, ConfigManager.SeleniumHubUrl);
#else
            _driver = _browserFactory.LocalDriver(ConfigManager.BrowserName);
#endif
           
            _driver.Manage().Window.Maximize();
            AutofacConfig.InitializeTestSession(_driver, _scenarioContext);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext != null)
            {
                var screenShotHelper = AutofacConfig.Resolve<ScreenShotHelper>();
                screenShotHelper.CurrentViewScreenShot();
            }

            _fiddlerMonitor.Stop();
            _driver?.Quit();
        }
    }
}