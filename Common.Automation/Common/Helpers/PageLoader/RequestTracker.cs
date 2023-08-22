using Fiddler;
using System.Collections.Generic;

namespace Common.Automation.Common.Helpers.PageLoader
{
    public class RequestTracker : IRequestTracker
    {
        private readonly List<string> _pendingRequests = new();
        private readonly List<string> _requestUrlsToSkip =  new() { "http://st.if.eu:443", ".ads.", "analytics", "google", "facebook", "bing", "giosg", "data.microsoft" };


        public void AddRequest(string url)
        {
            if (_requestUrlsToSkip.Contains(url)) return;
            lock (_pendingRequests)
            {
                _pendingRequests.Add(url);
            }
        }

        public void RemoveRequest(string url)
        {
            lock (_pendingRequests)
            {
                _pendingRequests.Remove(url);
            }
        }

        public List<string> GetPendingRequests()
        {
            lock (_pendingRequests)
            {
                return new List<string>(_pendingRequests);
            }
        }
    }
}