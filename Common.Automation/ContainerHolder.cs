using Autofac;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.ScreenShot;
using OpenQA.Selenium;

namespace Common.Automation
{
    public static class ContainerHolder
    {
        public static IContainer Container { get; private set; }

        public static void InitializeContainer(IWebDriver driver)
        {
            var builder = new ContainerBuilder();

            Configure(builder, driver);

            Container = builder.Build();
        }

        private static void Configure(ContainerBuilder builder, IWebDriver driver)
        {
            // Register driver instance
            builder.RegisterInstance(driver).As<IWebDriver>().SingleInstance();

            // Create DevToolsSessionManager instance
            var devToolsSessionManager = new DevToolsSessionManager();

            // Register DevToolsSessionManager instance
            builder.RegisterInstance(devToolsSessionManager).As<IDevToolsSessionManager>().SingleInstance();

            // Register NetworkAdapter and PageAdapter, which depend on IDevToolsSessionManager
            builder.Register(c => new NetworkAdapter(devToolsSessionManager)).SingleInstance();

            // Register other types
            builder.RegisterType<ScreenShotHelper>().InstancePerDependency();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static void DisposeContainer()
        {
            Container.Dispose();
        }
    }
}
