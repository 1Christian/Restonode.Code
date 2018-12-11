using System;
using System.Collections.Generic;

namespace Restonode.DAL.Entities
{
    [Serializable]
    public class Meal
    {
        public virtual int MealID { set; get; }

        public virtual decimal Cost { set; get; }

        public virtual string Name { set; get; }

        public virtual string Description { set; get; }

        public virtual IList<CustomerOrder> Orders { get; set; }

        public virtual IList<Restaurant> Restaurants { get; set; }
    }
}