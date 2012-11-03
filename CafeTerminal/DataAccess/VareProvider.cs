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
                    var res = session.CreateQuery("from Vare where CurrentlyInUse = 1").List();// 
                    foreach (var item in res)
                    {

                        list.Add((Vare)item);
                    }
                    return list;
                }
            }
        }
    }
}
