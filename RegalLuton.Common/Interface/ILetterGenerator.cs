using Common.Model;
using RegalLuton.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Common.Interface
{
    public interface ILetterGenerator
    {

        IEnumerable<LetterModel> Process(GenerateModel model);

        LetterModel Generate(CustomerModel model, string templateFileName, string outputFolder);

        void Write(IEnumerable<LetterModel> letters);

    }
}
