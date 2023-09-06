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
        protected readonly object LockObject = new();


        public bool ShouldSkipUrl(string url)
        {
            return _urLsToSkip.Any(url.Contains);
        }

        public abstract void Start(IWebDriver driver);

        public HashSet<string> GetPendingRequests()
        {
            lock (LockObject)
            {
                return PendingRequests.Keys.ToHashSet();
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
            }
        }

        public void RemoveRequest(string requestId)
        {
            lock (LockObject)
            {
                if (!PendingRequests.ContainsKey(requestId)) return;
                PendingRequests.Remove(requestId);
            }
        }
    }
}