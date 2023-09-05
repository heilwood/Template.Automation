using System.Collections.Generic;
using System.Linq;

namespace Common.Automation.Common.Helpers.DevTools
{
    public abstract class NetworkAdapterBase : INetworkAdapter
    {
        //TODO: Remove PendingRequests you dont need to use them as with stuckrequest as they are the same only differce
        protected readonly Dictionary<string, string> StuckRequests = new();
        private readonly List<string> _urLsToSkip = new() { ":443", "blob:", "cookielaw", "recaptcha", "data:", ".ads.", "track", "collect", "outlook", "analytics", "google", "facebook", "bing", "giosg", "data.microsoft" };
        protected HashSet<string> PendingRequestIds = new();

        protected const int MaxRequestIdLength = 20;
        protected readonly IDevToolsSessionManager DevToolsSessionManager;
        protected readonly LoggerHelper LoggerHelper;
        protected readonly object LockObject = new();

        public NetworkAdapterBase(IDevToolsSessionManager devToolsSessionManager, LoggerHelper loggerHelper)
        {
            DevToolsSessionManager = devToolsSessionManager;
            LoggerHelper = loggerHelper;
        }

        public bool ShouldSkipUrl(string url)
        {
            return _urLsToSkip.Any(url.Contains);
        }

        public abstract void Start();
        public abstract void ListenRequests();
        public abstract void ListenLoadingFinished();
        public abstract void ListenLoadingFailed();

        public HashSet<string> GetPendingRequests()
        {
            lock (LockObject)
            {
                return PendingRequestIds;
            }
        }

        public void ResetPendingRequests()
        {
            lock (LockObject)
            {
                PendingRequestIds = new HashSet<string>();
            }
        }

        public string GetStuckRequests()
        {
            var stuckUrls = string.Join(", ", StuckRequests.Values);
            return stuckUrls;
        }

        public void AddRequest(string requestUrl, string requestId)
        {
            if (ShouldSkipUrl(requestUrl)) return;

            lock (LockObject)
            {
                if (requestId.Length >= MaxRequestIdLength) return;
                StuckRequests[requestId] = requestUrl;
                PendingRequestIds.Add(requestId);
            }
        }

        public void RemoveRequest(string requestId)
        {
            lock (LockObject)
            {
                PendingRequestIds.Remove(requestId);

                if (!StuckRequests.ContainsKey(requestId)) return;
                StuckRequests.Remove(requestId);
                LoggerHelper.Log().Information($"Remove url: {StuckRequests[requestId]}");
            }
        }
    }
}