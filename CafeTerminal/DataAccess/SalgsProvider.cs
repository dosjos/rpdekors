using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeTerminal.DataAccesLayer;
using DomainObjectsSalg.Sales;
using NHibernate;

namespace CafeTerminal.DataAccess
{
    public class SalgsProvider
    {
        internal static void LagreSalg(Vare v)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Salg s = new Salg() { Pris = v.Pris, SlagsTid = DateTime.Now, VareId = v.Id};
                    session.Save(s);
                    transaction.Commit();
                }
            }
        }

        internal static List<Salg> GetTodaysSales()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                       var res = session.CreateQuery("from Salg where DatePart(YEAR, SlagsTid) = :year and DatePart(MONTH, SlagsTid) = :month AND DatePart(DAY, SlagsTid) = :day")
                           .SetParameter("year", DateTime.Now.Year).SetParameter("month", DateTime.Now.Month).SetParameter("day", DateTime.Now.Day).List<Salg>();
                       return res.ToList<Salg>();
                }
            }
        }

        internal static List<Salg> GetSalesIn(DateTime dateTime1, DateTime dateTime2)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Salg where Slagstid >= :start and Slagstid <= :stop")
                        .SetParameter("start", dateTime1).SetParameter("stop",dateTime2).List<Salg>();
                    return res.ToList<Salg>();
                }
            }
        }
    }
}
