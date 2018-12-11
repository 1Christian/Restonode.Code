using System;

namespace Restonode.DAL.Entities
{
    [Serializable]
    public class Score
    {
        public virtual int RestaurantID { set; get; }

        public virtual int CustomerID { set; get; }

        public virtual double RatingScore { set; get; }

        public virtual DateTime RatedDate { set; get; }

        public virtual Customer Customer { set; get; }

        public virtual Restaurant Restaurant { set; get; }
    }
}