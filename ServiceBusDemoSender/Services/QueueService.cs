using Azure.Messaging.ServiceBus;
using System.Text;
using System.Text.Json;

namespace ServiceBusDemoSender.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration configuration;

        public QueueService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendMessageAsync<T>(T message, string queueName)
        {
            var serviceBusClient = new ServiceBusClient(configuration.GetConnectionString("AzureServiceBus"));
            ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(queueName);

            string serializedMessageBody = JsonSerializer.Serialize(message);
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(serializedMessageBody));
            await serviceBusSender.SendMessageAsync(serviceBusMessage);





        }
    }
}
