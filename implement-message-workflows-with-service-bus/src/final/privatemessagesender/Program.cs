using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace privatemessagesender
{
    class Program
    {
        const string ServiceBusConnectionString = "";
        const string QueueName = "salesmessages";
        static IQueueClient _queueClient;

        static void Main(string[] args)
        {
            Console.WriteLine("Sending a message to the Sales Messages queue...");

            SendSalesMessageAsync().GetAwaiter().GetResult();

            Console.WriteLine("Message was sent successfully.");
        }

        static async Task SendSalesMessageAsync()
        {
            _queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            // Send messages.
            try
            {
                string messageBody = $"$10,000 order for bicycle parts from retailer Adventure Works.";
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                // Write the body of the message to the console.
                Console.WriteLine($"Sending message: {messageBody}");

                // Send the message to the queue.
                await _queueClient.SendAsync(message);

            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }

            await _queueClient.CloseAsync();
        }
    }
}
