using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Visitor_Registration.DataAccesLayer
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

        public static int GetLowestYear()
        {
            int temp = Convert.ToInt32(ConfigurationManager.AppSettings["lowestyear"]);
            return temp;
        }
        public static int GetHighestYear()
        {
            int temp = Convert.ToInt32(ConfigurationManager.AppSettings["highestyear"]);
            return temp;
        }
    }
}
