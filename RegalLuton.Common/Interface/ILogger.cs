using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Common.Interface
{
    public interface ILogger
    {
        void Log(string message);

        int MessageCount { get; }
    }
}
