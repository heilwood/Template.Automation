using Common.Automation.Common.Browser;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class NetworkAdapterFactory
    {
        private readonly ChromeNetworkAdapter _chromeNetworkAdapter;
        private readonly FirefoxNetworkAdapter _firefoxNetworkAdapter;

        public NetworkAdapterFactory(
            ChromeNetworkAdapter chromeNetworkAdapter,
            FirefoxNetworkAdapter firefoxNetworkAdapter)
        {
            _chromeNetworkAdapter = chromeNetworkAdapter;
            _firefoxNetworkAdapter = firefoxNetworkAdapter;
        }


        public INetworkAdapter CreateNetworkAdapter()
        {
            if (ConfigManager.BrowserName == BrowserName.Chrome)
            {
                return _chromeNetworkAdapter;
            }
            // Add other browser checks as needed

            return _firefoxNetworkAdapter;
        }
    }
}