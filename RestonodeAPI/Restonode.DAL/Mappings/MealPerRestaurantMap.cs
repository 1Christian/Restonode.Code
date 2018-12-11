using FluentNHibernate.Mapping;
using Restonode.DAL.Entities;

namespace Restonode.DAL.Mappings
{
    public class MealPerRestaurantMap : ClassMap<MealPerRestaurant>
    {
        public MealPerRestaurantMap()
        {
            Table("MealPerRestaurant");
            Id(x => x.RestaurantID);
            Id(x => x.MealID);
            Map(x => x.UnitCost);
        }
    }
}