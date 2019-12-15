using Common.Model;
using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Service
{
    public class CustomerMapperService : ICustomerMapper
    {

        private readonly ILogger logger;

        public CustomerMapperService(ILogger logger)
        {
            this.logger = logger;
        }

        public CustomerModel Map(string csvLine)
        {
            string[] values = csvLine.Split(',');

            CustomerModel customer = null;

            if (values.Length == 7)
            {
                
                try
                {
                    string title = values[1];
                    string firstName = values[2];
                    string surname = values[3];
                    decimal payoutAmount = Convert.ToDecimal(values[5]);
                    decimal annualPremium = Convert.ToDecimal(values[6]);

                    customer = new CustomerModel(payoutAmount, annualPremium)
                    {
                        Id = Convert.ToInt32(values[0]),
                        Title = title,
                        FirstName = firstName,
                        Surname = surname,
                        ProductName = values[4],
                        TitleFullName = string.Concat(title, " ", firstName, " ", surname),
                        TitleSurname = string.Concat(title, " ", surname)
                    };
                }
                catch (Exception e)
                {

                    customer = null;

                    this.logger.Log(string.Concat("Data conversion error : ", string.Join(", ", values)));
                }

            }
            else
            {
                this.logger.Log(string.Concat("Unexpected data error : ", string.Join(", ", values)));
            }

            return customer;
        }

    }
}
