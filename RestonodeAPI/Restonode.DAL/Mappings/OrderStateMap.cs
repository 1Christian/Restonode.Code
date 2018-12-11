using FluentNHibernate.Mapping;
using Restonode.DAL.Entities;

namespace Restonode.DAL.Mappings
{
    public class OrderStateMap : ClassMap<OrderState>
    {
        public OrderStateMap()
        {
            Table("OrderState");
            Id(x => x.StateID);
            Map(x => x.Description);
        }
    }
}