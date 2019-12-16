using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegalLuton.Common.Interface;
using RegalLuton.Common.Model;
using RegalLuton.Service;
using RegalLuton.Test.Extensions;
using RegalLuton.Test.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Test
{
    [TestClass]
    public class LetterGeneratorFileFailTests
    {

        private readonly ILetterGenerator generator;
        private readonly ILogger logger;

        public LetterGeneratorFileFailTests()
        {
            //setup our dependency injection so we can find desired implementations

            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILetterGenerator, LetterGeneratorService>()
                .AddSingleton<IFileSystem>(p => p.ResolveWith<MockFailFileSystemService>(true, false))
                .AddSingleton<ILogger, MockLoggerService>()
                .BuildServiceProvider();

            generator = serviceProvider.GetService<ILetterGenerator>();
            logger = serviceProvider.GetService<ILogger>(); // singleton
        }

        [TestMethod]
        public void Generator_Should_Not_Process_Data_Into_Letters_When_File_Is_Not_Found()
        {

            GenerateModel model = new GenerateModel()
            {
                DataFileName = string.Empty,
                TemplateFileName = string.Empty,
                OutputFolder = string.Empty
            };

            var result = generator.Process(model);

            Assert.AreEqual(result.Count, 0);

            Assert.AreEqual(logger.MessageCount, 1);

        }

    }
}
