using Restonode.Common.Enums;
using Restonode.Common.Interfaces;
using System.Collections.Generic;

namespace Restonode.Common.Settings
{
    public class RabbitMQSettings : ISettings
    {
        public string HostName { get; set; }

        public string ExchangeName { get; set; }

        public string ExchangeType { get; set; }

        public IEnumerable<RabbitMqQueue> Queues { get; set; }
    }

    public class RabbitMqQueue
    {
        public string Name { get; set; }

        public ConsumerType Type { get; set; }
    }
}