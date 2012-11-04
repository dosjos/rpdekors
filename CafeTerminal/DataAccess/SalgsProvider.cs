using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeTerminal.DataAccesLayer;
using DomainObjecsSalg.Sales;
using NHibernate;

namespace CafeTerminal.DataAccess
{
    public class SalgsProvider
    {
        internal static void LagreSalg(Vare v)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Salg s = new Salg() { Pris = v.Pris, SlagsTid = DateTime.Now, VareId = v.Id};
                    session.Save(s);
                    transaction.Commit();
                }
            }
        }
    }
}
