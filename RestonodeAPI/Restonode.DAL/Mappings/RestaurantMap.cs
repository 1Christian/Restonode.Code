using FluentNHibernate.Mapping;
using Restonode.DAL.Entities;

namespace Restonode.DAL.Mappings
{
    public class RestaurantMap : ClassMap<Restaurant>
    {
        public RestaurantMap()
        {
            Table("Restaurant");
            Id(x => x.RestaurantID);
            Map(x => x.Name);
            Map(x => x.Address);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
            Map(x => x.Mail);
            Map(x => x.Phone);
        }
    }
}