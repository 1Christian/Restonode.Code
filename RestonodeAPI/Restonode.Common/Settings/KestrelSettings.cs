using Restonode.Common.Interfaces;

namespace Restonode.Common.Settings
{
    public class KestrelSettings : ISettings
    {
        public int MaxConcurrentConnections { set; get; }

        public int MaxConcurrentUpgradedConnections { set; get; }

        public int MaxRequestBodySize { set; get; }

        public MinDataRate MinDataRate { set; get; }
    }

    public class MinDataRate
    {
        
        public int BytesPerSecond { set; get; }

        public int GracePeriod { set; get; }

        public int ListenPort { set; get; }
    }
}
