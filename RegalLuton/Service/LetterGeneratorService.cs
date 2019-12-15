using Common.Model;
using RegalLuton.Common.Interface;
using RegalLuton.Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RegalLuton.Service
{
    public class LetterGeneratorService : ILetterGenerator
    {

        private readonly IFileSystem fileSystem;
        private readonly ILogger logger;

        public LetterGeneratorService(IFileSystem fileSystem, ILogger logger)
        {
            this.fileSystem = fileSystem;
            this.logger = logger;
        }

        public IEnumerable<LetterModel> Process(GenerateModel model)
        {

            List<LetterModel> results = new List<LetterModel>();

            string currentDirectory = this.fileSystem.CurrentDirectory();

            string dataFileName = model.DataFileName;
            string fullDataPath = this.fileSystem.DeterminePath(currentDirectory, dataFileName);

            string templateFileName = model.TemplateFileName;
            string fullTemplatePath = this.fileSystem.DeterminePath(currentDirectory, templateFileName);

            string outputFolder = model.OutputFolder;
            string fullOutputFolderPath = this.fileSystem.DeterminePath(currentDirectory, outputFolder);

            if (!this.fileSystem.CheckFileExists(fullDataPath))
            {
                this.logger.Log("Cannot find data file");
                return results;
            }

            if (!this.fileSystem.CheckFileExists(fullTemplatePath))
            {
                this.logger.Log("Cannot find letter template file");
                return results;
            }

            if (!this.fileSystem.CheckDirectoryExists(fullOutputFolderPath))
            {
                this.logger.Log("Cannot find output folder");
                return results;
            }

            var data = this.fileSystem.ReadCSV(fullDataPath);

            foreach (CustomerModel customer in data)
            {
                if (customer != null)
                {
                    results.Add(Generate(customer, fullTemplatePath, fullOutputFolderPath));
                }
            }

            return results;

        }

        public LetterModel Generate(CustomerModel model, string templateFileName, string outputFolder)
        {

            LetterModel result = new LetterModel();

            try
            {
                result.FileName = string.Concat(outputFolder, model.Id, "_", model.TitleFullName.Replace(' ', '_'), ".txt");

                string template = this.fileSystem.ReadTextFile(templateFileName);

                List<TokenModel> tokenList = new List<TokenModel>();

                tokenList.Add(new TokenModel { Token = "{currentDate}", Value = model.CurrentDate });
                tokenList.Add(new TokenModel { Token = "{titleFullName}", Value = model.TitleFullName });
                tokenList.Add(new TokenModel { Token = "{titleSurname}", Value = model.TitleSurname });
                tokenList.Add(new TokenModel { Token = "{productName}", Value = model.ProductName });
                tokenList.Add(new TokenModel { Token = "{payoutAmount}", Value = model.PayoutAmount.ToString("F2") });
                tokenList.Add(new TokenModel { Token = "{annualPremium}", Value = model.AnnualPremium.ToString("F2") });
                tokenList.Add(new TokenModel { Token = "{creditCharge}", Value = model.CreditCharge.ToString("F2") });
                tokenList.Add(new TokenModel { Token = "{totalPremium}", Value = model.TotalPremium.ToString("F2") });
                tokenList.Add(new TokenModel { Token = "{initialMonthlyPayment}", Value = model.InitialMonthlyPaymentAmount.ToString("F2") });
                tokenList.Add(new TokenModel { Token = "{otherMonthlyPayment}", Value = model.OtherMonthlyPaymentAmount.ToString("F2") });

                foreach (TokenModel tm in tokenList)
                {
                    template = template.Replace(tm.Token, tm.Value);
                }

                result.Content = template;

            }
            catch (Exception e)
            {
                this.logger.Log(string.Concat("Generate letter error : ", e.Message));
            }

            return result;

        }

        public void Write(IEnumerable<LetterModel> letters)
        {
            foreach (LetterModel letter in letters)
            {

                if (this.fileSystem.CheckFileExists(letter.FileName))
                {
                    this.logger.Log(string.Concat("Letter already exists : ", letter.FileName));
                }
                else
                {
                    if (!string.IsNullOrEmpty(letter.Content))
                    {
                        this.fileSystem.WriteTextFile(letter.FileName, letter.Content);
                    }
                }
                
            }
        }

    }
}
