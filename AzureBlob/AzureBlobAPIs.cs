using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Azure.Storage.Blobs;
using Microsoft.Azure.Storage.Blob;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Azure.Storage.Blobs.Models;
using System.IO;

namespace AzureBlob
{
    public class AzureBlobAPIs
    {
        private BlobServiceClient blobServiceClient;
        private BlobContainerClient blobContainerClient;

        private List<BlobContainerClient> blobContainerClientList; 
        private string connectionString;

        public AzureBlobAPIs(string connectionString)
        {
            this.connectionString = connectionString;
            blobServiceClient = new BlobServiceClient(connectionString);

            string containerName = "container-" + Guid.NewGuid();


        }

        public BlobContainerClient createOrGetContainer(string containerName)
        {
            var a =  this.blobServiceClient.GetBlobContainerClient(containerName).CreateIfNotExists();
            return blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task uploadBlob(string containerName , string fileName , FileStream fileStream)
        {
            BlobContainerClient blobContainerClient = createOrGetContainer(containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, true);
        }

        public async Task<Stream> downloadBlob(string containerName, string fileName)
        {
            BlobContainerClient blobContainerClient = createOrGetContainer(containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
            BlobDownloadInfo download = await blobClient.DownloadAsync();
            return download.Content;
        }

        public async Task deleteContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = createOrGetContainer(containerName);
            Console.WriteLine("Deleting blob container...");
            await blobContainerClient.DeleteAsync();
        }

        public async Task deleteBlob(string containerName, string fileName)
        {
            BlobContainerClient blobContainerClient = createOrGetContainer(containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
            await  blobClient.DeleteAsync();
        }

        //public async Task<List<BlobItem>> GetAllBlobsInsideContainer(string containerName)
        //{
        //    this.blobServiceClient.get
        //    var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
        //    var a = await containerClient.GetBlobsAsync();
        //}

        public async  Task<BlobContainerClient> InitializeBlobContainerClient( string containerName)
        {
            BlobContainerClient containerClient;
            //try
            //{
            //containerClient = await this.blobServiceClient.CreateBlobContainerAsync(containerName);
            containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
            //}
            //catch (Exception ex)
            //{
            //    this.blobServiceClient.get
            //    this.blobServiceClient.GetBlobContainersAsync()


            //}

            return containerClient;
        }

        public async Task<CloudBlobContainer> CreateSampleContainerAsync(CloudBlobClient blobClient)
        {
            // Name the sample container based on new GUID, to ensure uniqueness.
            // The container name must be lowercase.
            string containerName = "container-" + Guid.NewGuid();

            // Get a reference to a sample container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            try
            {
                // Create the container if it does not already exist.
                bool result = await container.CreateIfNotExistsAsync();
                if (result == true)
                {
                    Console.WriteLine("Created container {0}", container.Name);
                }
            }
            catch (StorageException e)
            {
                Console.WriteLine("HTTP error code {0}: {1}",
                                    e.RequestInformation.HttpStatusCode,
                                    e.RequestInformation.ErrorCode);
                Console.WriteLine(e.Message);
            }

            return container;
        }
    }
}
