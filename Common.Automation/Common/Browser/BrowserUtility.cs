using Common.Automation.Common.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Automation.Common.Browser
{
    public static class BrowserUtility
    {
        public static BrowserName ParseBrowserName(string browserName)
        {
            if (Enum.TryParse(browserName, true, out BrowserName browserType))
            {
                return browserType;
            }
            return BrowserName.Unknown;
        }
    }
}







