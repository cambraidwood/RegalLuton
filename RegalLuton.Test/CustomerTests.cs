using Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Test
{
    [TestClass]
    public class CustomerTests
    {

        public CustomerTests()
        {

        }

        [TestMethod]
        public void CalculateCreditCharge_Should_Roundup_If_Pence_Fractional()
        {

            CustomerModel model = new CustomerModel(0, 0);

            var result = model.CalculateCreditCharge(123.45m);

            Assert.AreEqual(result, 6.18m);

        }

        [TestMethod]
        public void CalculateCreditCharge_Should_Not_Roundup_If_Pence_Is_Not_Fractional()
        {

            CustomerModel model = new CustomerModel(0, 0);

            var result = model.CalculateCreditCharge(120);

            Assert.AreEqual(result, 6);

        }

        [TestMethod]
        public void IsRoundPoundsPence_Should_Return_True_For_Valid_Payment_Value()
        {

            CustomerModel model = new CustomerModel(0, 0);

            var result = model.IsRoundPoundsPence(120.01m);

            Assert.AreEqual(result, true);

        }

        [TestMethod]
        public void IsRoundPoundsPence_Should_Return_False_For_Invalid_Payment_Value()
        {

            CustomerModel model = new CustomerModel(0, 0);

            var result = model.IsRoundPoundsPence(120.0156m);

            Assert.AreEqual(result, false);

        }

        [TestMethod]
        public void CalculateMonthlyPayments_Should_Return_Same_Values_For_Initial_And_Other_Payments_If_Valid_Average_Value()
        {

            CustomerModel model = new CustomerModel(0, 0);

            var result = model.CalculateMonthlyPayments(126, 10.5m);

            Assert.AreEqual(result.initialPayment, 10.5m);
            Assert.AreEqual(result.otherPayment, 10.5m);

        }

        [TestMethod]
        public void CalculateMonthlyPayments_Should_Return_Different_Values_For_Initial_And_Other_Payments_If_Invalid_Average_Value()
        {

            CustomerModel model = new CustomerModel(0, 0);

            var result = model.CalculateMonthlyPayments(152.25m, 12.6875m);

            Assert.AreEqual(result.initialPayment, 12.77m);
            Assert.AreEqual(result.otherPayment, 12.68m);

        }
    }
}
