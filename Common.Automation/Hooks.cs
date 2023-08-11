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
        private readonly BrowserFactory _browserFactory;

        protected Hooks() {
            Enum.TryParse(ConfigManager.BrowserName, out _browser);
            _browserFactory = AutofacConfig.Resolve<BrowserFactory>();
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
#if REMOTE
            _driver = _browserFactory.RemoteDriver(_browser, "http://10.162.113.68:4443/wd/hub");
#else
            _driver = _browserFactory.LocalDriver(_browser);
#endif
            _driver.Manage().Window.Maximize();
            AutofacConfig.RegisterDriver(_driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                var screenShotHelper = AutofacConfig.Resolve<ScreenShotHelper>();
                screenShotHelper.CurrentViewScreenShot();
            }

            _driver?.Quit();
            AutofacConfig.DisposeCurrentScope();
        }
    }
}