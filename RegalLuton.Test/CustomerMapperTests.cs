using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegalLuton.Common.Interface;
using RegalLuton.Service;
using RegalLuton.Test.Mock;

namespace RegalLuton.Test
{
    [TestClass]
    public class CustomerMapperTests
    {

        private readonly ICustomerMapper mapper;
        private readonly ILogger logger;

        public CustomerMapperTests()
        {
            //setup our dependency injection so we can find desired implementations

            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICustomerMapper, CustomerMapperService>()
                .AddSingleton<ILogger, MockLoggerService>()
                .BuildServiceProvider();

            mapper = serviceProvider.GetService<ICustomerMapper>();
            logger = serviceProvider.GetService<ILogger>(); // singleton
        }

        [TestMethod]
        public void Data_With_Not_Enough_Columns_Should_Return_Null()
        {

            string data = "1, Title";

            var result = mapper.Map(data);

            Assert.IsNull(result);

            Assert.AreEqual(logger.MessageCount, 1);

        }

        [TestMethod]
        public void Data_With_Invalid_Data_Should_Return_Null()
        {

            string data = "1,Title,Firstname,Surname,Product Name, 10000x, 120";

            var result = mapper.Map(data);

            Assert.IsNull(result);

            Assert.AreEqual(logger.MessageCount, 1);

        }

        [TestMethod]
        public void Data_With_Enough_Columns_Should_Return_A_Customer()
        {

            string data = "1,Title,Firstname,Surname,Product Name, 10000, 120";

            var result = mapper.Map(data);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.Title, "Title");
            Assert.AreEqual(result.FirstName, "Firstname");
            Assert.AreEqual(result.Surname, "Surname");
            Assert.AreEqual(result.ProductName, "Product Name");
            Assert.AreEqual(result.PayoutAmount, 10000m);
            Assert.AreEqual(result.AnnualPremium, 120m);

            Assert.AreEqual(logger.MessageCount, 0);

        }

        [TestMethod]
        public void Data_With_Enough_Columns_And_A_Rounded_Average_Monthly_Premium_Should_Return_A_Customer_With_Same_Initial_And_Other_Payments()
        {

            string data = "1,Title,Firstname,Surname,Product Name, 10000, 120";

            var result = mapper.Map(data);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.Title, "Title");
            Assert.AreEqual(result.FirstName, "Firstname");
            Assert.AreEqual(result.Surname, "Surname");
            Assert.AreEqual(result.ProductName, "Product Name");
            Assert.AreEqual(result.PayoutAmount, 10000m);
            Assert.AreEqual(result.AnnualPremium, 120m);
            Assert.AreEqual(result.TotalPremium, 126m);

            Assert.AreEqual(result.AverageMonthlyPremium, 10.5m);
            Assert.AreEqual(result.InitialMonthlyPaymentAmount, 10.5m);
            Assert.AreEqual(result.OtherMonthlyPaymentAmount, 10.5m);

            Assert.AreEqual(logger.MessageCount, 0);

        }

        [TestMethod]
        public void Data_With_Enough_Columns_And_Not_A_Rounded_Average_Monthly_Premium_Should_Return_A_Customer_With_Higher_Initial_And_Equal_Other_Payments()
        {

            string data = "1,Title,Firstname,Surname,Product Name, 10000, 145";

            var result = mapper.Map(data);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.Title, "Title");
            Assert.AreEqual(result.FirstName, "Firstname");
            Assert.AreEqual(result.Surname, "Surname");
            Assert.AreEqual(result.ProductName, "Product Name");
            Assert.AreEqual(result.PayoutAmount, 10000m);
            Assert.AreEqual(result.AnnualPremium, 145m);
            Assert.AreEqual(result.TotalPremium, 152.25m);

            Assert.AreEqual(result.AverageMonthlyPremium, 12.6875m);
            Assert.AreEqual(result.InitialMonthlyPaymentAmount, 12.77m);
            Assert.AreEqual(result.OtherMonthlyPaymentAmount, 12.68m);

            Assert.AreEqual(logger.MessageCount, 0);

        }

    }
}
