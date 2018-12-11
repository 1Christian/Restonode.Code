using System;
using System.Collections.Generic;

namespace Restonode.DAL.Entities
{
    [Serializable]
    public class Restaurant
    {
        public Restaurant()
        {
            Orders = new List<CustomerOrder>();
        }

        public virtual int RestaurantID { set; get; }

        public virtual string Name { set; get; }

        public virtual string Address { set; get; }

        public virtual double Latitude { set; get; }

        public virtual double Longitude { set; get; }

        public virtual string Mail { set; get; }

        public virtual string Phone { set; get; }

        public virtual IEnumerable<CustomerOrder> Orders { set; get; }

        public virtual IEnumerable<Score> Scores { set; get; }

        public virtual IEnumerable<Meal> Meals { set; get; }
    }
}