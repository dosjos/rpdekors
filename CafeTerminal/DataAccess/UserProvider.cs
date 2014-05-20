using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeTerminal.DataAccesLayer;
using DomainObjectsSalg.Sales;
using NHibernate;

namespace CafeTerminal.DataAccess
{
    public class UserProvider
    {
        internal static int SaveUser(DomainObjectsSalg.Sales.Users u)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(u);
                    transaction.Commit();
                    return u.Id;
                }
            }
        }

        internal static void SaveUsage(DomainObjectsSalg.Sales.UserLogg ul)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(ul);
                    transaction.Commit();
                }
            }
        }

        internal static List<DomainObjectsSalg.Sales.Users> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var list = session.CreateQuery("from Users where slettet != 1").List();

                    List<DomainObjectsSalg.Sales.Users> l = new List<DomainObjectsSalg.Sales.Users>();

                    foreach (DomainObjectsSalg.Sales.Users item in list)
                    {
                        l.Add(item);
                    }
                    return l;
                }
            }
        }

        internal static Users GetUser(int t)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var u = session.CreateQuery("from Users where id = :id").SetParameter("id",t).UniqueResult();
                    return (Users)u;
                }
            }
        }

        internal static void SaveLog(UserLogg ul)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(ul);
                    transaction.Commit();
                }
            }
        
        }

        internal static List<UserLogg> GetTodayUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from UserLogg where DatePart(YEAR, Brukstid) = :year  and DatePart(MONTH, Brukstid) = :month AND DatePart(DAY, Brukstid) = :day")
                        .SetParameter("year", DateTime.Now.Year)
                        .SetParameter("month", DateTime.Now.Month)
                        .SetParameter("day", DateTime.Now.Day)
                        .List<UserLogg>();
                    return res.ToList<UserLogg>();
                }
            }
        }
    }
}
