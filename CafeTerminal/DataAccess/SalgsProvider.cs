using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visitor_Registration.DataAccesLayer;
using DomainObjecsSalg.Sales;
using NHibernate;

namespace Visitor_Registration.DataAccess
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
    }
}
