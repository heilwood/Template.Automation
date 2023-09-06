﻿using Autofac;
using Common.Automation.Common;
using Common.Automation.Common.Actions;
using Common.Automation.Common.Browser.Settings;
using Common.Automation.Common.Browser;
using Common.Automation.Common.Helpers.DevTools;
using Common.Automation.Common.Helpers.ScreenShot;
using Common.Automation.Common.Helpers;
using OpenQA.Selenium;
using System.Threading;
using TechTalk.SpecFlow;
using System;


namespace Common.Automation
{
    public static class AutofacConfig
    {
        private static readonly IContainer DefaultContainer;
        private static readonly ThreadLocal<ILifetimeScope> MainScope = new(() => DefaultContainer.BeginLifetimeScope());

        static AutofacConfig()
        {
            var builder = new ContainerBuilder();

            RegisterHelpers(builder);
            RegisterAdapters(builder);
            RegisterRequestStrategyFactory(builder);
            RegisterBrowserComponents(builder);
            RegisterElements(builder);
            
            DefaultContainer = builder.Build();
        }

        public static T Resolve<T>()
        {
            try
            {
                if (MainScope.Value == null)
                {
                    throw new InvalidOperationException($"Main scope is null while trying to resolve type {typeof(T).FullName}");
                }

                return MainScope.Value.Resolve<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to resolve type {typeof(T).FullName}", ex);
            }
        }

        public static void InitializeTestSession(IWebDriver driver, ScenarioContext scenarioContext)
        {
            MainScope.Value?.Dispose();

            MainScope.Value = DefaultContainer.BeginLifetimeScope(b =>
            {
                b.RegisterInstance(driver).As<IWebDriver>().SingleInstance();
                b.RegisterInstance(scenarioContext).As<ScenarioContext>().SingleInstance();

            });
        }

        private static void RegisterAdapters(ContainerBuilder builder)
        {
            builder.Register(c => new ChromeNetworkAdapter()).SingleInstance();
            builder.Register(c => new FirefoxNetworkAdapter()).SingleInstance();
        }

        private static void RegisterHelpers(ContainerBuilder builder)
        {
            builder.RegisterType<LoggerHelper>().SingleInstance();
            builder.RegisterType<ScreenShotHelper>().InstancePerDependency();
        }

        private static void RegisterBrowserComponents(ContainerBuilder builder)
        {
            builder.RegisterType<ChromeSettings>().SingleInstance();
            builder.RegisterType<FirefoxSettings>().SingleInstance();
            builder.RegisterType<BrowserSettingsProvider>().As<IBrowserSettingsProvider>().SingleInstance();
            builder.RegisterType<BrowserFactory>().SingleInstance();
        }

        private static void RegisterElements(ContainerBuilder builder)
        {
            builder.RegisterType<Button>().InstancePerDependency();
            builder.RegisterType<Checkbox>().InstancePerDependency();
            builder.RegisterType<DatePicker>().InstancePerDependency();
            builder.RegisterType<Div>().InstancePerDependency();
            builder.RegisterType<Input>().InstancePerDependency();
            builder.RegisterType<Dropdown>().InstancePerDependency();
            builder.RegisterType<TextElement>().InstancePerDependency();
            builder.RegisterType<A>().InstancePerDependency();
            builder.RegisterType<Radio>().InstancePerDependency();
            builder.RegisterType<Window>().InstancePerDependency();
            builder.RegisterType<Navigation>().InstancePerDependency().PropertiesAutowired();
            builder.RegisterType<Tab>().InstancePerDependency();
        }


        private static void RegisterRequestStrategyFactory(ContainerBuilder builder)
        {
            builder.RegisterType<NetworkAdapterFactory>().SingleInstance();
        }
    }
}