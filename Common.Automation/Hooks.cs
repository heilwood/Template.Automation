using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Automation.Common.Browser;
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

        protected Hooks(ScenarioContext scenarioContext) {
            _browserFactory = AutofacConfig.Resolve<BrowserFactory>();
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

            _driver?.Quit();
        }
    }
}