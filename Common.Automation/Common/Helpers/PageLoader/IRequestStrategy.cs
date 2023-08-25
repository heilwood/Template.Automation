using System.Collections.Generic;

namespace Common.Automation.Common.Helpers.PageLoader
{
    public interface IRequestStrategy
    {
        List<string> GetPendingRequests();
        void Start();
    }
}