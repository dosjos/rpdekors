using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CafeTerminal.DataAccess;
using DomainObjectsSalg.Sales;
using CafeTerminal.UI;
namespace CafeTerminal.Controller
{
    public class MainController
    {
        private MainWindow mainWindow;
        private DataProvider dataProvider; 

        public MainController(MainWindow mainWindow, DataProvider _data)
        {
            this.mainWindow = mainWindow;
            dataProvider = _data;
        }

       

        internal void SaveVare(DomainObjectsSalg.Sales.Vare vare)
        {
            dataProvider.Save(vare);
        }


        internal List<Vare> GetVarerCurrentlyForSale()
        {
            return dataProvider.GetVarerCurrentlyInUse();
        }

        internal void LagreSalg(string p, int s)
        {
            var vare = dataProvider.GetVarer(p, s);
            dataProvider.LagreSalg(vare);
        }

        internal List<Vare> GetAlleVarer()
        {
            return dataProvider.GetAlleVarer();
        }

        internal Vare GetVare(string p1, int p2)
        {
            return dataProvider.GetVarer(p1, p2);
        }

        internal void UpdateVare(Vare v)
        {
            dataProvider.UpdateVare(v);
        }

        internal void UpdateMainButtons()
        {
            mainWindow.GetButtons();
            mainWindow.ResetTime();
        }

        internal Vare GetVare(int t)
        {
            return dataProvider.GetVare(t);
        }

        internal void PushVareUp(Vare v)
        {
            dataProvider.PushVareUp(v);
        }

        internal void PushVareDown(Vare v)
        {
            dataProvider.PushVareDown(v);
        }

        internal void LagreLogg(Logg l)
        {
            dataProvider.LagreLogg(l);
        }

        internal Logg GetLastLog()
        {
            return dataProvider.GetLastLogg();
        }

        internal int GetDagensSalg()
        {
            var list = dataProvider.GetTodaysSales();
            return list.Sum(item => item.Pris);
        }

        internal void EnableMainWindow()
        {
            mainWindow.ReenableWindow();
        }
        internal void LockMainWindow()
        {
            mainWindow.LockWindow();
        }

        internal bool HavePassSetting()
        {
            return dataProvider.HavePassSettings();
        }

        internal string GetPassord()
        {
            return dataProvider.GetPassord();
        }


        internal void LagrePassord(DomainObjectsSalg.Settings.Settings s)
        {
            dataProvider.LagrePass(s);
        }

        internal void EnableMainWindow(bool p)
        {
            mainWindow.ReenableWindow(p);
        }

        internal void LagreBruker(string navn, string yrke)
        {
            Users u = new Users() { Navn = navn, Rolle = yrke };
            int id = dataProvider.SaveUser(u);
           
        }

        internal void Restart()
        {
            mainWindow.Visible = false;
            mainWindow = new MainWindow(this);
            mainWindow.Visible = true;
        }


        internal List<Users> GetAlleBrukere()
        {
            return dataProvider.GetAllUsers();
        }

        internal Users GetBruker(int t)
        {
            return dataProvider.GetUser(t);
        }

        internal void SaveUserLog(UserLogg ul)
        {
            dataProvider.SaveLog(ul);
            mainWindow.DagensBruker(ul);
        }

        internal List<UserLogg> GetTodaysUsers()
        {
            return dataProvider.GetTodayUsers();
        }
    }
}
