using Automation.Common;
using BoDi;
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
        private readonly BrowserFactory _browserFactory;
        private readonly ScenarioContext _scenarioContext;
        private readonly IObjectContainer _container;

        protected Hooks(ScenarioContext scenarioContext, IObjectContainer container, SpecFlowDiConfig specFlowDiConfig)
        {
            _scenarioContext = scenarioContext;
            _container = container;
            specFlowDiConfig.RegisterServices(container);
            _browserFactory = _container.Resolve<BrowserFactory>();
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
            _container.RegisterInstanceAs(_driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TestError != null)
            {
                var screenShotHelper = _container.Resolve<ScreenShotHelper>();
                screenShotHelper.CurrentViewScreenShot();
            }

            _driver?.Quit();
        }
    }
}