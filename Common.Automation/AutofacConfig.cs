using Autofac;
using Common.Automation.Common;
using Common.Automation.Common.Actions;
using Common.Automation.Common.Actions.ElementsBase;
using Common.Automation.Common.Helpers;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.ScreenShot;
using OpenQA.Selenium;

namespace Common.Automation
{
    public static class AutofacConfig
    {
        private static IContainer _defaultContainer;
        private static ILifetimeScope _currentScope;

        static AutofacConfig()
        {
            var builder = new ContainerBuilder();

            RegisterDevToolsSession(builder);
            RegisterAdapters(builder);
            RegisterHelpers(builder);
            RegisterElements(builder);
            _defaultContainer = builder.Build();
        }

        public static T Resolve<T>()
        {
            if (_currentScope == null)
                _currentScope = _defaultContainer.BeginLifetimeScope();

            return _currentScope.Resolve<T>();
        }

        public static void RegisterDriver(IWebDriver driver)
        {
            _currentScope?.Dispose();

            _currentScope = _defaultContainer.BeginLifetimeScope(b =>
            {
                b.RegisterInstance(driver).As<IWebDriver>().SingleInstance();
            });
        }

        private static void RegisterDevToolsSession(ContainerBuilder builder)
        {
            var devToolsSessionManager = new DevToolsSessionManager();
            builder.RegisterInstance(devToolsSessionManager)
                   .As<IDevToolsSessionManager>()
                   .SingleInstance();
        }

        private static void RegisterAdapters(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var sessionManager = c.Resolve<IDevToolsSessionManager>();
                return new NetworkAdapter(sessionManager);
            }).SingleInstance();
        }

        private static void RegisterHelpers(ContainerBuilder builder)
        {
            builder.RegisterType<LoggerHelper>().SingleInstance();
            builder.RegisterType<ScreenShotHelper>().InstancePerDependency();
        }

        private static void RegisterElements(ContainerBuilder builder)
        {
            builder.RegisterType<Button>().InstancePerDependency();
            builder.RegisterType<Checkbox>().InstancePerDependency();
            builder.RegisterType<DatePicker>().InstancePerDependency();
            builder.RegisterType<Div>().InstancePerDependency();
            builder.RegisterType<Input>().InstancePerDependency();
            builder.RegisterType<Select>().InstancePerDependency();
            builder.RegisterType<TextElement>().InstancePerDependency();
            builder.RegisterType<Href>().InstancePerDependency();
            builder.RegisterType<Radio>().InstancePerDependency();
            builder.RegisterType<Window>().InstancePerDependency();
            builder.RegisterType<Navigation>().InstancePerDependency().PropertiesAutowired();
            builder.RegisterType<Tab>().InstancePerDependency();
        }

        public static void DisposeCurrentScope()
        {
            _currentScope?.Dispose();
            _currentScope = null;
        }
    }
}
