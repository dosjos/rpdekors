using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeTerminal.DataAccess;
//using DomainObjecsSalg.Sales;
using DomainObjecsSalg2.Sales;
using NHibernate;

namespace CafeRegnskap.DataAccess
{
    public class UserProvider
    {
        internal static int SaveUser(DomainObjecsSalg2.Sales.Users u)
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

        internal static void SaveUsage(DomainObjecsSalg2.Sales.UserLogg ul)
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

        internal static List<DomainObjecsSalg2.Sales.Users> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var list = session.CreateQuery("from Users where slettet != 1").List();

                    List<DomainObjecsSalg2.Sales.Users> l = new List<DomainObjecsSalg2.Sales.Users>();

                    foreach (DomainObjecsSalg2.Sales.Users item in list)
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
