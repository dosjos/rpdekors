using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using CafeRegnskap.DataAccess;
using CafeTerminal.DataAccess;
using DomainObjecsSalg2.Sales;

namespace CafeRegnskap.Controller
{
    public class MainController
    {
        public List<Salg> GetAllSales()
        {
            return SalgsProvider.GetAllSales();
        }

        internal string GetVareNavn(int p)
        {
            Vare v = VareProvider.GetVare(p);
            return v.Navn;
        }
    }
}
