using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ServiceBusDemoShared.Models;

namespace AzureFunctionTestQueue
{
    public class MyFunction
    {
        private readonly ILogger<MyFunction> _logger;

        public MyFunction(ILogger<MyFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(MyFunction))]
        public void Run([ServiceBusTrigger("personqueue", Connection = "AzureServiceBus")] ServiceBusReceivedMessage message)
        {

            PersonModel person = JsonSerializer.Deserialize<PersonModel>(message.Body);
            _logger.LogInformation("The person here is {var} {var2}",person.FirstName,person.LastName);
            
        }
    }
}
