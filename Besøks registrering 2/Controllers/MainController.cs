using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Settings;
using DomainObjects.Visit;
using Visitor_Registration.DataAccesLayer;
using DomainObjects;
using Visitor_Registration.UI;

namespace Visitor_Registration.Controllers
{
    public class MainController
    {
        private MainWindow mw;
        private RegisterKidForm kidForm;

        public MainController(MainWindow m)
        {

            mw = m;
        }
        internal object getAllKids()
        {
            return KidProvider.getAllKids();
        }

        internal Kid GetKid(string kidName)
        {
            return KidProvider.GetKid(kidName);
        }

        #region registerkids
        internal void RegisterKid(string kidName)
        {
            if (KidProvider.RegisterKid(kidName))
            {
                RegisterVisit(KidProvider.GetKid(kidName));
            }
            else
            {
                mw.Enabled = false;
                kidForm = new RegisterKidForm(this);
            }
        }

        public void RegisterVisit(Kid k)
        {
            try
            {
                VisitProvider.RegisterVisit(k);
                mw.AddVisit(k.FirstName);
            }
            catch (Exception e)
            {
                mw.Enabled = false;
                new ErrorMessage(this, "Du kan ikke registrere deg to ganger for samme dag");
                Console.WriteLine(e.StackTrace);
            }
        }

        internal BindingList<StringValue> GetTodaysVisits()
        {
            BindingList<StringValue> list = new BindingList<StringValue>();
            try
            {
                list = VisitProvider.GetTodaysVisits();
            }
            catch (Exception e)
            {
                new ErrorMessage(this, "Noe gikk feil under henting av dagens besøkende");
                Console.WriteLine(e.StackTrace);
            }
            return list;
        }


        internal void SaveKid(Kid k)
        {
            bool test = true;
            try
            {
                KidProvider.Save(k);
            }
            catch (Exception e)
            {
                test = false;
                new ErrorMessage(this, "Det er allerede registrert en person med samme fornavn, etternavn, fødselsår og postnummer. Dersom du aldri har registeret deg før, legg til en ekstra bokstav i fornavnet ditt: For eksempel hvis du heter \"Jan\", skriv \"Jan J.\"");
            }
            if (test)
            {
                RegisterVisit(k);
                ReEnableMainWindow();
                kidForm.Dispose();
            }
        }
        #endregion

        internal void ReEnableMainWindow()
        {
            mw.Enabled = true;
            mw.InitializeImages();
        }

        #region VisitDBcalls
        internal static void AdddGenericVisit(string s)
        {
            GenericVisitorProvider.AddVisit(s);
        }

        internal List<Visit> GetSortedVisitList(DateTime start, DateTime end)
        {
            List<Visit> list = VisitProvider.GetVisitsWithinDates(start, end);

            return list;
        }

        internal List<Kid> GetTodaysVisitKids()
        {
            return VisitProvider.GetTodaysVisitKids();
        }

        internal List<GenericVisitor> GetTodaysGenericVisits()
        {
            return GenericVisitorProvider.GetTodaysVisits();
        }

        internal List<int> GetAllVisitsThisYear()
        {
            return VisitProvider.GetAllVisitsThisYear();
        }

        internal List<GenericVisitor> GetAllGenericVisitsThisYear()
        {
            throw new NotImplementedException();
        }

        internal int GetGutterThisDay(DateTime dateTime)
        {
            return VisitProvider.GetGutterThisDay(dateTime);
        }
        #endregion

        internal void restart()
        {
            mw.Visible = false;
            mw = new MainWindow(this);
            mw.Visible = true;
        }

        #region agesettings
        internal void Settingscheck()
        {
            try
            {
                if (!SettingsProvider.HaveAgeSettings())
                {
                    InsertAgeSettings();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                try
                {
                    NHibernateHelper.ResetDatabase();

                }
                catch (Exception ee)
                {
                    new ErrorMessage(this, "Programmet finner ikke databasen, vennligst skjekk at databasen er installert og riktig satt opp med databasen \"VisitDatabase\" og at brukeren \"rodekors\" har dbcreator og adminrettigheter på den");
                    mw.ExitApplication(this, null);
                }
            }
        }

        private void InsertAgeSettings()
        {
            new InformationBox("Velkommen til Besøksregistrering for Røde Kors Cafe Condio. Før programmet tas i bruk bør du gå inn på options og kontrollpanel for å konfigurere programmet");
        }
        #endregion

        internal void NewControllpanel()
        {
            new ControlPanel(this);
            mw.Enabled = false;
        }

        #region images
        internal string GetRightImage()
        {
            return SettingsProvider.GetRightImage();
        }

        internal string GetLeftImage()
        {
            return SettingsProvider.GetLeftImage();
        }

        internal bool HaveLeftImage()
        {
            return SettingsProvider.HaveLeftImage();
        }

        internal bool HaveRightImage()
        {
            return SettingsProvider.HaveRightImage();
        }

        internal void InsertLeftImage(string p)
        {
            SettingsProvider.InsertLeftImage(p);
        }

        internal void InsertRightImage(string p)
        {
            SettingsProvider.InsertRightImage(p);
        }
        #endregion

        internal List<int> GetAllYearsWithVisits()
        {
            return VisitProvider.GetAllYearsWithVisits();
        }

        internal List<Kid> GetVisitByYear(string p)
        {
            return VisitProvider.GetVisitByYear(p);
        }

        internal List<GenericVisitor> GetGenericVisitByYear(string p)
        {
            return GenericVisitorProvider.GetVisitByYear(p);
        }

        internal List<int> GetMonthsWithVisits(string p)
        {
            return VisitProvider.GetMonthsWithVisits(p);
        }

        internal void UpdateKid(Kid KK)
        {
            KidProvider.UpdateKid(KK);
        }

        internal bool HavePassSetting()
        {
            return SettingsProvider.HavePassSettings();
        }

        internal string GetPassord()
        {
            return SettingsProvider.GetPassord();
        }


        internal void LagrePassord(Settings s)
        {
            SettingsProvider.LagrePass(s);
        }

        internal List<Kid> GetVisitByYearAndMonth(string p, int CurrentYear)
        {
            return VisitProvider.GetVisitByYearAndMonth( p,  CurrentYear);
        }

        internal List<GenericVisitor> GetGenericVisitByYearAndMonth(string p, int CurrentYear)
        {
            return GenericVisitorProvider.GetGenericVisitByYearAndMonth( p,  CurrentYear);
        }
    }
}
