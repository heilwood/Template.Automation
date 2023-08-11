using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Automation.Exceptions
{
    public class UnsupportedBrowserException : Exception
    {
        public UnsupportedBrowserException(string message) : base(message) { }
    }
}
