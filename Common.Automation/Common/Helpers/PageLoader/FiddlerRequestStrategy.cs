using System;
using System.Collections.Generic;
using Common.Automation.Common.Helpers.Fiddler;

namespace Common.Automation.Common.Helpers.PageLoader
{
    public class FiddlerRequestStrategy : IRequestStrategy
    {
        private readonly IFiddlerMonitor _fiddlerMonitor;
        public FiddlerRequestStrategy(IFiddlerMonitor fiddlerMonitor)
        {
            _fiddlerMonitor  = fiddlerMonitor;
        }

        public List<string> GetPendingRequests()
        {
            return _fiddlerMonitor.GetPendingRequests();
        }

        public void Start()
        {
            _fiddlerMonitor.Start();
        }
    }
}