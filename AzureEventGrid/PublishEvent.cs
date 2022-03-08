using Azure.Core.Serialization;
using Azure.Messaging.EventGrid;
using AzureEventGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventHubSamples
{
    public interface IPublishEvent
    {
        Task<Azure.Response> PublishEventsAsync<T>(T data);
    }

    public class PublishEvent : IPublishEvent
    {
     
        IEventGridClient _eventGridClient;
        
        public PublishEvent(IEventGridClient client)
        {
            _eventGridClient = client;
        }

        public async Task<Azure.Response> PublishEventsAsync<T>(T data)
        {

            var myCustomDataSerializer = new JsonObjectSerializer(
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            List<EventGridEvent> eventsList = new List<EventGridEvent>
        {

             
             new EventGridEvent("ExampleEventSubject","Example.EventType","1.0",myCustomDataSerializer.Serialize(data)),
         };
           var clientt = _eventGridClient.GetPublisherClientUsingSharedAccessKey();
           return await clientt.SendEventsAsync(eventsList);
        }

        private void GetPublishedEvents(EventGridEvent[] events)
        {
            //foreach (var ev in events)
            //{
            //    if (ev.EventType == )
            //}
        }
    }
}
