using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Restonode.Messaging.Factory;

namespace Restonode.Messaging
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var rabbitMqSetting = new Restonode.Common.Settings.RabbitMQSettings();
            var mailSettings = new Restonode.Common.Settings.MailSettings();

            configuration.GetSection("RabbitMQ").Bind(rabbitMqSetting);
            configuration.GetSection("Mail").Bind(mailSettings);

            var factory = new ConnectionFactory { HostName = rabbitMqSetting.HostName };

            Console.WriteLine("Starting Messaging Service...\n");

            ConsoleKeyInfo cki;

            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs ea) =>
            {
                Console.WriteLine("\nThe read operation has been interrupted.\n");

                Console.WriteLine($"Key pressed: {ea.SpecialKey}\n");

                ea.Cancel = true;

                Console.WriteLine("Press any key to resume...\n");
            };

            while (true)
            {
                Console.WriteLine("Press 'X' to quit or CTRL+C to interrupt the read operation.\n");

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                using (var consumerFactory = new ConsumerFactory(channel))
                {
                    channel.BasicQos(0, 1, false);

                    foreach (var queue in rabbitMqSetting.Queues)
                    {
                        channel.QueueDeclare(queue.Name, true, false, false, null);

                        channel.BasicConsume(queue.Name, false, consumerFactory.GetConsumer(queue.Type, mailSettings));
                    }

                    cki = Console.ReadKey(true);

                    if (cki.Key == ConsoleKey.X) break;
                }
            }
        }
    }
}