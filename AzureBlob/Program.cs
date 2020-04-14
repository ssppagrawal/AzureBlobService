
using Azure.Storage.Blobs;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AzureBlob
{
    class Program
    {
        //@Jagruti merge app.config with your web.config as it will look for keys there
        //Installed Azure.Storage.Blob as Nuget Package
        static async Task Main(string[] args)
        {
            string accountName = ConfigurationManager.AppSettings["AccountName"];
            string key = ConfigurationManager.AppSettings["Key"];
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            string filePath = @"C:\Users\saurabha\Desktop\asdf.txt";
            using FileStream fileStream = File.OpenRead(filePath);



            AzureBlobAPIs aba = new AzureBlobAPIs(connectionString);

            //aba.uploadBlob("cell-phone-bill", "asdf.txt-yourGUID.txt", fileStream);

            aba.uploadBlob("gym-bill", "asdf.txt-yourGUID.txt", fileStream);


            //aba.deleteContainer("cell-phone-bill");

            //aba.deleteBlob("cell-phone-bill", "asdf.txt-yourGUID.txt");

            Console.WriteLine(accountName);
            Console.WriteLine(key);
            Console.WriteLine(connectionString);
        }
    }
}
