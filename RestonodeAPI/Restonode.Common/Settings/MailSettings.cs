using Restonode.Common.Interfaces;

namespace Restonode.Common.Settings
{
    public class MailSettings : ISettings
    {
        public string SmtpHost { set; get; }

        public int SmtpPort { set; get; }

        public bool UseDefaultCredentials { set; get; }

        public bool EnableSsl { set; get; }

        public string From { set; get; }

        public string Subject { set; get; }
    }
}