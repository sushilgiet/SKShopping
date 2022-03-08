using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Newtonsoft.Json;

namespace AzureEventHub
{
    public class SendEvent
    {
        private  string _connectionString = "<EVENT HUBS NAMESPACE - CONNECTION STRING>";
        private  string _eventHubName = "<EVENT HUB NAME>";
        EventHubProducerClient producerClient;
        public SendEvent(string connectionString,string eventHubName)
        {
            _connectionString = connectionString;
            _eventHubName = eventHubName;
         
        }
      
        public async Task SendDataAsync<T>(List<T> data)
        {
            producerClient = new EventHubProducerClient(_connectionString, _eventHubName);
            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
            for (int i = 0; i < data.Count; i++)
            {
               var json= JsonConvert.SerializeObject(data[i]);
                if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(json))))
                {
                    // if it is too large for the batch
                    throw new Exception($"Event {i} is too large for the batch and cannot be sent.");
                }
            }

            try
            {
                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
            }
            finally
            {
                await producerClient.DisposeAsync();
            }
        }

    }
}
