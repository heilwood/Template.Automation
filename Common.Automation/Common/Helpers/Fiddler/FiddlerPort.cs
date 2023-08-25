using System;

namespace Common.Automation.Common.Helpers.Fiddler
{
    public class FiddlerPort
    {
        public ushort Port { get; private set; }

        public FiddlerPort()
        {
            SetPort();
        }

        private void SetPort()
        {
            try
            {
                var listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, 0);
                listener.Start();
                Port = (ushort)((System.Net.IPEndPoint)listener.LocalEndpoint).Port;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to set the Fiddler port.", ex);
            }
        }
    }
}