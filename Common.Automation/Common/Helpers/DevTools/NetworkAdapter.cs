using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.DevTools.V114.Network;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class NetworkAdapter
    {
        private readonly IDevToolsSessionManager _devToolsSessionManager;
        public HashSet<string> PendingRequestIds { get; private set; }
        public Dictionary<string, string> StuckRequests = new Dictionary<string, string>();
        private readonly LoggerHelper _loggerHelper;

        public bool SkipUrlContains(string url)
        {
            var urlsToSkips = new List<string> { "google", "bing" };
            return urlsToSkips.Any(url.Contains);
        }

        public NetworkAdapter(IDevToolsSessionManager devToolsSessionManager)
        {
            _devToolsSessionManager = devToolsSessionManager;
            PendingRequestIds = new HashSet<string>();
            _loggerHelper = new LoggerHelper();
        }

        private void EnableNetworkAdapter()
        {
            var networkAdapter = _devToolsSessionManager.Session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V114.DevToolsSessionDomains>().Network;
            networkAdapter.Enable(new EnableCommandSettings());
        }

        public void ListenLoadingFinished()
        {
            EnableNetworkAdapter();

            void LoadingFinishedEvent(object sender, LoadingFinishedEventArgs e)
            {
                lock (PendingRequestIds)
                {
                    PendingRequestIds.Remove(e.RequestId);
                    StuckRequests.Remove(e.RequestId);
                }
            }

            _devToolsSessionManager.Session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V114.DevToolsSessionDomains>().Network.LoadingFinished += LoadingFinishedEvent;
        }

        public void ListenLoadingFailed()
        {
            EnableNetworkAdapter();

            void LoadingFailedEvent(object sender, LoadingFailedEventArgs e)
            {
                lock (PendingRequestIds)
                {
                    PendingRequestIds.Remove(e.RequestId);
                    StuckRequests.Remove(e.RequestId);
                }
            }

            _devToolsSessionManager.Session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V114.DevToolsSessionDomains>().Network.LoadingFailed += LoadingFailedEvent;
        }

        public void ListenRequests()
        {
            var networkAdapter = _devToolsSessionManager.Session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V114.DevToolsSessionDomains>().Network;
            networkAdapter.Enable(new EnableCommandSettings());

            void RequestEvent(object sender, RequestWillBeSentEventArgs e)
            {
                if (SkipUrlContains(e.Request.Url)) return;

                lock (PendingRequestIds)
                {
                    if (e.RequestId.Length < 20)
                    {
                        StuckRequests[e.RequestId] = e.Request.Url;
                        PendingRequestIds.Add(e.RequestId);
                    }
                }
            }

            networkAdapter.RequestWillBeSent += RequestEvent;
        }

        public HashSet<string> GetPendingRequests()
        {
            lock (PendingRequestIds)
            {
                return PendingRequestIds;
            }
        }

        public void ResetPendingRequests()
        {
            lock (PendingRequestIds)
            {
                PendingRequestIds = new HashSet<string>();
            }
        }

        public void PrintStuckRequests()
        {
            _loggerHelper.Log().Information("Next requests has been stuck:");
            foreach (var req in StuckRequests)
            {
                _loggerHelper.Log().Information(req.Value);
            }
        }
    }


}
