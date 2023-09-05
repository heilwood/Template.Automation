using System.Collections.Generic;

namespace Common.Automation.Common.Helpers.DevTools
{
    public interface INetworkAdapter
    {
        HashSet<string> GetPendingRequests(); 
        string GetStuckRequests();
        void ResetPendingRequests();
        void Start();
    }
}