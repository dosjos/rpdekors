using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Settings;
using NHibernate;

namespace Visitor_Registration.DataAccesLayer
{
    public class SettingsProvider
    {
        internal static bool HaveAgeSettings()
        {
            //TODO skjekk om databasen inneholder settings
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    int i = 0;
                    var res = session.CreateQuery("from Settings").List<Settings>();
                    foreach (var item in res)
                    {
                        if (item.Type.Equals("lowestyear"))
                        {
                            i++;
                        }
                        if (item.Type.Equals("highestyear"))
                        {
                            i++;
                        }
                    }
                    if (i == 2)
                    {
                        return true;    
                    }
                    
                }
            }
            return false;
        }

        internal static decimal GetLowestYear()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Settings").List<Settings>();
                    foreach (var item in res)
                    {
                        if (item.Type.Equals("lowestyear"))
                        {
                            return Convert.ToInt32(item.Value);
                        }
                    }
                }
            }
            return 0;
        }

        internal static decimal GetHighestYear()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Settings").List<Settings>();
                    foreach (var item in res)
                    {
                        if (item.Type.Equals("highestyear"))
                        {
                            return Convert.ToInt32(item.Value);
                        }
                    }
                }
            }
            return 0;
        }

        internal static void SaveHighestYear(decimal p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Settings").List<Settings>();
                    foreach (var item in res)
                    {
                        if (item.Type.Equals("highestyear"))
                        {
                            item.Value = "" + (int)p;
                            session.Update(item);
                            transaction.Commit();

                            return;
                        }
                    }
                    Settings s = new Settings()
                    {
                        Type = "highestyear",
                        Value = "" + (int)p
                    };
                    session.Save(s);
                    transaction.Commit();
                }
            }
        }

        internal static void SaveLowestYear(decimal p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var res = session.CreateQuery("from Settings").List<Settings>();
                    foreach (var item in res)
                    {
                        if (item.Type.Equals("lowestyear"))
                        {
                            item.Value = "" + (int)p;
                            session.Update(item);
                            transaction.Commit();

                            return;
                        }
                    }
                    Settings s = new Settings()
                    {
                        Type = "lowestyear",
                        Value = "" + (int)p
                    };
                    session.Save(s);
                    transaction.Commit();

                }
            }
        }
    }
}
