using BoDi;
using Common.Automation.Common;
using Common.Automation.Common.Browser;
using Common.Automation.Common.Browser.Settings;
using Common.Automation.Common.ElementActions;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.ScreenShot;
using TechTalk.SpecFlow;

namespace Common.Automation
{
    [Binding]
    public class SpecFlowDiConfig
    {
        public void RegisterServices(IObjectContainer container)
        {
            RegisterHelpers(container);
            RegisterNetworkComponents(container);
            RegisterBrowserComponents(container);
            RegisterElements(container);
        }

        private void RegisterHelpers(IObjectContainer container)
        {
            container.RegisterTypeAs<LoggerHelper, LoggerHelper>();
            container.RegisterTypeAs<ScreenShotHelper, ScreenShotHelper>();
        }

        private void RegisterBrowserComponents(IObjectContainer container)
        {
            container.RegisterTypeAs<ChromeSettings, ChromeSettings>();
            container.RegisterTypeAs<FirefoxSettings, FirefoxSettings>();
            container.RegisterTypeAs<BrowserSettingsProvider, IBrowserSettingsProvider>();
            container.RegisterTypeAs<BrowserFactory, BrowserFactory>(); ;
        }

        private void RegisterElements(IObjectContainer container)
        {
            container.RegisterTypeAs<Button, Button>();
            container.RegisterTypeAs<Checkbox, Checkbox>();
            container.RegisterTypeAs<DatePicker, DatePicker>();
            container.RegisterTypeAs<Div, Div>();
            container.RegisterTypeAs<Input, Input>();
            container.RegisterTypeAs<Dropdown, Dropdown>();
            container.RegisterTypeAs<TextElement, TextElement>();
            container.RegisterTypeAs<A, A>();
            container.RegisterTypeAs<Radio, Radio>();
            container.RegisterTypeAs<Window, Window>();
            container.RegisterTypeAs<Navigation, Navigation>();
            container.RegisterTypeAs<Tab, Tab>();
        }

        private void RegisterNetworkComponents(IObjectContainer container)
        {
            container.RegisterTypeAs<ChromeNetworkAdapter, ChromeNetworkAdapter>();
            container.RegisterTypeAs<FirefoxNetworkAdapter, FirefoxNetworkAdapter>();
            container.RegisterTypeAs<NetworkAdapterFactory, NetworkAdapterFactory>();

            var networkAdapterFactory = container.Resolve<NetworkAdapterFactory>();
            var networkAdapter = networkAdapterFactory.CreateNetworkAdapter();
            container.RegisterInstanceAs(networkAdapter);
        }
    }
}

