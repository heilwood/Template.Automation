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
            _networkInterceptor?.StopMonitoring();

            var networkInterceptor = driver.Manage().Network;

            networkInterceptor.NetworkResponseReceived += ResponseSentEvent;
            networkInterceptor.NetworkRequestSent += RequestSentEvent;

            await networkInterceptor.StartMonitoring();
            _networkInterceptor = networkInterceptor;
        }

        public override void Start(IWebDriver driver)
        {
            MonitorNetwork(driver).Wait();
        }

        public override void Stop()
        {
            _networkInterceptor.StopMonitoring();
        }
    }
}
