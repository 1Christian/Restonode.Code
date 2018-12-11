using FluentNHibernate.Mapping;
using Restonode.DAL.Entities;

namespace Restonode.DAL.Mappings
{
    public class CustomerOrderMap : ClassMap<CustomerOrder>
    {
        public CustomerOrderMap()
        {
            Table("CustomerOrder");
            Id(x => x.OrderID);
            Map(x => x.TotalCost);
            Map(x => x.Address);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
            Map(x => x.Date);
            Map(x => x.RestaurantID);
            Map(x => x.StateID);
            Map(x => x.CustomerID);
        }
    }
}