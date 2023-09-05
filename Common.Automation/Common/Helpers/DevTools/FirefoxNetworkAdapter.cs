using OpenQA.Selenium.DevTools.V85;
using OpenQA.Selenium.DevTools.V85.Network;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class FirefoxNetworkAdapter : NetworkAdapterBase
    {
        private volatile NetworkAdapter _networkAdapter;

        public FirefoxNetworkAdapter(IDevToolsSessionManager devToolsSessionManager, LoggerHelper loggerHelper) :  base(devToolsSessionManager, loggerHelper)
        {
        }

        private NetworkAdapter GetNetworkAdapter()
        {

            if (_networkAdapter != null) return _networkAdapter;
            lock (LockObject)
            {

                if (_networkAdapter != null) return _networkAdapter;
                _networkAdapter = DevToolsSessionManager.Session.GetVersionSpecificDomains<DevToolsSessionDomains>().Network;
                _networkAdapter.Enable(new EnableCommandSettings());
            }

            return _networkAdapter;
        }

        public override void ListenRequests()
        {
            var networkAdapter = GetNetworkAdapter();

            networkAdapter.RequestWillBeSent += RequestEvent;
            return;

            void RequestEvent(object sender, RequestWillBeSentEventArgs e)
            {
                AddRequest(e.Request.Url, e.RequestId);
            }
        }

        public override void ListenLoadingFinished()
        {
            var networkAdapter = GetNetworkAdapter();

            networkAdapter.LoadingFinished += LoadingFinishedEvent;
            return;

            void LoadingFinishedEvent(object sender, LoadingFinishedEventArgs e)
            {
                RemoveRequest(e.RequestId);
            }
        }

        public void ListenResponseReceived()
        {
            var networkAdapter = GetNetworkAdapter();

            networkAdapter.ResponseReceived += ResponseReceivedEvent;
            return;

            void ResponseReceivedEvent(object sender, ResponseReceivedEventArgs e)
            {
                RemoveRequest(e.RequestId);
            }
        }

        public override void ListenLoadingFailed()
        {
            var networkAdapter = GetNetworkAdapter();

            networkAdapter.LoadingFailed += LoadingFailedEvent;
            return;

            void LoadingFailedEvent(object sender, LoadingFailedEventArgs e)
            {
                RemoveRequest(e.RequestId);
            }
        }


        public override void Start()
        {
            ListenRequests();
            ListenResponseReceived();
            ListenLoadingFinished();
            ListenLoadingFailed();
        }
    }


}