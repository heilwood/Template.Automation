using OpenQA.Selenium;
using System.Threading.Tasks;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class ChromeNetworkAdapter : NetworkAdapterBase
    {

        void ResponseSentEvent(object sender, NetworkResponseReceivedEventArgs e) => RemoveRequest(e.RequestId);
        void RequestSentEvent(object sender, NetworkRequestSentEventArgs e) => AddRequest(e.RequestUrl, e.RequestId);

        public async Task MonitorNetwork(IWebDriver driver)
        {
            if (IsListening) return;

            var networkInterceptor = driver.Manage().Network;

            networkInterceptor.NetworkResponseReceived += ResponseSentEvent;
            networkInterceptor.NetworkRequestSent += RequestSentEvent;

            await networkInterceptor.StartMonitoring();
            IsListening = true;
        }

        public override void Start(IWebDriver driver)
        {
            MonitorNetwork(driver).Wait();
        }
    }
}
