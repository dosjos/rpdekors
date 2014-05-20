using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using CafeTerminal.DataAccess;
using DomainObjecsSalg2.Settings;
using NHibernate;

namespace CafeRegnskap.DataAccess
{
    public class SettingsProvider
    {

        internal static bool HavePassSettings()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Settings where Type like 'Passord'").UniqueResult();
                    if (res != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        internal static string GetPassord()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Settings where Type like 'Passord'").UniqueResult();
                    if (res != null)
                    {
                        Settings s = (Settings)res;
                        return s.Value;
                    }
                    return string.Empty;
                }
            }
        }

        internal static void LagrePass(DomainObjecsSalg2.Settings.Settings s)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Settings where Type like 'Passord'").UniqueResult();
                    if (res != null)
                    {
                        Settings ss = (Settings)res;
                        ss.Value = s.Value;
                        session.Update(ss);
                    }
                    else
                    {
                        session.Save(s);
                    }
                    transaction.Commit();
                }
            }
        }
    }
}
