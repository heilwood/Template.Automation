using Serilog;

namespace Common.Automation.Common.Helpers
{
    public class LoggerHelper
    {
        public Serilog.Core.Logger Log()
        {
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            return log;
        }
    }
}