using Restonode.Common.Interfaces;

namespace Restonode.Common.Settings
{
    public class AppSettings : ISettings
    {
        public string ConnectionString { get; set; }
    }
}