using Common.Model;
using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RegalLuton.Service
{
    public class CSVReaderService : ICSVReader
    {

        private readonly ICustomerMapper _customerMapperService;

        public CSVReaderService(ICustomerMapper customerMapperService)
        {
            _customerMapperService = customerMapperService;
        }

        public IEnumerable<Customer> Read(string csvPath)
        {

            List<Customer> customers = new List<Customer>();


            try
            {

                // do not read the header row

                customers = File.ReadAllLines(csvPath)
                                           .Skip(1)
                                           .Select(v => _customerMapperService.Map(v))
                                           .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return customers;
        }
    }
}
