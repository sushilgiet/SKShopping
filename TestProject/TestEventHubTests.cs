using System;
using TestProject;
using Xunit;
using AzureEventHub;
using System.Collections.Generic;

namespace TestProject
{
    public class TestEventHubTests
    {
        [Fact]
        public void Test1_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string connectionString = "Endpoint=sb://skeventhub1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=JvUCiIqwZkyMHYD9yp9INXqjXygQ79EaE9fyb9Q/GeE=";
            string eventHubName = "eventhub2";
            var eventHub = new AzureEventHub.SendEvent(connectionString, eventHubName);

            // Act
            List<HubData> hubData = new List<HubData>();
            for(int j = 0; j < 100; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    hubData.Add(new HubData { Id = i.ToString() + j.ToString(), Name = "Name" + i.ToString() + j.ToString() });
                   
                }

              eventHub.SendDataAsync(hubData).GetAwaiter().GetResult();
            }

            // Assert
            Assert.True(true);
        }

        class HubData
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
