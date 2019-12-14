using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Common.Interface
{
    public interface ICustomerMapper
    {
        public Customer Map(string csvLine);
    }
}
