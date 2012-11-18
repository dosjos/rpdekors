using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visitor_Registration.DataAccesLayer;
using NHibernate;

namespace Visitor_Registration.DataAccess
{
    public class UserProvider
    {
        internal static int SaveUser(DomainObjecsSalg.Sales.Users u)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(u);
                    transaction.Commit();
                    return u.Id;
                }
            }
        }

        internal static void SaveUsage(DomainObjecsSalg.Sales.UserLogg ul)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(ul);
                    transaction.Commit();
                }
            }
        }
    }
}
