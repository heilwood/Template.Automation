using System;
using Common.Automation.Common.Browser;
using Common.Automation.Common.Helpers.ScreenShot;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Common.Automation
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver;
        private readonly BrowserName _browser;
        private BrowserFactory _browserFactory;
        private readonly ScenarioContext _scenarioContext;

        protected Hooks(ScenarioContext scenarioContext) {
            Enum.TryParse(ConfigManager.BrowserName, out _browser);
            _browserFactory = AutofacConfig.Resolve<BrowserFactory>();
            _scenarioContext = scenarioContext;
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
#if REMOTE
            _driver = _browserFactory.RemoteDriver(_browser, ConfigManager.SeleniumHubUrl);
#else
            _driver = _browserFactory.LocalDriver(_browser);
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