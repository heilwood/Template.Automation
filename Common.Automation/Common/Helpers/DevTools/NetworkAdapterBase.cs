using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Common.Automation.Common.Helpers.DevTools
{
    public abstract class NetworkAdapterBase : INetworkAdapter
    {
        private readonly List<string> _urLsToSkip = new() { ":443", "blob:", "cookielaw", "recaptcha", "data:", ".ads.", "track", "collect", "outlook", "analytics", "google", "facebook", "bing", "giosg", "data.microsoft" };
        protected ConcurrentDictionary<string, string> PendingRequests = new();
        protected const int MaxRequestIdLength = 20;
        protected bool IsListening;
        public abstract void Start(IWebDriver driver);

        public bool ShouldSkipUrl(string url) => _urLsToSkip.Any(url.Contains);

        public HashSet<string> GetPendingRequests() => PendingRequests.Keys.ToHashSet();

        public string GetStuckRequests() => string.Join(", ", PendingRequests.Values);

        public void ResetPendingRequests() => PendingRequests.Clear();

        public void RemoveRequest(string requestId) => PendingRequests.TryRemove(requestId, out _);

        public void AddRequest(string requestUrl, string requestId)
        {
            if (ShouldSkipUrl(requestUrl)) return;
            if (requestId.Length >= MaxRequestIdLength) return;

            PendingRequests[requestId] = requestUrl;
        }

       

    }
}