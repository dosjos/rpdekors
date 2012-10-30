using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeTerminal.DomainObjects;

namespace DomainObjects.Visit
{
    public class Visit
    {
        public virtual DateTime VisitTime { get; set; }
        public virtual int Id { get; set; }
        public virtual Kid KidId { get; set; }
        public virtual String RestrictionDate { get; set; }

        public Visit(){
            if (VisitTime == DateTime.MinValue)
            {
                VisitTime = DateTime.Now;
                if (RestrictionDate == null)
                {
                    RestrictionDate = DateTime.Now.Year + " " + DateTime.Now.Month + " " + DateTime.Now.Day;
                }
            }
            else
            {
                SetRestrictionDate();
            }

        }

        public virtual void SetRestrictionDate()
        {
            RestrictionDate = VisitTime.Year + " " + VisitTime.Month + " " + VisitTime.Day;
        }
    }
}
