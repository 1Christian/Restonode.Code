using System;
using System.Threading.Tasks;
using Restonode.Common.Interfaces;

namespace Restonode.Common.Notifiers
{
    public class SmsNotifier : ISmsNotifier
    {
        public async Task<object> NotifyAsync(byte[] message)
        {
            return await Task.Run(() =>
            {
                var sms = "SMS sent > New order was received";

                Console.WriteLine(sms);

                return sms;
            });
        }
    }
}