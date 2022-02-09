using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace EventHubSamples.Models
{
    public class GridEvent<T> where T : class
    {
        public string Id { get; set; }
        public string EventType { get; set; }
        public string Subject { get; set; }
        public string EventTime { get; set; }
        public T Data { get; set; }
        public string Topic { get; set; }
       
    }
    public class CloudEvent<T> where T : class
    {
        [JsonProperty("specversion")]
        public string SpecVersion { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
    public class PIDData
    {
        public int PID { get; set; }
    }
}
