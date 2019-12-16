
using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Test.Mock
{
    public class MockLoggerService : ILogger
    {

        private List<string> messages = new List<string>();

        public int MessageCount => messages.Count;

        public void Log(string message)
        {
            messages.Add(message);
        }
    }
}
