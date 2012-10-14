﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using NHibernate;
using DomainObjects.Visit;
using Visitor_Registration.DomainObjects;
using NHibernate.Criterion;

namespace Visitor_Registration.DataAccesLayer
{
    public class VisitProvider
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

        public static void Save(Visit v)
        {
            
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(v);//TODO Feiler her med GenericADOException dersom constrainten ikke oveholdes
                        transaction.Commit();
                    }
                }
            
        }

        internal static List<Visit> GetVisitsWithinDates(DateTime start, DateTime end)
        {
            List<Visit> list = new List<Visit>(); ;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery(" FROM Visit WHERE VisitTime >= :start " +
                        "AND VisitTime <= :end order by VisitTime")
                        .SetParameter("start", start)
                        .SetParameter("end", end)
                        .List<Visit>();
                    list = (List<Visit>)res;


                    //var res = session.CreateQuery("from Kid k where k.FirstName + ' ' + k.LastName = :name")
                   //  .SetParameter("name", kidName)
                }
            }

            return list;
        }

        internal static List<Kid> GetTodaysVisitKids()
        {
            List<Visit> list = null;
            List<Kid> kids;
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
                kids = KidProvider.GetKidsBasedOnIdInVisit(list);
               
            }
            return kids;
        }

        internal static IEnumerable GetAllVisitsThisYear()
        {
            List<int> list = new List<int>();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    DateTime d = DateTime.Now;
                    
                    var res = session.CreateSQLQuery("select count(*) as c, DATEPART(WEEKDAY, VisitTime) as v" +
                                                  " from Visit  " +
                                                  " where YEAR(VisitTime) = :year "+
                                                  " group by DATEPART(WEEKDAY, VisitTime) " +
                                                  " order by DATEPART(WEEKDAY, VisitTime) "
                                                  ).SetParameter("year", d.Year)
                                                  .List<object[]>()
                                                  .Select(x => new {Count = (int)x[0], Count2 = (int)x[1] })
                                                  ;
                    //http://stackoverflow.com/questions/6029631/nhibernate-3-1-linq-group-by-then-order-by-count-issue
                    foreach (var item in res)
                    {
                        Console.WriteLine("{0}-{1}", item.Count, item.Count2);
                    }
                    return res;

    

                }
            }
            return null;
        }
    }
}
