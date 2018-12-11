using System;

namespace Restonode.DAL.Entities
{
    [Serializable]
    public class OrderState
    {
        public virtual int StateID { set; get; }

        public virtual string Description { set; get; }
    }
}