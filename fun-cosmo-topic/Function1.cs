using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace fun_cosmo_topic
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([CosmosDBTrigger(
            databaseName: "weather",
            collectionName: "items",
            ConnectionStringSetting = "AccountEndpoint=https://skcosmos81.documents.azure.com:443/;AccountKey=TKbie67wTyoHQp9crNiTTQwJboBfemoREAOVUGoNpDBOZJzaUSop7XPiFBr16Ih7e3uQEXEqIDISQcwmsaByDA==;",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
