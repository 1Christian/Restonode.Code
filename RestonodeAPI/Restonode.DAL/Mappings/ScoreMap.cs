using FluentNHibernate.Mapping;
using Restonode.DAL.Entities;

namespace Restonode.DAL.Mappings
{
    public class ScoreMap : ClassMap<Score>
    {
        public ScoreMap()
        {
            Table("Score");
            Id(x => x.RestaurantID);
            Id(x => x.CustomerID);
            Map(x => x.RatingScore);
            Map(x => x.RatedDate);
        }
    }
}