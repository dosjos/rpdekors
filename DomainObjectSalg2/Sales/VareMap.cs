using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace DomainObjecsSalg2.Sales
{
    class VareMap : ClassMap<Vare>
    {
        public VareMap()
        {
            Id(x => x.Id);
            Map(x => x.Navn);
            Map(x => x.Pris);
            Map(x => x.CurrentlyInUse);
            Map(x => x.Rank);
            Map(x => x.Farge);
        }
    }
}
