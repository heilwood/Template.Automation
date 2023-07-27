using System;
using OpenQA.Selenium;

namespace Common.Automation.Common
{
    
    public class DriverHolder
    {
        private static IWebDriver _driver;

        public static T CreateObject<T>()
        {     
            T obj = (T)Activator.CreateInstance(typeof(T), _driver);
            return obj;
        }

        public static void Init(IWebDriver driver)
        {
            if (_driver == null)
            {
                _driver = driver;
                return;
            }
            throw new Exception("Trying to initialize Driver more than once!");
        }

        public static void Utilize()
        {
            _driver = null;
        }
    }
}