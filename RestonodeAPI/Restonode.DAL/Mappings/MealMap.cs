using FluentNHibernate.Mapping;
using Restonode.DAL.Entities;

namespace Restonode.DAL.Mappings
{
    public class MealMap : ClassMap<Meal>
    {
        public MealMap()
        {
            Table("Meal");
            Id(x => x.MealID);
            Map(x => x.Cost);
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}