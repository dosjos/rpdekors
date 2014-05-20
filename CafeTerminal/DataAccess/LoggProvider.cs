using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeTerminal.DataAccesLayer;
using DomainObjectsSalg.Sales;
using NHibernate;

namespace CafeTerminal.DataAccess
{
    public class LoggProvider
    {
        internal static void LagreLogg(Logg v)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(v);
                    transaction.Commit();
                }
            }
        }

        internal static Logg GetLastLogg()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var res =
                            session.CreateQuery(
                                "from Logg where datepart(YEAR, LoggTid) = Datepart(YEAR, GETDATE()) and Datepart(MONTH, LoggTid) = Datepart(MONTH, GETDATE()) and Datepart(DAY, LoggTid) = Datepart(DAY, GETDATE())")
                                   .List<Logg>();
                        return (Logg) res[0];
                    }
                    catch (Exception e)
                    {
                        return new Logg(){Text = "Skriv her"};
                    }
                }
            }
        }



        internal static List<Logg> GetAlleLogger(DateTime dateTime1, DateTime dateTime2)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Logg where LoggTid >= :start and LoggTid <= :stop")
                        .SetParameter("start", dateTime1).SetParameter("stop", dateTime2).List<Logg>();
                    return res.ToList<Logg>();
                }
            }
        }
    }
}
