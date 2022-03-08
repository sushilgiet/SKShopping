using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace fun_topicsubs
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("topic1", "Azure Pass - Sponsorship", Connection = "Endpoint=sb://skservicebus81.servicebus.windows.net/;SharedAccessKeyName=topiclistner;SharedAccessKey=G1c5td29Lr5N/N7dGm0ITXDTIJ3W8m85NBoJJ7un+C0=;EntityPath=topic1")]string mySbMsg, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
