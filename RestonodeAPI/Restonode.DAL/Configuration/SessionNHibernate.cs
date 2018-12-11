using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Restonode.DAL.Mappings;

namespace Restonode.DAL.Configuration
{
    public static class SessionNHibernate
    {
        private static ISessionFactory sessionFactory;

        public static ISession Session { get; private set; }

        static SessionNHibernate()
        {
            Session = null;
        }

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                    sessionFactory = CreateSessionFactory();

                return sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            Session = SessionFactory.OpenSession();

            return Session;
        }

        public static void CloseSession()
        {
            Session.Close();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            try
            {
                return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString("Data source=.\\SQLExpress;initial catalog=dbo.Restonode;integrated security=True;MultipleActiveResultSets=true;").AdoNetBatchSize(100))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomerMap>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<RestaurantMap>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MealMap>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MealPerRestaurantMap>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ScoreMap>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<OrderStateMap>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomerOrderMap>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<OrderDetailMap>())
                    .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
    
            return null;
        }
    }
}