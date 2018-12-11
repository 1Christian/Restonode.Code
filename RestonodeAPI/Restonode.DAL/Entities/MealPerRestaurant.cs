using System;

namespace Restonode.DAL.Entities
{
    [Serializable]
    public class MealPerRestaurant
    {
        public virtual int MealID { set; get; }

        public virtual Meal Meal { set; get; }

        public virtual int RestaurantID { set; get; }

        public virtual Restaurant Restaurant { set; get; }

        public virtual decimal UnitCost { set; get; }
    }
}