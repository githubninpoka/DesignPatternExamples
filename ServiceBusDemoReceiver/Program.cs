using Azure.Messaging.ServiceBus;
using ServiceBusDemoShared.Models;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace ServiceBusDemoReceiver;

// rewrite to .net9 from a video by Tim Corey
// https://www.youtube.com/watch?v=v52yC9kq0Yg&t=1617s

internal class Program
{
    const string connectionstring = "because this was a simple console application i just pasted the connection string here";
    const string queueName = "personqueue";
    static ServiceBusClient serviceBusClient;
    static ServiceBusReceiver serviceBusReceiver;

    static async Task Main(string[] args)
    {
        serviceBusClient = new(connectionstring);
        serviceBusReceiver = serviceBusClient.CreateReceiver(queueName);

        
        var messageHandlerOptions = new ServiceBusProcessorOptions()
        {
            MaxConcurrentCalls = 1,
            AutoCompleteMessages = false
        };
        var processor = serviceBusClient.CreateProcessor(queueName, messageHandlerOptions);

        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync();
        Console.WriteLine("what ya waiting for");
        Console.ReadLine();
        await processor.CloseAsync();
        
    }

    private static async Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine("Something went horribly wrong");
        Console.WriteLine("The error is: " + args.Exception.Message);
    }

    private static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        Console.WriteLine(args.Message.Body);
        var jsonString = Encoding.UTF8.GetString(args.Message.Body);
        var person = JsonSerializer.Deserialize<PersonModel>(jsonString);
        Console.WriteLine($"The person in this case is {person!.FirstName} {person.LastName}");
        
        // because AutoCompleteMessage == false
        await args.CompleteMessageAsync(args.Message);
    }

}
