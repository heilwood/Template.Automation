using OpenQA.Selenium;
using System.Threading.Tasks;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class ChromeNetworkAdapter : NetworkAdapterBase
    {
        private INetwork _networkInterceptor;
        void ResponseSentEvent(object sender, NetworkResponseReceivedEventArgs e) => RemoveRequest(e.RequestId);
        void RequestSentEvent(object sender, NetworkRequestSentEventArgs e) => AddRequest(e.RequestUrl, e.RequestId);

        public async Task MonitorNetwork(IWebDriver driver)
        {
            if (_networkInterceptor == null)
            {
                var networkInterceptor = driver.Manage().Network;
                _networkInterceptor = networkInterceptor;
                await _networkInterceptor.StartMonitoring();
            }

            _networkInterceptor.NetworkResponseReceived += ResponseSentEvent;
            _networkInterceptor.NetworkRequestSent += RequestSentEvent;
        }

        public override void Start(IWebDriver driver)
        {
            MonitorNetwork(driver).Wait();
        }

        public override void Stop()
        {
            _networkInterceptor.NetworkResponseReceived -= ResponseSentEvent;
            _networkInterceptor.NetworkRequestSent -= RequestSentEvent;
        }
    }
}
