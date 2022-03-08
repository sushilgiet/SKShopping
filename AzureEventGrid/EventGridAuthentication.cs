using Azure;
using Azure.Identity;
using Azure.Messaging.EventGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AzureEventGrid
{
    public interface IEventGridClient
    {
        EventGridPublisherClient GetPublisherClientUsingAD();
        EventGridPublisherClient GetPublisherClientUsingSas();
        EventGridPublisherClient GetPublisherClientUsingSharedAccessKey();
    }

    public class EventGridClient : IEventGridClient
    {
        private string _topicEndpoint;
        private string _topicAccessKey;

        public EventGridClient(string topicEndPoint,string key)
        {
            _topicEndpoint = topicEndPoint;
            _topicAccessKey = key;
        }

        public EventGridPublisherClient GetPublisherClientUsingSharedAccessKey()
        {
            EventGridPublisherClient client = new EventGridPublisherClient(
                new Uri(_topicEndpoint),
                 new AzureKeyCredential(_topicAccessKey));
            return client;
        }
        public EventGridPublisherClient GetPublisherClientUsingSas()
        {
            var builder = new EventGridSasBuilder(new Uri(_topicEndpoint), DateTimeOffset.Now.AddHours(1));
            var keyCredential = new AzureKeyCredential(_topicAccessKey);
            string sasToken = builder.GenerateSas(keyCredential);
            var sasCredential = new AzureSasCredential(sasToken);
            EventGridPublisherClient client = new EventGridPublisherClient(
                new Uri(_topicEndpoint),
                sasCredential);
            return client;
        }
        public EventGridPublisherClient GetPublisherClientUsingAD()
        {
            EventGridPublisherClient client = new EventGridPublisherClient(
              new Uri(_topicEndpoint),
              new DefaultAzureCredential());
            return client;
        }
    }
}
