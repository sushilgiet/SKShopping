
using EventHubSamples.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EventHubSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventGridController : ControllerBase
    {
        IPublishEvent _publisher;
        IConfiguration _configuration;
        ILogger<EventGridController> _logger;
        public EventGridController(IPublishEvent pub, IConfiguration config, ILogger<EventGridController> logger)
        {
            _publisher = pub;
            _configuration = config;
            _logger = logger;
        }
        [HttpGet]
        [Route("PublishEvents")]
        public IActionResult Get()
        {
            _publisher.PublishEventsAsync().GetAwaiter().GetResult();
            return Ok();
        }
        //[HttpPost]
        //[Route("PostEvents")]
        //public IActionResult Post()
        //{

        //    return Ok("jhj");
        //}



        private bool EventTypeSubcriptionValidation
            => HttpContext.Request.Headers["aeg-event-type"].FirstOrDefault() ==
               "SubscriptionValidation";

        private bool EventTypeNotification
            => HttpContext.Request.Headers["aeg-event-type"].FirstOrDefault() ==
               "Notification";

        #region Public Methods


        [HttpPost]
        [Route("PostEvents")]
        public IActionResult Post()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var jsonContent = reader.ReadToEndAsync().Result;
                _logger.LogInformation(jsonContent);
                // Check the event type.
                // Return the validation code if it's 
                // a subscription validation request. 
                if (EventTypeSubcriptionValidation)
                {
                    _logger.LogInformation("EventTypeSubcriptionValidation");
                    return HandleValidation(jsonContent);
                }
                else if (EventTypeNotification)
                {
                    // Check to see if this is passed in using
                    // the CloudEvents schema
                    if (IsCloudEvent(jsonContent))
                    {
                        _logger.LogInformation("CloudEvent");
                        return  HandleCloudEvent(jsonContent);
                    }
                    _logger.LogInformation("EventTypeNotification");
                    return HandleGridEvents(jsonContent);
                }

                return BadRequest();
            }
        }

        #endregion

        #region Private Methods

        private JsonResult HandleValidation(string jsonContent)
        {
            var gridEvent =
                JsonConvert.DeserializeObject<List<GridEvent<Dictionary<string, string>>>>(jsonContent)
                    .First();

            //await this._hubContext.Clients.All.SendAsync(
            //    "gridupdate",
            //    gridEvent.Id,
            //    gridEvent.EventType,
            //    gridEvent.Subject,
            //    gridEvent.EventTime.ToLongTimeString(),
            //    jsonContent.ToString());

            // Retrieve the validation code and echo back.
            var validationCode = gridEvent.Data["validationCode"];
            return new JsonResult(new
            {
                validationResponse = validationCode
            });
        }

        IActionResult HandleGridEvents(string jsonContent)
        {
            var events = JArray.Parse(jsonContent);
            foreach (var e in events)
            {
                // Invoke a method on the clients for 
                // an event grid notiification.                        
                var details = JsonConvert.DeserializeObject<GridEvent<dynamic>>(e.ToString());
                //    await this._hubContext.Clients.All.SendAsync(
                //        "gridupdate",
                //        details.Id,
                //        details.EventType,
                //        details.Subject,
                //        details.EventTime.ToLongTimeString(),
                //        e.ToString());
                //}

                return Ok(details);
            }
            return BadRequest();
        }
            [NonAction]
            IActionResult HandleCloudEvent(string jsonContent)
            {
                var details = JsonConvert.DeserializeObject<CloudEvent<dynamic>>(jsonContent);
                var eventData = JObject.Parse(jsonContent);

                //await this._hubContext.Clients.All.SendAsync(
                //    "gridupdate",
                //    details.Id,
                //    details.Type,
                //    details.Subject,
                //    details.Time,
                //    eventData.ToString()
                //);

                return Ok(details);
            }
            [NonAction]
            static bool IsCloudEvent(string jsonContent)
            {
                // Cloud events are sent one at a time, while Grid events
                // are sent in an array. As a result, the JObject.Parse will 
                // fail for Grid events. 
                try
                {
                    // Attempt to read one JSON object. 
                    var eventData = JObject.Parse(jsonContent);

                    // Check for the spec version property.
                    var version = eventData["specversion"].Value<string>();
                    if (!string.IsNullOrEmpty(version)) return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return false;
            }
            #endregion
        }
    }


