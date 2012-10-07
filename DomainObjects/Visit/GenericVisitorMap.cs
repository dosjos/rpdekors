using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace DomainObjects.Visit
{
    public class GenericVisitorMap : ClassMap<GenericVisitor>
    {
        public GenericVisitorMap()
        {
            Id(x => x.Id);
            Map(x => x.VisitTime);
            Map(x => x.Type);
        }
    }
}
