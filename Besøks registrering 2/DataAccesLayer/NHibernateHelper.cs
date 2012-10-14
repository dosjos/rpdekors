using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Visitor_Registration.DataAccesLayer;
using DomainObjects.Visit;
using Visitor_Registration.DomainObjects;
using DomainObjects.Settings;
using System;

namespace Visitor_Registration
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
                .Database(MsSqlConfiguration.MsSql2012
                              .ConnectionString(
                              (c => c
                                .Server(CustomizationManager.GetServer())
                                .TrustedConnection()
                                .Database(CustomizationManager.GetDatabase())
                                .Username("rodekors")
                                .Password("rodekors")))
                //    @"Server=localhost\SQLExpress;Database=VisitDatabase;Trusted_Connection=True;Uid=rodekors;")
                              .ShowSql()
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Visit>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Kid>())
                .Mappings(m =>m.FluentMappings.AddFromAssemblyOf<GenericVisitor>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Settings>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                               .Create(true,true))
                .BuildSessionFactory();
        }


        private static void InitializeSessionFactory()
        {
            try
            {
                _sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                                  .ConnectionString(
                                  (c => c
                                    .Server(CustomizationManager.GetServer())
                                    .TrustedConnection()
                                    .Database(CustomizationManager.GetDatabase())
                                    .Username("rodekors")
                                    .Password("rodekors")))
                    //    @"Server=localhost\SQLExpress;Database=VisitDatabase;Trusted_Connection=True;Uid=rodekors;")
                                  .ShowSql()
                    )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Visit>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Kid>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<GenericVisitor>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Settings>())
                    //.ExposeConfiguration(cfg => new SchemaExport(cfg)
                    //                               .Create(true,true))
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

/* QUERY
SELECT TOP 1000 [Id]
      ,[VisitTime]
      ,[KidId]
  FROM [VisitDatabase].[dbo].[Visit]
  WHERE [VisitTime] >= DATEADD(DAY, DATEDIFF(DAY, '19000101', GETDATE()), '19000101')
	AND [VisitTime] < DATEADD(DAY, DATEDIFF(DAY, '18991231', GETDATE()), '19000101')

*/