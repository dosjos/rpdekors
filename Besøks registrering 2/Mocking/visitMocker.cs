using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Visit;
using Visitor_Registration.DataAccesLayer;
using Visitor_Registration.DomainObjects;

namespace Visitor_Registration.Mocking
{
    class visitMocker
    {


        private static int NUMBER_OF_USERS = 30;
        ArrayList users = new ArrayList();
        Random rand;


        public void MockVisits()
        {
            NHibernateHelper.ResetDatabase();
            rand = new Random();
            CreateUsers();
            MakeEmVisitUs();
            CreateGenericVisitors();
        }

        private void CreateGenericVisitors()
        {
            for (int i = 0; i < 400; i++)
            {
                string res = "";
                switch (i % 4)
                {
                    case 1:
                        res = "Gutt";
                        break;
                    case 2:
                        res = "Jente";
                        break;
                    case 3:
                        res = "Anonym";
                        break;
                    case 0:
                        res = "Ukjent";
                        break;
                }

                GenericVisitorProvider.AddVisit(res);
            }
        }

        private void MakeEmVisitUs()
        {
            for (int i = 0; i < 600; i++)
            {
                Kid k = (Kid)users[rand.Next(users.Count - 1)];
                Visit v = new Visit();
                v.KidId = k;
                DateTime d = new DateTime(rand.Next(2011, 2013), rand.Next(1, 13), rand.Next(1, 31));
                v.VisitTime = d;
                v.SetRestrictionDate();
                try
                {
                    VisitProvider.Save(v);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private void CreateUsers()
        {
            for (int i = 0; i < NUMBER_OF_USERS; i++)
            {
                Kid k = new Kid();
                k.FirstName = "kid" + i;
                k.LastName = "Kid" + i;
                k.Gender = i % 2 == 0 ? "Kvinne" : "Mann";
                k.Age = rand.Next(CustomizationManager.GetLowestYear(), CustomizationManager.GetHighestYear());
                users.Add(k);
                KidProvider.Save(k);
            }
        }
    }
}
