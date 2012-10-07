using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Visitor_Registration.DomainObjects;

namespace Visitor_Registration.DataAccesLayer
{
    public class KidMap : ClassMap<Kid>
    {
        public KidMap(){
            Id(x => x.Id);
            Map(x => x.FirstName).UniqueKey("UniquePerson");
            Map(x => x.LastName).UniqueKey("UniquePerson");
            Map(x => x.Age).UniqueKey("UniquePerson");
            Map(x => x.Email);
            Map(x => x.Ethnisity);
            Map(x => x.Gender);
            Map(x => x.Postcode).UniqueKey("UniquePerson");
            Map(x => x.TLF);
        }
    }
}
