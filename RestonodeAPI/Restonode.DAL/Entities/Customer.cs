using System;
using System.Collections.Generic;

namespace Restonode.DAL.Entities
{
    [Serializable]
    public class Customer
    {
        public Customer()
        {
            Orders = new List<CustomerOrder>();
            Scores = new List<Score>();
        }

        public virtual int CustomerID { set; get; }

        public virtual string FirstName { set; get; }

        public virtual string LastName { set; get; }

        public virtual string Login { set; get; }

        public virtual string Password { set; get; }

        public virtual string PhoneNumber { set; get; }

        public virtual string Mail { set; get; }

        public virtual DateTime CreatedDate { set; get; }

        public virtual DateTime LastModifiedDate { set; get; }

        public virtual bool Active { set; get; }

        public virtual IList<CustomerOrder> Orders { set; get; }

        public virtual IList<Score> Scores { set; get; }
    }
}