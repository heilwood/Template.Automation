using System;
using System.Collections.Generic;
using System.Linq;
using Common.Automation.Common.Helpers.PageLoader;

namespace Common.Automation.Common.Helpers.Fiddler
{
    public class RequestTracker : IRequestTracker
    {
        private readonly List<string> _pendingRequests = new();
        private readonly List<string> _requestUrlsToSkip =  new() { ":443", ".ads.", "firefox", "mozilla", "track", "collect", "outlook", "analytics", "google", "facebook", "bing", "giosg", "data.microsoft" };


        public void AddRequest(string url)
        {
            if (_requestUrlsToSkip.Any(url.Contains)) return;
            lock (_pendingRequests)
            {
                Console.WriteLine(url);
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