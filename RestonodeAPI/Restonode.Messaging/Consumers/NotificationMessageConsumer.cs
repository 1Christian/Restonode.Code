using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Restonode.Common.Interfaces;

namespace Restonode.Messaging.Consumers
{
    public class NotificationMessageConsumer : EventingBasicConsumer, IConsumer
    {
        protected readonly ISmsNotifier _notifier;

        protected IModel _channel;

        public NotificationMessageConsumer(IModel channel, ISmsNotifier notifier) 
            : base(channel)
        {
            _notifier = notifier;
            _channel = channel;
            Received += NotificationMessageConsumer_Received;
        }

        private void NotificationMessageConsumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            _notifier.NotifyAsync(ea.Body);

            _channel.BasicAck(ea.DeliveryTag, false);                        
        }
    }
}