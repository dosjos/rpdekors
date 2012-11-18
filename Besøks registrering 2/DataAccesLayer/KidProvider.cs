using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Visitor_Registration.DomainObjects;
using DomainObjects.Visit;
using System.Data.SqlClient;
using NHibernate.Exceptions;

namespace Visitor_Registration.DataAccesLayer
{
    public class KidProvider
    {
        internal static Boolean CheckIfKidExist(string kidName)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Kid k where k.FirstName + ' ' + k.LastName = :name and Deleted = 0")
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


        public static Boolean RegisterKid(string kidName)
        {
            if (CheckIfKidExist(kidName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean RegisterVisit(string kidName)
        {
            Visit v = new Visit();
            Kid k = GetKid(kidName);
            v.KidId = k;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(v);
                        transaction.Commit();
                    }
                    catch (GenericADOException e)
                    {
                        Console.WriteLine(e.StackTrace);
                        return false;
                    }
                }
            }
            return true;
        }

        public static Kid GetKid(string kidName)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Kid k where k.FirstName + ' ' + k.LastName = :name and Deleted = 0")
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

        public static void Save(Kid k)
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
                    var res = session.CreateQuery("from Kid k where k.FirstName + ' ' + k.LastName = :name and Deleted = 0")
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

        public static object getAllKids()
        {
            List<object> list = new List<object>();
            list.Add("");
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (session != null)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        var res = session.CreateCriteria(typeof(Kid)).List<Kid>();
                        foreach (var item in res)
                        {
                            if (item.Deleted == false)
                            {
                                list.Add(item.FirstName + " " + item.LastName);
                            }
                        }
                    }
                }
            }
            return list;
        }

        internal static List<Kid> GetKidsBasedOnIdInVisit(List<Visit> list)
        {
            List<Kid> result = new List<Kid>();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    if(list != null){
                        foreach (var item in list)
                        {
                            var res = session.CreateQuery("from Kid k where k.Id  = :id")
                                    .SetParameter("id", item.KidId.Id)
                                    .List<Kid>();
                            if (res != null && res[0].Deleted == false)
                            {
                                result.Add(res[0]);
                            }
                        }
                    }
                }
            }

            return result;
        }

        internal static void UpdateKid(Kid KK)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(KK);
                    transaction.Commit();
                }
            }
        }
    }
}
