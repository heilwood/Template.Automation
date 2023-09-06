using System;

namespace Common.Automation.Exceptions
{
    public class UnsupportedBrowserException : Exception
    {
        public UnsupportedBrowserException(string message) : base(message) { }
    }
}
