using Azure.Storage.Blobs;
using System;

namespace AzureStorage
{
    public class StorageBlobClient
    {
        public BlobServiceClient GetBlobClientByStorageConnString(string storageConnectionString)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);
            return blobServiceClient;
        }
    }
}
