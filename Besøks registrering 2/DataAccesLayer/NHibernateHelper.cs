﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Visitor_Registration.DomainObjects;
using DomainObjects.Visit;

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

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(
                                  @"Server=localhost\SQLExpress;Database=VisitDatabase;Trusted_Connection=True;")
                              .ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Visit>())
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Kid>())

                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(true,true))
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}