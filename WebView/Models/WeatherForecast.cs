using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Models
{
    public class WeatherForecast
    {
        [JsonProperty(PropertyName = "Date")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "TemperatureC")]
        public int TemperatureC { get; set; }
        [JsonProperty(PropertyName = "TemperatureF")]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        [JsonProperty(PropertyName = "Summary")]
        public string Summary { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
    }
}
