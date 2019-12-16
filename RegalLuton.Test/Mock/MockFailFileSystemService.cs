using Common.Model;
using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegalLuton.Test.Mock
{
    public class MockFailFileSystemService : IFileSystem
    {

        private bool fileExists = false;
        private bool directoryExists = false;

        public MockFailFileSystemService(bool directoryExists, bool fileExists)
        {
            this.directoryExists = directoryExists;
            this.fileExists = fileExists;
        }

        public bool CheckDirectoryExists(string path)
        {
            return this.directoryExists;
        }

        public bool CheckFileExists(string path)
        {
            return this.fileExists;
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

            return results;
        }

        public string ReadTextFile(string path)
        {
            return string.Empty;
        }

        public void WriteTextFile(string fileName, string content)
        {

        }

    }

}
