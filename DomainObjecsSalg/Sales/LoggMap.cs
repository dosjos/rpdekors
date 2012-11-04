using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace DomainObjecsSalg.Sales
{
    public class LoggMap : ClassMap<Logg>
    {
        LoggMap()
        {
            Id(x => x.Id);
            Map(x => x.LoggTid);
            Map(x => x.Text);
        }
    }
}
