using Serilog;

namespace Common.Automation.Common.Helpers
{
    public class LoggerHelper
    {
        private static readonly Serilog.Core.Logger Logger;

        static LoggerHelper()
        {
            Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public Serilog.Core.Logger Log()
        {
            return Logger;
        }
    }
}