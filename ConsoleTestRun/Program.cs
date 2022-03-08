using System;
using System.Collections.Generic;

namespace ConsoleTestRun
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Endpoint=sb://skeventhub1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=JvUCiIqwZkyMHYD9yp9INXqjXygQ79EaE9fyb9Q/GeE=";
            string eventHubName = "eventhub2";
            var eventHub = new AzureEventHub.SendEvent(connectionString, eventHubName);

            // Act
            List<HubData> hubData = new List<HubData>();
            for (int j = 0; j < 100; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    hubData.Add(new HubData { Id = i.ToString() + j.ToString(), Name = "Name" + i.ToString() + j.ToString() });
                   
                }
                eventHub.SendDataAsync(hubData).GetAwaiter().GetResult();

            }

        }
        class HubData
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
