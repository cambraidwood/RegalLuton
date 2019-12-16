using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class CustomerModel
    {

        public CustomerModel(decimal payoutAmount, decimal annualPremium)
        {
            this.CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
            this.PayoutAmount = payoutAmount;
            this.AnnualPremium = annualPremium;

            this.CreditCharge = CalculateCreditCharge(this.AnnualPremium);
            this.TotalPremium = this.AnnualPremium + this.CreditCharge;
            this.AverageMonthlyPremium = this.TotalPremium / 12m;

            if (IsRoundPoundsPence(this.AverageMonthlyPremium))
            {
                this.InitialMonthlyPaymentAmount = this.AverageMonthlyPremium;
                this.OtherMonthlyPaymentAmount = this.AverageMonthlyPremium;
            }
            else
            {
                var payments = CalculateMonthlyPayments(this.TotalPremium, this.AverageMonthlyPremium);

                this.InitialMonthlyPaymentAmount = payments.initialPayment;
                this.OtherMonthlyPaymentAmount = payments.otherPayment;
            }

        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string ProductName { get; set; }

        public decimal PayoutAmount { get; set; }

        public decimal AnnualPremium { get; set; }

        // calculated fields

        public string CurrentDate { get; set; }

        public string TitleFullName { get; set; }

        public string TitleSurname { get; set; }

        public decimal CreditCharge { get; set; }

        public decimal TotalPremium { get; set; }

        public decimal AverageMonthlyPremium { get; set; }

        public decimal InitialMonthlyPaymentAmount { get; set; }

        public decimal OtherMonthlyPaymentAmount { get; set; }

        public override string ToString()
        {
            return (@$"{System.Environment.NewLine} Id - {this.Id} 
                    {System.Environment.NewLine} {this.Title} {this.FirstName} {this.Surname}
                    {System.Environment.NewLine} {this.ProductName}
                    {System.Environment.NewLine} {this.PayoutAmount.ToString("F2")}
                    {System.Environment.NewLine} {this.AnnualPremium.ToString("F2")}
                    {System.Environment.NewLine} {this.CurrentDate}
                    {System.Environment.NewLine} {this.TitleFullName}
                    {System.Environment.NewLine} {this.TitleSurname}
                    {System.Environment.NewLine} {this.CreditCharge.ToString("F2")}
                    {System.Environment.NewLine} {this.TotalPremium.ToString("F2")}
                    {System.Environment.NewLine} {this.AverageMonthlyPremium}
                    {System.Environment.NewLine} {this.InitialMonthlyPaymentAmount.ToString("F2")}
                    {System.Environment.NewLine} {this.OtherMonthlyPaymentAmount.ToString("F2")}");
        }

        public decimal CalculateCreditCharge(decimal annualPremium)
        {

            // I am assuming that any fragment of a penny results in an upwards rounding

            decimal actual = annualPremium * 0.05m;
            decimal rounded = Math.Round(actual, 2, MidpointRounding.AwayFromZero);

            if (actual > rounded)
            {
                return rounded + 0.01m;
            }
            else
            {
                return rounded;
            }
        }

        public bool IsRoundPoundsPence(decimal actual)
        {
            decimal rounded = Math.Round(actual, 2, MidpointRounding.AwayFromZero);

            return (actual == rounded);
        }

        public (decimal initialPayment, decimal otherPayment) CalculateMonthlyPayments(decimal total, decimal average)
        {
            decimal roundedDown = Math.Round(average, 2, MidpointRounding.ToZero);

            return (total - (11m * roundedDown), roundedDown);
        }

    }

}
