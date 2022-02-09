using Azure.Core.Serialization;
using Azure.Messaging.EventGrid;
using EventHubSamples.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventHubSamples
{
    public interface IPublishEvent
    {
        Task<Azure.Response> PublishEventsAsync();
    }

    public class PublishEvent : IPublishEvent
    {
     
        IEventGridClient _eventGridClient;
        IConfiguration _configuration;
        public PublishEvent(IEventGridClient client, IConfiguration configuration)
        {
            _eventGridClient = client;
            _configuration = configuration;
        }

        public async Task<Azure.Response> PublishEventsAsync()
        {

            var myCustomDataSerializer = new JsonObjectSerializer(
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            List<EventGridEvent> eventsList = new List<EventGridEvent>
        {

             new EventGridEvent( "ExampleEventSubject","Example.EventType","1.0",new PIDData { PID = 5}),
             new EventGridEvent("ExampleEventSubject","Example.EventType","1.0",myCustomDataSerializer.Serialize(new PIDData {PID = 15})),
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
