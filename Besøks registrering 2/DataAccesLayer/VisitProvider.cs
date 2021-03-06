﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using NHibernate;
using DomainObjects.Visit;
using DomainObjects;
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
        /// <summary>
        /// Gives a list of ints that contains the number of visits this year, and orders it by the weekdays
        /// </summary>
        /// <returns>The number of visits on distinct weekdays this year</returns>
        internal static List<int> GetAllVisitsThisYear()
        {
            List<int> list = new List<int>();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    DateTime d = DateTime.Now;

                    var res = session.CreateSQLQuery("select  count(*), DATEPART(WEEKDAY, VisitTime)" +
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
                        list.Add(item.Count);
                    }
                    return list;
                }
            }
        }

        /// <summary>
        /// A method that gives the amount of boys who visited the given datre
        /// </summary>
        /// <param name="dateTime">the Datetime to search for visits on</param>
        /// <returns>The number of boys who visited on the given date</returns>
        internal static int GetGutterThisDay(DateTime dateTime)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery(" FROM Visit WHERE DATEPART(DAY, VisitTime) like DATEPART(DAY, :date) " +
                    " AND DATEPART(MONTH, VisitTime) like DATEPART(MONTH, :date2) AND DATEPART(YEAR, VisitTime) like DATEPART(YEAR, :date3) "
                        )
                        .SetParameter("date", dateTime.Date)
                        .SetParameter("date2", dateTime.Date)
                        .SetParameter("date3", dateTime.Date)
                        .List<Visit>();

                    int i = (from item in res
                             where item.KidId.Gender.Equals("Mann")
                             select item).Count<Visit>();
                    return i;
                }
            }
        }
        /// <summary>
        /// Gives a list of all years that have visits
        /// </summary>
        /// <returns>A list containing all years with visits</returns>
        internal static List<int> GetAllYearsWithVisits()
        {
            List<int> list = new List<int>();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    IQuery res = session.CreateSQLQuery("select distinct(DATEPART(YEAR, VisitTime)) from Visit");
                    var list2 = res.List();
                    foreach (var item in list2)
                    {
                        list.Add((int)item);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Gets all the visits in the year provided
        /// </summary>
        /// <param name="p">The year to get the visits in</param>
        /// <returns>A list of all the kids that visited this year</returns>
        internal static List<Kid> GetVisitByYear(string p)
        {
            List<Visit> list = null;
            List<Kid> kids;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery(" FROM Visit WHERE DATEPART(YEAR, VisitTime) like :year")
                        .SetParameter("year", p)
                        .List<Visit>();
                    list = (List<Visit>)res;
                }
                kids = KidProvider.GetKidsBasedOnIdInVisit(list);
            }
            return kids;
        }
        /// <summary>
        /// Gets a distinct list containing alle the monts with visits a choosen year
        /// </summary>
        /// <param name="p">The year to get the month list for</param>
        /// <returns>A sorted list containing a distinc set of months with visits</returns>
        internal static List<int> GetMonthsWithVisits(string p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("From Visit where Datepart(YEAR, VisitTime) = :year")
                        .SetParameter("year", p)
                        .List<Visit>();

                    var res2 = (from v in res
                                orderby v.VisitTime.Month
                               select v.VisitTime.Month)
                               .Distinct();

                    return res2.ToList<int>();
                }
            }
        }

        internal static List<Kid> GetVisitByYearAndMonth(string p, int CurrentYear)
        {
            List<Visit> list = null;
            List<Kid> kids;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery(" FROM Visit WHERE DATEPART(YEAR, VisitTime) like :year and Datepart(MONTH, VisitTime) = :month")
                        .SetParameter("year", CurrentYear)
                        .SetParameter("month", p)
                        .List<Visit>();
                    list = (List<Visit>)res;
                }
                kids = KidProvider.GetKidsBasedOnIdInVisit(list);
            }
            return kids;
        }
    }
}
