using Common.Model;
using Microsoft.Extensions.DependencyInjection;
using RegalLuton.Common.Interface;
using RegalLuton.Common.Model;
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
                .AddSingleton<IFileSystem, FileSystemService>()
                .AddSingleton<ILogger, LoggerService>()
                .AddSingleton<ICustomerMapper, CustomerMapperService>()
                .AddSingleton<ILetterGenerator, LetterGeneratorService>()
                .AddSingleton<IConfigurationFile, ConfigurationFileService>()
                .BuildServiceProvider();

            // get services ...

            var generator = serviceProvider.GetService<ILetterGenerator>();
            var logger = serviceProvider.GetService<ILogger>();
            var config = serviceProvider.GetService<IConfigurationFile>();

            try
            {
                GenerateModel model = new GenerateModel();

                model.DataFileName = config.GetString("DataFileName");
                model.TemplateFileName = config.GetString("TemplateFileName");
                model.OutputFolder = config.GetString("OutputFolder");

                generator.Write(generator.Process(model));
            }
            catch (Exception e)
            {
                logger.Log(e.Message);
            }
            
        }
    }
}
