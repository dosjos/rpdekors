using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using DomainObjecsSalg.Sales;
using DomainObjecsSalg.Settings;


namespace Visitor_Registration.DataAccesLayer
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();

                return _sessionFactory;
            }
        }

        public static void ResetDatabase()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(
                              (c => c
                                .Server(CustomizationManager.GetServer())
                                .TrustedConnection()
                                .Database(CustomizationManager.GetDatabase())
                                .Username("rodekors")
                                .Password("rodekors")))
                              .ShowSql()
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Vare>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Salg>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Logg>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Settings>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Users>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserLogg>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                               .Create(true, true))
                .BuildSessionFactory();
        }


        private static void InitializeSessionFactory()
        {
            try
            {
                _sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008
                                  .ConnectionString(
                                  (c => c
                                    .Server(CustomizationManager.GetServer())
                                    .TrustedConnection()
                                    .Database(CustomizationManager.GetDatabase())
                                    .Username("rodekors")
                                    .Password("rodekors")))
                      .ShowSql()//Uncomment denne for å få output
                    )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Vare>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Salg>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Logg>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Settings>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Users>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserLogg>())

                    .BuildSessionFactory();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
            }
        }

        public static ISession OpenSession()
        {
            try
            {
                return SessionFactory.OpenSession();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return null;
        }
    }
}
