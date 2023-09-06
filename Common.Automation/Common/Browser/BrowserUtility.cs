using System;

namespace Common.Automation.Common.Browser
{
    public static class BrowserUtility
    {
        public static BrowserName ParseBrowserName(string browserName)
        {
            return Enum.TryParse(browserName, true, out BrowserName browserType) ? browserType : BrowserName.Chrome;
        }
    }
}







