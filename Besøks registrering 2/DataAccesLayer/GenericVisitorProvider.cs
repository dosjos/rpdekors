using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Visit;
using NHibernate;

namespace Visitor_Registration.DataAccesLayer
{
    public class GenericVisitorProvider
    {
        internal static void AddVisit(string s)
        {
            GenericVisitor gv = new GenericVisitor();
            gv.Type = s;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(gv);
                    transaction.Commit();
                }
            }
        }
    }
}
