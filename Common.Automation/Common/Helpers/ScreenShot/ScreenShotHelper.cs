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
    public class ScreenShotHelper : StepBase
    {
        private readonly LoggerHelper _loggerHelper;
        private readonly IWebDriver _driver;

        public ScreenShotHelper(IWebDriver driver)
        {
            _loggerHelper = new LoggerHelper();
            _driver = driver;
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
            var fileNameBase = ScenarioContext.Current.ScenarioInfo.Title.ToIdentifier();
            var argsTable = (ArrayList)ScenarioContext.Current.ScenarioInfo.Arguments.Values.SyncRoot;
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