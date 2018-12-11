using FluentNHibernate.Mapping;
using Restonode.DAL.Entities;

namespace Restonode.DAL.Mappings
{
    public class OrderDetailMap : ClassMap<OrderDetail>
    {
        public OrderDetailMap()
        {
            Table("OrderDetail");
            Id(x => x.OrderID);
            Id(x => x.MealID);
            Map(x => x.Cost);
            Map(x => x.Quantity);
        }
    }
}