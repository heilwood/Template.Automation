using System;
using System.Collections.Generic;
using Common.Automation.Common.Helpers.PageLoader;
using Fiddler;

namespace Common.Automation.Common.Helpers.Fiddler
{
    public class FiddlerMonitor : IFiddlerMonitor
    {
        private readonly IRequestTracker _requestTracker;
        private readonly CertificateManager _certificateManager;
        private readonly FiddlerPort _fiddlerPort;


        public FiddlerMonitor(IRequestTracker requestTracker, CertificateManager certificateManager, FiddlerPort fiddlerPort)
        {
            _requestTracker = requestTracker ?? throw new ArgumentNullException(nameof(requestTracker));
            _certificateManager = certificateManager ?? throw new ArgumentNullException(nameof(certificateManager));
            _fiddlerPort = fiddlerPort ?? throw new ArgumentNullException(nameof(fiddlerPort));
        }

        public void Start()
        {
            var settings = new FiddlerCoreStartupSettingsBuilder()
                .DecryptSSL()
                .ListenOnPort(_fiddlerPort.Port)
                .Build();

            _certificateManager.EnsureCertificateIsInstalled();

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

        //private ushort GetAvailablePort()
        //{
        //    var listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, 0);
        //    listener.Start();
        //    var port = (ushort)((System.Net.IPEndPoint)listener.LocalEndpoint).Port;
        //    listener.Stop();
        //    FiddlerPort = port;
        //    return port;
        //}
    }
}