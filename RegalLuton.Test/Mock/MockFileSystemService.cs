using Common.Model;
using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Test.Mock
{
    public class MockFileSystemService : IFileSystem
    {
        public bool CheckDirectoryExists(string path)
        {
            return true;
        }

        public bool CheckFileExists(string path)
        {
            return true;
        }

        public string CurrentDirectory()
        {
            return string.Empty;
        }

        public string DeterminePath(string basePath, string fileFolderName)
        {
            return string.Empty;
        }

        public IEnumerable<CustomerModel> ReadCSV(string csvPath)
        {
            List<CustomerModel> results = new List<CustomerModel>();

            results.Add(new CustomerModel(1000, 120)
            {
                Id = 1,
                Title = "Mr",
                FirstName = "Cameron",
                Surname = "Braidwood",
                ProductName = "Product 1",
                TitleFullName = "Mr Cameron Braidwood",
                TitleSurname = "Mr Braidwood"
            });

            results.Add(new CustomerModel(2000, 150)
            {
                Id = 2,
                Title = "Miss",
                FirstName = "Julie",
                Surname = "Smith",
                ProductName = "Product 2",
                TitleFullName = "Miss Julie Smith",
                TitleSurname = "Miss Smith"
            });

            results.Add(new CustomerModel(3000, 90)
            {
                Id = 3,
                Title = "Miss",
                FirstName = "Nia",
                Surname = "Medina",
                ProductName = "Product 3",
                TitleFullName = "Miss Nia Medina",
                TitleSurname = "Miss Medina"
            });

            return results;
        }

        public string ReadTextFile(string path)
        {
            return @"{currentDate}

FAO: {titleFullName}

RE: Your Renewal

Dear {titleSurname}

We hereby invite you to renew your Insurance Policy, subject to the following terms.

Your chosen insurance product is {productName}.
	
The amount payable to you in the event of a valid claim will be £{payoutAmount}.
	
Your annual premium will be £{annualPremium}.
	
If you choose to pay by Direct Debit, we will add a credit charge of £{creditCharge}, bringing the total to £{totalPremium}. This is payable by an initial payment of £{initialMonthlyPayment}, followed by 11 payments of £{otherMonthlyPayment} each.

Please get in touch with us to arrange your renewal by visiting https://www.regallutoncodingtest.co.uk/renew or calling us on 01625 123456.

Kind Regards
Regal Luton";

        }

        public void WriteTextFile(string fileName, string content)
        {
            
        }
    }
}
