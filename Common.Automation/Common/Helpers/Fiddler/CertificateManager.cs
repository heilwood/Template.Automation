// CertificateManager.cs

using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Fiddler;

namespace Common.Automation.Common.Helpers.Fiddler
{
    public class CertificateManager
    {
        private const string CertPath = "ExampleFiddlerRoot.cer";
        private const string CertName = "ExampleFiddlerRoot";
        private readonly LoggerHelper _loggerHelper;

        public CertificateManager(LoggerHelper loggerHelper)
        {
            _loggerHelper = loggerHelper ?? throw new ArgumentNullException(nameof(loggerHelper));
        }

        public bool IsCertificateInstalled()
        {
            using (X509Store store = new(StoreName.Root, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                foreach (var cert in store.Certificates)
                {
                    if (cert.Subject.Contains(CertName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public void EnsureCertificateIsInstalled()
        {
            if (IsCertificateInstalled())
            {
                return;
            }

            if (!CertMaker.rootCertExists())
            {
                if (!CertMaker.createRootCert())
                {
                    _loggerHelper.Log().Error("Failed to create Fiddler root certificate.");
                    return;
                }
            }

            byte[] certificateBytes;
            try
            {
                certificateBytes = CertMaker.GetRootCertificate().Export(X509ContentType.Cert);
                InstallCertificate(certificateBytes);
            }
            catch (Exception ex)
            {
                _loggerHelper.Log().Error($"Error exporting or writing certificate: {ex.Message}");
            }
        }


        public void InstallCertificate(byte[] certificateBytes)
        {
            if (IsCertificateInstalled())
            {
                return;
            }

            File.WriteAllBytes(CertPath, certificateBytes);
            InstallCertificateSilently(CertPath);
        }

        private void InstallCertificateSilently(string certPath)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "certutil.exe",
                Arguments = $"-addstore -f \"Root\" \"{certPath}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    var output = process.StandardOutput.ReadToEnd();
                    var error = process.StandardError.ReadToEnd();
                    _loggerHelper.Log().Error($"Failed to install certificate. Output: {output} Error: {error}");
                }
            }
        }
    }
}
