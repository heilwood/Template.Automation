using System;
using System.Linq;
using Common.Automation.Common;
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
        private ScreenShotHelper _screenShotHelper;
        private readonly BrowserName _browser;
        private readonly BrowserFactory _browserFactory;

        protected Hooks() {
            Enum.TryParse(ConfigManager.BrowserName, out _browser);
            _browserFactory = new BrowserFactory();
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
            //_driver = BrowserFactory.RemoteDriver(_browser, "http://swc195240:4445/wd/hub");
            _driver = _browserFactory.LocalDriver(_browser);
            _driver.Manage().Window.Maximize();
            AutofacConfig.RegisterDriver(_driver);
            //DriverHolder.Init(_driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                _screenShotHelper = AutofacConfig.Resolve<ScreenShotHelper>();
                _screenShotHelper.CurrentViewScreenShot();
            }

            _driver?.Quit();
            //DriverHolder.Utilize();
            AutofacConfig.DisposeCurrentScope();
        }
    }
}