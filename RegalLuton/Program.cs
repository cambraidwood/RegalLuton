using Common.Model;
using Microsoft.Extensions.DependencyInjection;
using RegalLuton.Common.Interface;
using RegalLuton.Service;
using System;
using System.IO;

namespace RegalLuton
{
    class Program
    {
        static void Main(string[] args)
        {

            // setup our dependency injection so we can find the implementations
            // of the service classes

            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICSVReader, CSVReaderService>()
                .AddSingleton<ILogger, LoggerService>()
                .AddSingleton<ICustomerMapper, CustomerMapperService>()
                .BuildServiceProvider();

            // locate a service that implements the CSV Reader interface ...

            var reader = serviceProvider.GetService<ICSVReader>();
            var logger = serviceProvider.GetService<ILogger>();

            try
            {
                string path = Directory.GetCurrentDirectory();
                string filename = @"..\..\..\Resources\Input\customer.csv";
                string fullPath = Path.GetFullPath(Path.Combine(path, filename));

                if (File.Exists(fullPath))
                {
                    var data = reader.Read(fullPath);

                    foreach (Customer c in data)
                    {
                        logger.Log(c?.ToString());
                    }

                }
            }
            catch (Exception e)
            {
                logger.Log(e.Message);
            }
            
        }
    }
}
