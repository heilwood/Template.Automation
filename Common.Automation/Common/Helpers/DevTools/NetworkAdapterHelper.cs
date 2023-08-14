using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.DevTools.V114;
using OpenQA.Selenium.DevTools.V114.Network;

namespace Common.Automation.Common.Helpers.DevTools
{
    public class NetworkAdapterHelper
    {
        private readonly Dictionary<string, string> _stuckRequests = new Dictionary<string, string>();
        private readonly List<string> _urLsToSkip = new List<string> { "google", "bing", "giosg", "data.microsoft" };
        private HashSet<string> _pendingRequestIds = new HashSet<string>();
        private readonly IDevToolsSessionManager _devToolsSessionManager;

        private readonly LoggerHelper _loggerHelper;
        private volatile NetworkAdapter _networkAdapter;
        private const int MaxRequestIdLength = 20;
        private readonly object _lockObject = new object();


        public NetworkAdapterHelper(IDevToolsSessionManager devToolsSessionManager, LoggerHelper loggerHelper)
        {
            _devToolsSessionManager = devToolsSessionManager;
            _loggerHelper = loggerHelper;
        }

        public bool ShouldSkipUrl(string url)
        {
            return _urLsToSkip.Any(url.Contains);
        }

        private NetworkAdapter GetNetworkAdapter()
        {
            if (_networkAdapter != null) return _networkAdapter;
            lock (_lockObject)
            {
                if (_networkAdapter != null) return _networkAdapter;
                _networkAdapter = _devToolsSessionManager.Session.GetVersionSpecificDomains<DevToolsSessionDomains>().Network;
                _networkAdapter.Enable(new EnableCommandSettings());
            }

            return _networkAdapter;
        }

        public void ListenRequests()
        {

            var networkAdapter = GetNetworkAdapter();

            void RequestEvent(object sender, RequestWillBeSentEventArgs e)
            {
                if (ShouldSkipUrl(e.Request.Url)) return;

                lock (_lockObject)
                {
                    if (e.RequestId.Length >= MaxRequestIdLength) return;
                    _stuckRequests[e.RequestId] = e.Request.Url;
                    _pendingRequestIds.Add(e.RequestId);
                }
            }

            networkAdapter.RequestWillBeSent += RequestEvent;
        }

        public void ListenLoadingFinished()
        {
            var networkAdapter = GetNetworkAdapter();

            void LoadingFinishedEvent(object sender, LoadingFinishedEventArgs e)
            {
                lock (_lockObject)
                {
                    _pendingRequestIds.Remove(e.RequestId);
                    _stuckRequests.Remove(e.RequestId);
                }
            }

            networkAdapter.LoadingFinished += LoadingFinishedEvent;
        }

        public void ListenLoadingFailed()
        {
            var networkAdapter = GetNetworkAdapter();

            void LoadingFailedEvent(object sender, LoadingFailedEventArgs e)
            {
                lock (_lockObject)
                {
                    _pendingRequestIds.Remove(e.RequestId);
                    _stuckRequests.Remove(e.RequestId);
                }
            }

            networkAdapter.LoadingFailed += LoadingFailedEvent;
        }


        public HashSet<string> GetPendingRequests()
        {
            lock (_lockObject)
            {
                return _pendingRequestIds;
            }
        }

        public void ResetPendingRequests()
        {
            lock (_lockObject)
            {
                _pendingRequestIds = new HashSet<string>();
            }
        }

        public void PrintStuckRequests()
        {
            var stuckUrls = string.Join(", ", _stuckRequests.Values);
            _loggerHelper.Log().Information($"Next requests have been stuck: {stuckUrls}");
        }
    }


}
