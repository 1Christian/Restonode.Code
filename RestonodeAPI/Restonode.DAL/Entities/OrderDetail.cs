using System;

namespace Restonode.DAL.Entities
{
    [Serializable]
    public class OrderDetail
    {
        public virtual int OrderID { get; set; }

        public virtual CustomerOrder Order { set; get; }

        public virtual int MealID { get; set; }

        public virtual Meal Meal { set; get; }

        public virtual decimal Cost { set; get; }

        public virtual int Quantity { set; get; }
    }
}