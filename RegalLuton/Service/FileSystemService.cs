using Common.Model;
using RegalLuton.Common.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RegalLuton.Service
{
    public class FileSystemService : IFileSystem
    {

        private readonly ICustomerMapper customerMapperService;
        private readonly ILogger logger;

        public FileSystemService(ICustomerMapper customerMapperService, ILogger logger)
        {
            this.customerMapperService = customerMapperService;
            this.logger = logger;
        }

        public IEnumerable<CustomerModel> ReadCSV(string csvPath)
        {

            List<CustomerModel> customers = new List<CustomerModel>();

            try
            {

                // do not read the header row

                customers = File.ReadAllLines(csvPath)
                                           .Skip(1)
                                           .Select(v => this.customerMapperService.Map(v))
                                           .ToList();
            }
            catch (Exception e)
            {
                this.logger.Log(string.Concat("Data input error : ", e.Message));
            }
            
            return customers;
        }

        public bool CheckDirectoryExists(string path)
        {
            return (Directory.Exists(path));
        }

        public bool CheckFileExists(string path)
        {
            return (File.Exists(path));
        }

        public string CurrentDirectory()
        {
            return (Directory.GetCurrentDirectory());
        }

        public string DeterminePath(string basePath, string fileFolderName)
        {
            return (Path.GetFullPath(Path.Combine(basePath, fileFolderName)));
        }

        public string ReadTextFile(string path)
        {
            return (File.ReadAllText(path));
        }

        public void WriteTextFile(string filename, string content)
        {
            File.WriteAllText(filename, content);
        }
    }
}
