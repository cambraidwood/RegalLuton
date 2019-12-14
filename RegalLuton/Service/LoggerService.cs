using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Service
{
    public class LoggerService : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(string.Concat("ooh, we logged something ...", message));
        }
    }
}
