using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebView
{
    public class APICaller
    {
        private static string _baseUrl;

        public APICaller(IConfiguration configuration)
        {
            _baseUrl=configuration.GetValue<string>("WebAPIBaseUrl");
        }
        public static TResponse Get<TResponse>(string url)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler, true))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                SetToken(client);
                client.BaseAddress = new Uri(_baseUrl);
                client.Timeout = TimeSpan.FromSeconds(2400);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode == false)
                {
                    //To Enable disable logs 
                    //if (string.Compare(logEnable, "Yes", true) == 0)
                    //{
                    //    var data = response.Content.ReadAsAsync<string>().Result;
                    //    Utility.LogError(data);
                    //}
                    return default(TResponse);
                }
                else
                {
                    // JsonConvert.DeserializeObject<List<RootObject>>(content)
                    var data = response.Content.ReadFromJsonAsync<TResponse>().Result;
                    return data;
                }
            }
        }

        public static IEnumerable<TResponse> GetMany<TResponse>(string url)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler, true))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                SetToken(client);
                client.BaseAddress = new Uri(_baseUrl);
                client.Timeout = TimeSpan.FromSeconds(2400);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode == false)
                {
                    //To Enable disable logs 
                    //if (string.Compare(logEnable, "Yes", true) == 0)
                    //{
                    //    var data = response.Content.ReadAsAsync<string>().Result;
                    //    Utility.LogError(data);
                    //}
                    return null;
                }
                else
                {
                    // JsonConvert.DeserializeObject<List<RootObject>>(content)
                    var data = response.Content.ReadFromJsonAsync<IEnumerable<TResponse>>().Result;
                    return data;
                }
            }

        }

        public static HttpResponseMessage Post<TRequest>(string url, TRequest content) where TRequest : class
        {
            HttpResponseMessage response = null;

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler, true))
            {
                client.Timeout = TimeSpan.FromSeconds(2400);
                SetToken(client);
                client.BaseAddress = new Uri(_baseUrl);

                response = client.PostAsJsonAsync<TRequest>(url, content).Result;

                return response;
                //if (response.IsSuccessStatusCode)
                //{

                //    return response.Content.ReadFromJsonAsync<TResponse>().Result;//ReadAsAsync<List<TResponse>>().Result; ;

                //}
                //else
                //{
                //    //To Enable disable logs 
                //    //if (string.Compare(logEnable, "Yes", true) == 0)
                //    //{
                //    //    var data = response.Content.ReadAsAsync<string>().Result;
                //    //    Utility.LogError(data);
                //    //}
                //    return default(TResponse);
                //}

            }

        }

        public static IEnumerable<TResponse> PostL<TResponse, TRequest>(string url, TRequest content) where TRequest : class
        {
            HttpResponseMessage response = null;

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler, true))
            {
                client.Timeout = TimeSpan.FromSeconds(2400);
                SetToken(client);
                client.BaseAddress = new Uri(_baseUrl);
                
                response = client.PostAsJsonAsync<TRequest>(url, content).Result;

                if (response.IsSuccessStatusCode)
                {

                    return response.Content.ReadFromJsonAsync<IEnumerable<TResponse>>().Result;//ReadAsAsync<List<TResponse>>().Result; ;

                }
                else
                {
                    //To Enable disable logs 
                    //if (string.Compare(logEnable, "Yes", true) == 0)
                    //{
                    //    var data = response.Content.ReadAsAsync<string>().Result;
                    //    Utility.LogError(data);
                    //}
                    return null;
                }

            }
            
        }
      
        private static void SetToken(HttpClient client)
        {

            //client.DefaultRequestHeaders.Add("APIKey", apiKey);
        }
        public static HttpResponseMessage Put<TRequest>(string url, TRequest content) where TRequest : class
        {
            HttpResponseMessage response = null;

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler, true))
            {
                client.Timeout = TimeSpan.FromSeconds(2400);
                SetToken(client);
                client.BaseAddress = new Uri(_baseUrl);

                response = client.PutAsJsonAsync<TRequest>(url, content).Result;
                return response;
                //if (response.IsSuccessStatusCode)
                //{

                //    return response.Content.ReadFromJsonAsync<TResponse>().Result;//ReadAsAsync<List<TResponse>>().Result; ;

                //}
                //else
                //{
                //    //To Enable disable logs 
                //    //if (string.Compare(logEnable, "Yes", true) == 0)
                //    //{
                //    //    var data = response.Content.ReadAsAsync<string>().Result;
                //    //    Utility.LogError(data);
                //    //}
                //    return default(TResponse);
                //}

            }

        }

    }


}
