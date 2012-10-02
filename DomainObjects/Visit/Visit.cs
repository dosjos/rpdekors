using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visitor_Registration.DomainObjects;

namespace DomainObjects.Visit
{
    public class Visit
    {
        public virtual DateTime VisitTime { get; set; }
        public virtual int Id { get; set; }
        public virtual Kid KidId { get; set; }


    }
}
