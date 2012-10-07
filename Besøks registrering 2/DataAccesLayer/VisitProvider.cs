using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using NHibernate;
using DomainObjects.Visit;
using Visitor_Registration.DomainObjects;

namespace Visitor_Registration.DataAccesLayer
{
    class VisitProvider
    {
        internal static System.ComponentModel.BindingList<StringValue> GetTodaysVisits()
        {
            List<Visit> list = null;
            BindingList<StringValue> result = new BindingList<StringValue>();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery(" FROM Visit WHERE VisitTime >= DATEADD(DAY, DATEDIFF(DAY, '19000101', GETDATE()), '19000101')" +
                        "AND VisitTime < DATEADD(DAY, DATEDIFF(DAY, '18991231', GETDATE()), '19000101')")
                        .List<Visit>();
                    list = (List<Visit>)res;
                }
                var kids = KidProvider.GetKidsBasedOnIdInVisit(list);
                foreach (var item in kids)
                {
                    result.Add(new StringValue(item.FirstName));
                }
            }
            return result;
        }

        public static void RegisterVisit(Kid k)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Visit v = new Visit();
                    v.KidId = k;
                    session.Save(v);//TODO Feiler her med GenericADOException dersom constrainten ikke oveholdes
                    transaction.Commit();
                }
            }
        }
    }
}
