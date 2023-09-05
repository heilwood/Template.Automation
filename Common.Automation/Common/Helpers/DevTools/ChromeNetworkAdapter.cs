using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.DevTools.V116;
using OpenQA.Selenium.DevTools.V116.Network;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class ChromeNetworkAdapter : NetworkAdapterBase
    {
        private volatile NetworkAdapter _networkAdapter;

        public ChromeNetworkAdapter(IDevToolsSessionManager devToolsSessionManager, LoggerHelper loggerHelper)
            : base(devToolsSessionManager, loggerHelper)
        {
        }


        public NetworkAdapter GetNetworkAdapter()
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
            ListenLoadingFinished();
            ListenLoadingFailed();
        }


    }


}
