using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Visit;
using NHibernate;

namespace Visitor_Registration.DataAccesLayer
{
    public class GenericVisitorProvider
    {
        public static void AddVisit(string s)
        {
            GenericVisitor gv = new GenericVisitor();
            gv.Type = s;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(gv);
                    transaction.Commit();
                }
            }
        }

        internal static List<GenericVisitor> GetTodaysVisits()
        {
            List<GenericVisitor> kids;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery(" FROM GenericVisitor WHERE VisitTime >= DATEADD(DAY, DATEDIFF(DAY, '19000101', GETDATE()), '19000101')" +
                        "AND VisitTime < DATEADD(DAY, DATEDIFF(DAY, '18991231', GETDATE()), '19000101')")
                        .List<GenericVisitor>();
                    kids = (List<GenericVisitor>)res;
                }
            }
            return kids;
        }

        internal static List<GenericVisitor> GetVisitByYear(string p)
        {
            List<GenericVisitor> kids;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery(" FROM GenericVisitor WHERE DATEPART(YEAR, VisitTime) like :year ").SetParameter("year", p)
                        .List<GenericVisitor>();
                    kids = (List<GenericVisitor>)res;
                }
            }
            return kids;
        }
    }
}
