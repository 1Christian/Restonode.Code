using RabbitMQ.Client;
using Restonode.Common.Enums;
using Restonode.Common.Interfaces;
using Restonode.Common.Notifiers;
using Restonode.Common.Settings;
using Restonode.Messaging.Consumers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restonode.Messaging.Factory
{
    public class ConsumerFactory : IDisposable
    {
        private readonly IModel _channel;

        private bool disposed = false;

        public ConsumerFactory(IModel channel)
        {
            _channel = channel;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            disposed = true;
        }

        public IConsumer GetConsumer(ConsumerType type, MailSettings settings = null)
        {
            switch (type)
            {
                case ConsumerType.OrderConsumer:
                    {
                        var notifier = new MailNotifier(settings);

                        return (IConsumer)new OrderMessageConsumer(_channel, notifier);
                    }
                case ConsumerType.NotificationConsumer:
                    return (IConsumer)new NotificationMessageConsumer(_channel, new SmsNotifier());
                default:
                    throw new NotSupportedException();
            }
        }
    }
}