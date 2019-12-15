using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Common.Interface
{
    public interface IFileSystem
    {

        IEnumerable<CustomerModel> ReadCSV(string csvPath);

        bool CheckFileExists(string path);

        bool CheckDirectoryExists(string path);

        string CurrentDirectory();

        string DeterminePath(string basePath, string fileFolderName);

        string ReadTextFile(string path);

        void WriteTextFile(string fileName, string content);

    }
}
