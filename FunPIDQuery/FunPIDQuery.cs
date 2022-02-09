using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventGrid.SystemEvents;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace FunPIDQuery
{
    public static class FunPIDQuery
    {
        [FunctionName("FunPIDQuery")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string response = string.Empty;

            using (var reader = new StreamReader(req.Body, Encoding.UTF8))
            {
                var jsonContent = reader.ReadToEndAsync().Result;
                var events = JArray.Parse(jsonContent);
               
                foreach(var ev in events)
                {
                    var gridEvent = JsonConvert.DeserializeObject<GridEvent<Dictionary<string, string>>>(ev.ToString());
                    if (gridEvent.EventType.Contains("Validation"))
                    {
                        
                        log.LogInformation("EventTypeSubcriptionValidation");
                        var validationCode = gridEvent.Data["validationCode"];
                        return new JsonResult(new
                        {
                            validationResponse = validationCode
                        });
                    }
                    else
                    {
                        try
                        {
                            var details = gridEvent.Data;
                        }
                        catch (Exception ex) {
                            log.LogInformation(ex.StackTrace);

                        }
                        
                        
                    }
                }


                 
            }
            //BinaryData events = await BinaryData.FromStreamAsync(req.Body);
            
            //log.LogInformation($"Received events: {events}");
            //try
            //{
            //  eventGridEvents = EventGridEvent.ParseMany(events);

            //}
            //catch (Exception ex) {
            //    log.LogError($"Parse Error: {ex.Message}");
            
            //}
            

            //foreach (EventGridEvent eventGridEvent in eventGridEvents)
            //{
            //    // Handle system events
            //    if (eventGridEvent.TryGetSystemEventData(out object eventData))
            //    {
            //        // Handle the subscription validation event
            //        if (eventData is SubscriptionValidationEventData subscriptionValidationEventData)
            //        {
            //            log.LogInformation($"Got SubscriptionValidation event data, validation code: {subscriptionValidationEventData.ValidationCode}, topic: {eventGridEvent.Topic}");
            //            // Do any additional validation (as required) and then return back the below response

            //            var responseData = new SubscriptionValidationResponse()
            //            {
            //                ValidationResponse = subscriptionValidationEventData.ValidationCode
            //            };
            //            return new OkObjectResult(responseData);
            //        }
            //    }
            //}
           
            return new OkObjectResult(response);
        }
        private static JsonResult HandleValidation(string json)
        {


            var gridEvent =
                  JsonConvert.DeserializeObject<List<GridEvent<Dictionary<string, string>>>>(json)[0];
            var validationCode = gridEvent.Data["validationCode"];
            return new JsonResult(new
            {
                validationResponse = validationCode
            });
        }
    }

}

