using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeTerminal.DataAccess;
using DomainObjecsSalg.Sales;

namespace CafeTerminal.Controller
{
    public class MainController
    {
        private MainWindow mainWindow;

        public MainController(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        internal void SaveVare(DomainObjecsSalg.Sales.Vare vare)
        {
            VareProvider.Save(vare);
        }

        internal void ReactivateSettingsWindow()
        {
            
        }

        internal List<Vare> GetVarerCurrentlyForSale()
        {
            return VareProvider.GetVarerCurrentlyInUse();
        }

        internal void LagreSalg(string p, int s)
        {
            var vare = VareProvider.GetVarer(p, s);
            SalgsProvider.LagreSalg(vare);
        }
    }
}
