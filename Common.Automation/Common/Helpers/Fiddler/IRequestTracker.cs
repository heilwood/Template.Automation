using System.Collections.Generic;

namespace Common.Automation.Common.Helpers.Fiddler
{
    public interface IRequestTracker
    {
        void AddRequest(string url);
        void RemoveRequest(string url);
        List<string> GetPendingRequests();
    }
}
