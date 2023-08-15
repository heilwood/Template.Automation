using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace Common.Automation.Common.Helpers.ScreenShot
{
    public class ScreenShotHelper
    {
        private readonly LoggerHelper _loggerHelper;
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public ScreenShotHelper(IWebDriver driver, LoggerHelper loggerHelper, ScenarioContext scenarioContext)
        {
            _loggerHelper = loggerHelper;
            _driver = driver;
            _scenarioContext = scenarioContext;
        }

        public string GetFullImagePath()
        {

            var assembly = Assembly.GetExecutingAssembly();
            var dir = $@"{Path.GetDirectoryName(assembly.Location)}\Screenshots";

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var name = GetScreenShotName();
            var filePath = Path.Combine(dir, name);
            return filePath;
        }


        public virtual string GetScreenShotName()
        {
            var fileNameBase = _scenarioContext.ScenarioInfo.Title.ToIdentifier();
            var argsTable = (ArrayList)_scenarioContext.ScenarioInfo.Arguments.Values.SyncRoot;
            var argsList = argsTable.ToArray().Select(o => (DictionaryEntry)o)
                .Select(d => d.Value).ToList();

            var args = string.Join("_", argsList);
            var name = $"{args}_{ConfigManager.BrowserName}_{fileNameBase}.png".Replace("/", "_");
            return name;
        }

        public void CurrentViewScreenShot()
        {
            try
            {
                if (_driver == null) return;

                var screenShotPath = GetFullImagePath();
                var screenShotTaker = ((ITakesScreenshot)_driver).GetScreenshot();
                screenShotTaker.SaveAsFile(screenShotPath, ScreenshotImageFormat.Png);
                _loggerHelper.Log().Information($"{{rp#file#{screenShotPath}}}");
            }
            catch (Exception ex)
            {
                _loggerHelper.Log().Error(@"Error while taking screenshot: {0}", ex);
            }
        }
    }
}