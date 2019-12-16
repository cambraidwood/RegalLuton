using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegalLuton.Common.Interface;
using RegalLuton.Common.Model;
using RegalLuton.Service;
using RegalLuton.Test.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Test
{
    [TestClass]
    public class LetterGeneratorTests
    {

        private readonly ILetterGenerator generator;
        private readonly ILogger logger;

        public LetterGeneratorTests()
        {
            //setup our dependency injection so we can find desired implementations

            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILetterGenerator, LetterGeneratorService>()
                .AddSingleton<IFileSystem, MockFileSystemService>()
                .AddSingleton<ILogger, MockLoggerService>()
                .BuildServiceProvider();

            generator = serviceProvider.GetService<ILetterGenerator>();
            logger = serviceProvider.GetService<ILogger>(); // singleton
        }

        [TestMethod]
        public void Generator_Should_Process_Data_Into_Letters()
        {

            GenerateModel model = new GenerateModel()
            {
                 DataFileName = string.Empty,
                 TemplateFileName = string.Empty,
                 OutputFolder = string.Empty
            };

            var result = generator.Process(model);

            Assert.AreEqual(result.Count, 3);

            Assert.AreEqual(result[0].FileName, "1_Mr_Cameron_Braidwood.txt");
            Assert.IsTrue(result[0].Content.Contains("Cameron"));
            Assert.IsTrue(result[0].Content.Contains("Braidwood"));

        }

    }



}
