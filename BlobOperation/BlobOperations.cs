using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
namespace AzureStorage
{
   public class BlobOperations
    {
        public async Task<BlobContainerClient> CreateBlobContainerAsync(BlobServiceClient blobServiceClient, string containerName)
        {
           
            // Create the container and return a container client object
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            return containerClient;
        }

        public  BlobContainerClient GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            return containerClient;
        }
        public async Task UploadBlob(BlobContainerClient containerClient, string containerName,string blobLocalPath,string fileName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            using FileStream uploadFileStream = File.OpenRead(blobLocalPath);
            await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();
           
        }
        public async Task<List<BlobItem>> ListBlobsAsync(BlobContainerClient containerClient)
        {
            List<BlobItem> blobItems = new List<BlobItem>();
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                blobItems.Add(blobItem);
            }
            return blobItems;
        }
        public async Task DownLoadBlobAsync(BlobContainerClient containerClient, string downloadFilePath, string fileName)
        {

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using (FileStream downloadFileStream = File.OpenWrite(downloadFilePath))
            {
                await download.Content.CopyToAsync(downloadFileStream);
                downloadFileStream.Close();
            }
        }
        public BlobClient GetBlobClient(BlobContainerClient containerClient, string fileName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            return blobClient;
        }
        public async Task DeleteContainer(BlobContainerClient containerClient)
        {
           await containerClient.DeleteAsync();
        }
        public async Task DeleteBlob(BlobContainerClient containerClient,string blobName)
        {
            await containerClient.DeleteBlobIfExistsAsync(blobName);
        }
    }
}
