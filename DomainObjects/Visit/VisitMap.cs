using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace DomainObjects.Visit
{
    class VisitMap: ClassMap<Visit>
    {
        public VisitMap(){
            Id(x => x.Id);
            Map(x => x.VisitTime);
            References(x => x.KidId).Column("KidId");
        }
    }
}
