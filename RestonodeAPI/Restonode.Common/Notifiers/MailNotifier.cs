using System;
using System.IO;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Restonode.Common.Interfaces;
using Restonode.Common.Settings;

namespace Restonode.Common.Notifiers
{
    public class MailNotifier : IMailNotifier
    {
        private readonly MailSettings _settings;

        public MailNotifier(MailSettings settings)
        {
            _settings = settings;
        }

        public async Task<object> NotifyAsync(byte[] message)
        {
            return await Task.Run(async () => {
                using (var mail = new MailMessage())
                using (var smtp = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort))
                {
                    smtp.UseDefaultCredentials = _settings.UseDefaultCredentials;

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.EnableSsl = _settings.EnableSsl;

                    mail.From = new MailAddress(_settings.From);

                    mail.To.Add(new MailAddress("ctorres@coasa.com.ar"));

                    mail.Subject = _settings.Subject;

                    mail.Body = (Deserialize<IMessage>(message)).Body;

                    await smtp.SendMailAsync(mail);

                    Console.WriteLine("Mail sent > New order was received.");

                    return mail;
                }
            });            
        }

        private T Deserialize<T>(byte[] param)
        {
            using (MemoryStream ms = new MemoryStream(param))
            {
                IFormatter br = new BinaryFormatter();

                return (T)br.Deserialize(ms);
            }
        }
    }
}