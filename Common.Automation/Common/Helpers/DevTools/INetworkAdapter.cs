﻿using System.Collections.Generic;
using OpenQA.Selenium;

namespace Common.Automation.Common.Helpers.DevTools
{
    public interface INetworkAdapter
    {
        HashSet<string> GetPendingRequests(); 
        string GetStuckRequests();
        void Start(IWebDriver driver);
    }
}