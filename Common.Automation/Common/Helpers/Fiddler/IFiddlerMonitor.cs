using System.Collections.Generic;

namespace Common.Automation.Common.Helpers.Fiddler
{
    public interface IFiddlerMonitor
    {
        void Start();
        void Stop();
        List<string> GetPendingRequests();
    }
}