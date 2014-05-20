using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace CafeRegnskap.DataAccess
{
    public class CustomizationManager
    {
        internal static string GetServer()
        {
            return ConfigurationManager.AppSettings["server"];
        }

        internal static string GetDatabase()
        {
            return ConfigurationManager.AppSettings["database"];
        }
    }
}
