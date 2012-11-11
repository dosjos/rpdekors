using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeTerminal.DataAccesLayer;
using DomainObjecsSalg.Sales;
using NHibernate;

namespace CafeTerminal.DataAccess
{
    public class VareProvider
    {
        internal static void Save(Vare vare)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    int i = 0;
                    try
                    {
                        var res = session.CreateQuery("select MAX(Rank) from Vare");
                         i = (int)res.UniqueResult();
                        i++;
                    }
                    catch (Exception e)
                    {
                    
                    }
                    if (i == 0)
                    {
                        i = 1;
                    }
                    vare.Rank = i;
                    Console.WriteLine(i);
                    session.Save(vare);
                    transaction.Commit();

                }
            }
        }

        internal static List<Vare> GetVarerCurrentlyInUse()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    List<Vare> list = new List<Vare>();
                    var res = session.CreateQuery("from Vare where CurrentlyInUse = 1 Order by Rank ASC").List();//  
                    foreach (var item in res)
                    {

                        list.Add((Vare)item);
                    }
                    return list;
                }
            }
        }

        internal static Vare GetVarer(string p, int s)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var vare = session.CreateQuery("from Vare where Pris = :pris and Navn like :name")
                        .SetParameter("pris", s)
                        .SetParameter("name", p).List<Vare>();
                    if (vare[0] != null)
                    {
                        return vare[0];
                    }
                    return null;
                }
            }
        }

        internal static List<Vare> GetAlleVarer()
        {
            List<Vare> list = new List<Vare>();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var vare = session.CreateQuery("from Vare Order by Rank ASC").List<Vare>();

                    foreach (var item in vare)
                    {
                        list.Add((Vare)item);
                    }
                    return list;
                }
            }
        }

        internal static void UpdateVare(Vare v)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(v);
                    transaction.Commit();
                }
            }
        }

        internal static Vare GetVare(int t)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var vare = session.CreateQuery("from Vare where Id = :id")
                        .SetParameter("id", t)
                        .List<Vare>();
                    return vare[0];
                }
            }
        }

        internal static void PushVareUp(Vare v)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    int temp;
                    if(v.Rank != 1){
                        temp= v.Rank;
                        v.Rank--;

                        Console.WriteLine("Old rank {0}, new rank {1}", temp, v.Rank);

                        var res = session.CreateQuery("from Vare Where Rank = :rank").SetParameter("rank", v.Rank);
                        Vare vv = (Vare)res.UniqueResult();
                        vv.Rank = temp;

                        Console.WriteLine("vv new rank {0}", vv.Rank);

                        session.Update(v);
                        session.Update(v);
                        transaction.Commit();
                    }
                }
            }
        }

        internal static void PushVareDown(Vare v)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res2= session.CreateQuery("select MAX(Rank) from Vare");
                    int i = (int)res2.UniqueResult();

                    int temp;
                    if (v.Rank != i)
                    {
                        temp = v.Rank;
                        v.Rank++;

                        Console.WriteLine("Old rank {0}, new rank {1}", temp, v.Rank);

                        var res = session.CreateQuery("from Vare Where Rank = :rank").SetParameter("rank", v.Rank);
                        Vare vv = (Vare)res.UniqueResult();
                        vv.Rank = temp;

                        Console.WriteLine("vv new rank {0}", vv.Rank);

                        session.Update(v);
                        session.Update(v);
                        transaction.Commit();
                    }
                }
            }
        }
    }
}

