using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V85.Network;
using System;
using DevToolsSessionDomains = OpenQA.Selenium.DevTools.V85.DevToolsSessionDomains;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class FirefoxNetworkAdapter : NetworkAdapterBase
    {
        private volatile NetworkAdapter _networkAdapter;

        //public FirefoxNetworkAdapter(IDevToolsSessionManager devToolsSessionManager, LoggerHelper loggerHelper) :  base(devToolsSessionManager, loggerHelper)
        //{
        //}

        private void SetNetworkAdapter(DevToolsSession session)
        {
            _networkAdapter = session.GetVersionSpecificDomains<DevToolsSessionDomains>().Network;
            _networkAdapter.Enable(new EnableCommandSettings());
        }

        public override void ListenRequests()
        {

            _networkAdapter.RequestWillBeSent += RequestEvent;
            return;

            void RequestEvent(object sender, RequestWillBeSentEventArgs e)
            {
                AddRequest(e.Request.Url, e.RequestId);
            }
        }

        public override void ListenLoadingFinished()
        {
            _networkAdapter.LoadingFinished += LoadingFinishedEvent;
            return;

            void LoadingFinishedEvent(object sender, LoadingFinishedEventArgs e)
            {
                RemoveRequest(e.RequestId);
            }
        }

        private void ListenResponseReceived()
        {
            _networkAdapter.ResponseReceived += ResponseReceivedEvent;
            return;

            void ResponseReceivedEvent(object sender, ResponseReceivedEventArgs e)
            {
                RemoveRequest(e.RequestId);
            }
        }

        public override void ListenLoadingFailed()
        {
            _networkAdapter.LoadingFailed += LoadingFailedEvent;
            return;

            void LoadingFailedEvent(object sender, LoadingFailedEventArgs e)
            {
                RemoveRequest(e.RequestId);
            }
        }


        public override void Start(IWebDriver driver)
        {
            var session = (driver as IDevTools)?.GetDevToolsSession();
            SetNetworkAdapter(session);
            ListenRequests();
            ListenResponseReceived();
            ListenLoadingFinished();
            ListenLoadingFailed();
        }
    }


}