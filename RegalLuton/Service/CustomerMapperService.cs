using Common.Model;
using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Service
{
    public class CustomerMapperService : ICustomerMapper
    {

        private readonly ILogger _logger;

        public CustomerMapperService(ILogger logger)
        {
            _logger = logger;
        }

        public Customer Map(string csvLine)
        {
            string[] values = csvLine.Split(',');

            Customer customer = null;

            if (values.Length == 8)
            {

                customer = new Customer();

                customer.Id = Convert.ToInt32(values[0]);
                customer.Title = values[1];
                customer.FirstName = values[2];
                customer.Surname = values[3];
                customer.ProductName = values[4];
                customer.PayoutAmount = Convert.ToDecimal(values[5]);
                customer.AnnualPremium = Convert.ToDecimal(values[6]);
            }
            else
            {
                _logger.Log("Data issue");
            }

            return customer;
        }

    }
}
