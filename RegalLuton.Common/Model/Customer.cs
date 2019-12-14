using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class Customer
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string ProductName { get; set; }

        public decimal PayoutAmount { get; set; }

        public decimal AnnualPremium { get; set; }

        public override string ToString()
        {
            return (@$"{System.Environment.NewLine} Id - {this.Id} 
                    {System.Environment.NewLine} {this.Title} {this.FirstName} {this.Surname}
                    {System.Environment.NewLine} {this.ProductName}
                    {System.Environment.NewLine} {this.PayoutAmount}
                    {System.Environment.NewLine} {this.AnnualPremium}");
        }

    }

}
