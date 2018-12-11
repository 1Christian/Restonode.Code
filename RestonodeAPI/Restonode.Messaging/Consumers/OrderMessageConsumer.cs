using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Restonode.Common.Interfaces;

namespace Restonode.Messaging.Consumers
{
    public class OrderMessageConsumer : EventingBasicConsumer, IConsumer
    {
        protected readonly ISmsNotifier _notifier;

        protected IModel _channel;

        public OrderMessageConsumer(IModel channel, ISmsNotifier notifier)
            : base(channel)
        {
            _notifier = notifier;
            _channel = channel;
            Received += OrderMessageConsumer_Received;
        }

        private async void OrderMessageConsumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            await _notifier.NotifyAsync(ea.Body);

            _channel.BasicAck(ea.DeliveryTag, false);
        }
    }
}