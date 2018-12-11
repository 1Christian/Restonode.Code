using Restonode.Common.Interfaces;

namespace Restonode.Common.Settings
{
    public class SwaggerSettings : ISettings
    {
        public string DocVersion { set; get; }

        public string Title { set; get; }

        public string Description { set; get; }

        public string TermOfService { set; get; }

        public string SwaggerEndpoint { set; get; }

        public string RouteTemplate { set; get; }
    }
}