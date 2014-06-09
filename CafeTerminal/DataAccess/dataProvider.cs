using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DomainObjectsSalg.Sales;
using DomainObjectsSalg.Settings;

namespace CafeTerminal.DataAccess
{
    public class DataProvider
    {
        SalgDbContext db = new SalgDbContext();
        public void LagreLogg(Logg v)
        {
            db.Logg.Add(v);
            db.SaveChanges();
        }

        public Logg GetLastLogg()
        {

            return db.Logg.FirstOrDefault(x => x.LoggTid == DateTime.Today);
            //var res =
            //    session.CreateQuery(
            //        "from Logg where datepart(YEAR, LoggTid) = Datepart(YEAR, GETDATE()) and Datepart(MONTH, LoggTid) = Datepart(MONTH, GETDATE()) and Datepart(DAY, LoggTid) = Datepart(DAY, GETDATE())")
            //           .List<Logg>();
        }



        public List<Logg> GetAlleLogger(DateTime dateTime1, DateTime dateTime2)
        {
            return db.Logg.Where(x => x.LoggTid >= dateTime1 && x.LoggTid <= dateTime2).ToList();

            //var res = session.CreateQuery("from Logg where LoggTid >= :start and LoggTid <= :stop")
            //    .SetParameter("start", dateTime1).SetParameter("stop", dateTime2).List<Logg>();
            //return res.ToList<Logg>();
        }

        public void LagreSalg(Vare v)
        {
            var s = new Salg() { Pris = v.Pris, SlagsTid = DateTime.Now, VareId = v.Id };
            db.Salg.Add(s);
            db.SaveChanges();
        }

        public List<Salg> GetTodaysSales()
        {
            var l = db.Salg.ToList();

            var s = l.Where(x => x.SlagsTid.Date == DateTime.Today).ToList();
                //(from salg in l
                //    where salg.SlagsTid == DateTime.Today.Date
                //    select salg).ToList();
            return s;

            //.Where(x => EntityFunctions.TruncateTime(x.SlagsTid.Date) == EntityFunctions.TruncateTime(DateTime.Today.Date)).ToList();
            //var res = session.CreateQuery("from Salg where DatePart(YEAR, SlagsTid) = :year and DatePart(MONTH, SlagsTid) = :month AND DatePart(DAY, SlagsTid) = :day")
            //            .SetParameter("year", DateTime.Now.Year).SetParameter("month", DateTime.Now.Month).SetParameter("day", DateTime.Now.Day).List<Salg>();
            //        return res.ToList<Salg>();
        }

        public List<Salg> GetSalesIn(DateTime dateTime1, DateTime dateTime2)
        {
            return db.Salg.Where(x => x.SlagsTid.Date >= dateTime1 && x.SlagsTid.Date <= dateTime2).ToList();
            //var res = session.CreateQuery("from Salg where Slagstid >= :start and Slagstid <= :stop")
            //            .SetParameter("start", dateTime1).SetParameter("stop", dateTime2).List<Salg>();
            //        return res.ToList<Salg>();
        }

        public bool HavePassSettings()
        {
            if (db.Settings.Count(x => x.Type == "Passord") != 0)
            {
                return true;
            }
            return false;
        }

        public string GetPassord()
        {
            var setting = db.Settings.FirstOrDefault(x => x.Type == "Passord");
            return setting != null ? setting.Value : string.Empty;
        }

        public void LagrePass(Settings s)
        {

            var setting = db.Settings.FirstOrDefault(x => x.Type == "Passord");
            if (setting == null)
            {
                setting = new Settings
                {
                    Type = "Passord",
                    Value = s.Value
                };
                db.Settings.Add(setting);
            }
            else
            {
                setting.Value = s.Value;
            }
            db.SaveChanges();

        }

        public int SaveUser(DomainObjectsSalg.Sales.Users u)
        {
            db.Users.Add(u);
            db.SaveChanges();
            return u.Id;
        }

        public void SaveUsage(DomainObjectsSalg.Sales.UserLogg ul)
        {
            db.UserLoggs.Add(ul);
            db.SaveChanges();

        }

        public List<Users> GetAllUsers()
        {
            return db.Users.Where(x => !x.Slettet).ToList();
        }

        public Users GetUser(int t)
        {
            return db.Users.FirstOrDefault(x => x.Id == t);
        }

        public void SaveLog(UserLogg ul)
        {
            db.UserLoggs.Add(ul);
            db.SaveChanges();
        }

        public List<UserLogg> GetTodayUsers()
        {
            return db.UserLoggs.Where(x => x.Brukstid == DateTime.Today).ToList();
            //var res = session.CreateQuery("from UserLogg where DatePart(YEAR, Brukstid) = :year  and DatePart(MONTH, Brukstid) = :month AND DatePart(DAY, Brukstid) = :day")
            //            .SetParameter("year", DateTime.Now.Year)
            //            .SetParameter("month", DateTime.Now.Month)
            //            .SetParameter("day", DateTime.Now.Day)
            //            .List<UserLogg>();
            //        return res.ToList<UserLogg>();
        }

        public void Save(Vare vare)
        {
            int rank = 0;
            if (db.Varer.Any())
            {
                rank = db.Varer.Max(x => x.Rank);
            }
            if (rank == 0)
            {
                rank = 1;
            }
            else
            {
                rank++;
            }
            vare.Rank = rank;
            db.Varer.Add(vare);
            db.SaveChanges();
        }

        public List<Vare> GetVarerCurrentlyInUse()
        {
            return db.Varer.Where(x => x.CurrentlyInUse).OrderBy(y => y.Rank).ToList();
        }

        public Vare GetVarer(string p, int s)
        {
            return db.Varer.FirstOrDefault(x => x.Pris == s && x.Navn == p);
        }

        public List<Vare> GetAlleVarer()
        {
            return db.Varer.OrderBy(x => x.Rank).ToList();
        }

        public void UpdateVare(Vare v)
        {
            db.Varer.Attach(v);
            db.Entry(v).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Vare GetVare(int t)
        {
            return db.Varer.FirstOrDefault(x => x.Id == t);
        }

        public void PushVareUp(Vare v)
        {
            if (v.Rank != 1)
            {
                var temp = v.Rank--;
                var vare = db.Varer.FirstOrDefault(x => x.Rank == v.Rank);
                vare.Rank++;
                db.Entry(vare).State = EntityState.Modified;
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();

            }
        }

        public void PushVareDown(Vare v)
        {
            var max = db.Varer.Max(x => x.Rank);
            if (v.Rank != max)
            {
                var temp = v.Rank++;
                var vare = db.Varer.FirstOrDefault(x => x.Rank == v.Rank);
                vare.Rank--;
                db.Entry(vare).State = EntityState.Modified;
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();

            }
        }
    }
}
