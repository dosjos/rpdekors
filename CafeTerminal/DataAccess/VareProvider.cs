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
                    var res = session.CreateQuery("from Vare").List();//  where CurrentlyInUse = 1
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
                    Console.WriteLine(vare.Count);
                    return vare[0];
                }
            }
        }
    }
}
