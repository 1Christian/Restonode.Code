using FluentNHibernate.Mapping;
using Restonode.DAL.Entities;

namespace Restonode.DAL.Mappings
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table("Customer");
            Id(x => x.CustomerID);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Login);
            Map(x => x.Password);
            Map(x => x.PhoneNumber);
            Map(x => x.Mail);
            Map(x => x.CreatedDate);
            Map(x => x.LastModifiedDate);
            Map(x => x.Active);
        }
    }
}