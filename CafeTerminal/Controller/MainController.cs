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


        internal List<Vare> GetVarerCurrentlyForSale()
        {
            return VareProvider.GetVarerCurrentlyInUse();
        }

        internal void LagreSalg(string p, int s)
        {
            var vare = VareProvider.GetVarer(p, s);
            SalgsProvider.LagreSalg(vare);
        }

        internal List<Vare> GetAlleVarer()
        {
            return VareProvider.GetAlleVarer();
        }

        internal Vare GetVare(string p1, int p2)
        {
            return VareProvider.GetVarer(p1, p2);
        }

        internal void UpdateVare(Vare v)
        {
            VareProvider.UpdateVare(v);
        }

        internal void UpdateMainButtons()
        {
            mainWindow.GetButtons();
        }

        internal Vare GetVare(int t)
        {
            return VareProvider.GetVare(t);
        }

        internal void PushVareUp(Vare v)
        {
            VareProvider.PushVareUp(v);
        }

        internal void PushVareDown(Vare v)
        {
            VareProvider.PushVareDown(v);
        }

        internal void LagreLogg(Logg l)
        {
            LoggProvider.LagreLogg(l);
        }

        internal Logg GetLastLog()
        {
            return LoggProvider.GetLastLogg();
        }

        internal int GetDagensSalg()
        {
            var list = SalgsProvider.GetTodaysSales();
            int sum = 0;
            foreach (var item in list)
            {
                sum += item.Pris;
            }


            return sum;
        }
    }
}
