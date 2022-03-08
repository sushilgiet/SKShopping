using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB
{
   public class CosmoDBClient
    {
        public static async Task<CosmosClient> InitializeCosmosClientInstanceAsync(string databaseName, string containerName, string accountEndpoint, string key)
        {
           
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(accountEndpoint, key);
            //Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            //await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            return client;
        }
    }
}
