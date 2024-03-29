﻿using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Service
{
    public class LoggerService : ILogger
    {
        private List<string> messages = new List<string>();

        public int MessageCount => messages.Count;

        public void Log(string message)
        {
            messages.Add(message);

            Console.WriteLine(string.Concat(System.Environment.NewLine, "logged ... ", message));
        }
    }
}
