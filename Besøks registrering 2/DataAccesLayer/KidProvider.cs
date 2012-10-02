using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Visitor_Registration.DomainObjects;
using DomainObjects.Visit;

namespace Visitor_Registration.DataAccesLayer
{
    public class KidProvider
    {
        private static ISessionFactory sessionFactory;


        internal Boolean CheckIfKidExist(string kidName)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Kid k where k.FirstName + ' ' + k.LastName = :name")
                     .SetParameter("name", kidName)
                        .List<Kid>();
                    if (res.Count > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        internal Boolean RegisterKid(string kidName)
        {
            if (CheckIfKidExist(kidName))
            {
                RegisterVisit(kidName);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void RegisterVisit(string kidName)
        {
            Visit v = new Visit();
            v.VisitTime = DateTime.Now;
            Kid k = GetKid(kidName);
            v.KidId = k;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(k);
                    session.Save(v);
                    transaction.Commit();
                }
            }
        }

        private Kid GetKid(string kidName)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Kid k where k.FirstName + ' ' + k.LastName = :name")
                     .SetParameter("name", kidName)
                        .List<Kid>();
                    if (res.Count > 0)
                    {
                        return res[0];
                    }
                }
            }
            return null;
        }

        internal static void Save(Kid k)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(k);
                    transaction.Commit();
                }
            }
        }

        internal string GetFirstName(string kidName)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Kid k where k.FirstName + ' ' + k.LastName = :name")
                     .SetParameter("name", kidName)
                        .List<Kid>();
                    if (res.Count > 0)
                    {
                        return res[0].FirstName;
                    }
                }
            }
            return null;
        }

        internal object getAllKids()
        {
            List<object> list = new List<object>();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateCriteria(typeof(Kid)).List<Kid>();
                    foreach (var item in res)
                    {
                        list.Add(item.FirstName + " " + item.LastName);
                    }
//                        IList<Post> posts = session
//17	                .CreateCriteria(typeof(Post))
//18	                .List<Post>();
                }
            }
            return list;
        }
    }
}
