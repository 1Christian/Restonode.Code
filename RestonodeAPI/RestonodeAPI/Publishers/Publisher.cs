using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Restonode.API.Interfaces;

namespace Restonode.API.Publishers
{
    public class Publisher : IPublisher
    {
        protected readonly ConnectionFactory _factory;
        protected readonly IOptions<Common.Settings.RabbitMQSettings> _settings;

        public Publisher(ConnectionFactory factory, IOptions<Common.Settings.RabbitMQSettings> settings)
        {
            _factory = factory;
            _settings = settings;
        }

        public async Task<bool> PublishAsync(object message)
        {
            return await Task.Run(() =>
            {
                _factory.HostName = _settings.Value.HostName;

                using (var connection = _factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    var properties = channel.CreateBasicProperties();

                    properties.Persistent = true;

                    channel.ExchangeDeclare(_settings.Value.ExchangeName, _settings.Value.ExchangeType);

                    var messageConverted = GetByteArrayFromObjectAsync(message);

                    foreach (var queue in _settings.Value.Queues)
                    {
                        channel.QueueDeclare(queue.Name, true, false, false, null);

                        channel.QueueBind(queue.Name, _settings.Value.ExchangeName, string.Empty);
                    }

                    channel.BasicPublish(_settings.Value.ExchangeName, string.Empty, properties, messageConverted.Result);
                }

                return true;
            });
        }

        private async Task<byte[]> GetByteArrayFromObjectAsync(object objToConvert)
        {
            return await Task.Run(() =>
            {
                if (objToConvert == null) return null;

                var formatter = new BinaryFormatter();

                using (var stream = new MemoryStream())
                {
                    formatter.Serialize(stream, objToConvert);
                    return stream.ToArray();
                }
            });
            
        }
    }
}