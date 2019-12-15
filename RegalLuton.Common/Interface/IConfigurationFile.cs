using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Common.Interface
{
    public interface IConfigurationFile
    {

        string GetString(string key);

        int? GetInteger(string key);

    }
}
