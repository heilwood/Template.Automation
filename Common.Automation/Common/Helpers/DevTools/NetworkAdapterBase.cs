using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Common.Automation.Common.Helpers.DevTools
{
    public abstract class NetworkAdapterBase : INetworkAdapter
    {
        protected Dictionary<string, string> PendingRequests = new();
        private readonly List<string> _urLsToSkip = new() { ":443", "blob:", "cookielaw", "recaptcha", "data:", ".ads.", "track", "collect", "outlook", "analytics", "google", "facebook", "bing", "giosg", "data.microsoft" };

        protected const int MaxRequestIdLength = 20;
        protected readonly LoggerHelper LoggerHelper;
        protected readonly object LockObject = new();

        //public NetworkAdapterBase(IDevToolsSessionManager devToolsSessionManager, LoggerHelper loggerHelper)
        //{
        //    DevToolsSessionManager = devToolsSessionManager;
        //    LoggerHelper = loggerHelper;
        //}

        public bool ShouldSkipUrl(string url)
        {
            return _urLsToSkip.Any(url.Contains);
        }

        public abstract void Start(IWebDriver driver);
        public abstract void ListenRequests();
        public abstract void ListenLoadingFinished();
        public abstract void ListenLoadingFailed();

        public HashSet<string> GetPendingRequests()
        {
            lock (LockObject)
            {
                return PendingRequests.Keys.ToHashSet();
            }
        }

        public void ResetPendingRequests()
        {
            lock (LockObject)
            {
                PendingRequests = new Dictionary<string, string>();
                //PendingRequestIds = new HashSet<string>();
            }
        }

        public string GetStuckRequests()
        {
            var stuckUrls = string.Join(", ", PendingRequests.Values);
            return stuckUrls;
        }

        public void AddRequest(string requestUrl, string requestId)
        {
            if (ShouldSkipUrl(requestUrl)) return;

            lock (LockObject)
            {
                if (requestId.Length >= MaxRequestIdLength) return;
                PendingRequests[requestId] = requestUrl;
                //PendingRequestIds.Add(requestId);
            }
        }

        public void RemoveRequest(string requestId)
        {
            lock (LockObject)
            {
                //PendingRequestIds.Remove(requestId);

                if (!PendingRequests.ContainsKey(requestId)) return;
                PendingRequests.Remove(requestId);
                
            }
        }
    }
}