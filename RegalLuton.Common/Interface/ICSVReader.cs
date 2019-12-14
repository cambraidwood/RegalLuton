using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Common.Interface
{
    public interface ICSVReader
    {

        IEnumerable<Customer> Read(string csvPath);

    }
}
