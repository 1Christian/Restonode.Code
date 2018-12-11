using System;
using System.Collections.Generic;

namespace Restonode.DAL.Entities
{
    [Serializable]
    public class CustomerOrder
    {
        public virtual int OrderID { set; get; }

        public virtual string Address { set; get; }

        public virtual double Latitude { set; get; }

        public virtual double Longitude { set; get; }

        public virtual decimal TotalCost { set; get; }

        public virtual DateTime Date { set; get; }

        public virtual int CustomerID { get; set; }

        public virtual int RestaurantID { get; set; }

        public virtual int StateID { get; set; }

        public virtual Restaurant Restaurant { set; get; }

        public virtual Customer Customer { set; get; }

        public virtual OrderState State { get; set; }

        public virtual IList<Meal> Meals { get; set; }
    }
}