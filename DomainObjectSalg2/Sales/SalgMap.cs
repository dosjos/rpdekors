using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace DomainObjecsSalg2.Sales
{
    public class SalgMap : ClassMap<Salg>
    {
        SalgMap()
        {
            Id(z => z.Id);
            Map(x => x.Pris);
            Map(x => x.SlagsTid);
            Map(x => x.VareId);
        }
    }
}
