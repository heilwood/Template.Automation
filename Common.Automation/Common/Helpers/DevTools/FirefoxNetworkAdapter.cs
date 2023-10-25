using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V85.Network;
using DevToolsSessionDomains = OpenQA.Selenium.DevTools.V85.DevToolsSessionDomains;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class FirefoxNetworkAdapter : NetworkAdapterBase
    {
        private volatile NetworkAdapter _networkAdapter;

        private void SetNetworkAdapter(DevToolsSession session)
        {
            _networkAdapter = session.GetVersionSpecificDomains<DevToolsSessionDomains>().Network;
            _networkAdapter.Enable(new EnableCommandSettings());
        }

        private void RequestEvent(object sender, RequestWillBeSentEventArgs e) => AddRequest(e.Request.Url, e.RequestId);
        private void ResponseReceivedEvent(object sender, ResponseReceivedEventArgs e) => RemoveRequest(e.RequestId);
        private void LoadingFailedEvent(object sender, LoadingFailedEventArgs e) => RemoveRequest(e.RequestId);
        private void LoadingFinishedEvent(object sender, LoadingFinishedEventArgs e) => RemoveRequest(e.RequestId);

        public override void Start(IWebDriver driver)
        {
            if (IsListening) return;

            var session = (driver as IDevTools)?.GetDevToolsSession();
            SetNetworkAdapter(session);

            _networkAdapter.ResponseReceived += ResponseReceivedEvent;
            _networkAdapter.LoadingFailed += LoadingFailedEvent;
            _networkAdapter.LoadingFinished += LoadingFinishedEvent;
            _networkAdapter.RequestWillBeSent += RequestEvent;
            IsListening = true;
        }
    }


}