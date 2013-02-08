using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainObjecsSalg.Sales;
using NHibernate;

namespace CafeTerminal.DataAccess
{
    public class LoggProvider
    {
        internal static void LagreLogg(Logg v)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(v);
                    transaction.Commit();
                }
            }
        }

        internal static Logg GetLastLogg()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Logg where Id = (select max(Id) from Logg)").UniqueResult();
                    return (Logg) res;
                }
            }
        }
    }
}
