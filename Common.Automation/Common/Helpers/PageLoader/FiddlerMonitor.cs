using System.Collections.Generic;
using Fiddler;

namespace Common.Automation.Common.Helpers.PageLoader
{
    public class FiddlerMonitor : IFiddlerMonitor
    {
        private readonly IRequestTracker _requestTracker;

        public FiddlerMonitor(IRequestTracker requestTracker)
        {
            _requestTracker = requestTracker;
        }

        public void Start()
        {
            var settings = new FiddlerCoreStartupSettingsBuilder()
                .RegisterAsSystemProxy()
                .ListenOnPort(8888)
                .Build();

            FiddlerApplication.Startup(settings);


            FiddlerApplication.BeforeRequest += (session) => _requestTracker.AddRequest(session.fullUrl);
            FiddlerApplication.BeforeResponse += (session) => _requestTracker.RemoveRequest(session.fullUrl);
        }

        public void Stop()
        {
            FiddlerApplication.Shutdown();
        }

        public List<string> GetPendingRequests()
        {
            return _requestTracker.GetPendingRequests();
        }
    }
}