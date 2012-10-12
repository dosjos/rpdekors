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
        private Controllers.MainController mc;


        public void MockVisits()
        {
            NHibernateHelper.ResetDatabase();
            rand = new Random();
            CreateUsers();
            MakeEmVisitUs();

        }

        private  void MakeEmVisitUs()
        {
            for (int i = 0; i < 400; i++)
            {
                Kid k = (Kid)users[rand.Next(users.Count - 1)];
                Visit v = new Visit();
                v.KidId = k;
                DateTime d = new DateTime(rand.Next(2011, 2013), rand.Next(1, 13), rand.Next(1, 31));
                v.VisitTime = d;
                v.SetRestrictionDate();
                VisitProvider.Save(v);
            }
        }

        private  void CreateUsers()
        {
            for (int i = 0; i < NUMBER_OF_USERS; i++)
            {
                Kid k = new Kid();
                k.FirstName = "kid" + i;
                k.LastName = "Kid" + i;
                k.Age = rand.Next(CustomizationManager.GetLowestYear(), CustomizationManager.GetHighestYear());
                users.Add(k);
                KidProvider.Save(k);
            }
        }
    }
}
