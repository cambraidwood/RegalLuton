using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace RegalLuton.Service
{
    public class ConfigurationFileService : IConfigurationFile
    {

        private readonly ILogger logger;

        public ConfigurationFileService(ILogger logger)
        {
            this.logger = logger;
        }

        public int? GetInteger(string key)
        {
            int result;
            bool isValid = false;

            string val = ConfigurationManager.AppSettings[key];

            if (!string.IsNullOrEmpty(val))
            {
                isValid = int.TryParse(val, out result);

                if (isValid)
                {
                    return (result);
                }
                else
                {
                    this.logger.Log(string.Concat("Configuration integer not valid : ", val));
                }
            }
            else
            {
                this.logger.Log(string.Concat("Configuration integer not found : ", key));
            }

            return null;

        }

        public string GetString(string key)
        {

            string val = ConfigurationManager.AppSettings[key];

            if (!string.IsNullOrEmpty(val))
            {
                return (val);
            }
            else
            {
                this.logger.Log(string.Concat("Configuration string not found : ", key));
            }

            return string.Empty;

        }

    }
}
