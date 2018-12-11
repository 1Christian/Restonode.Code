using Restonode.Common.Interfaces;

namespace Restonode.Common.Settings
{
    public class MvcSettings : ISettings
    {
        public string MapRouteName { set; get; }

        public string MapRouteTemplate { set; get; }
    }
}