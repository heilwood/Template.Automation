using System.IO;
using System.Reflection;

namespace Common.Automation.Common.Helpers
{
    public class PathHelper
    {
        public static string GetWorkingDirectory()
        {

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            var fullPath = Path.GetFullPath(Path.Combine(path, @"..\..\"));
            return fullPath;
        }
    }
}
