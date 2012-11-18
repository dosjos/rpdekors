using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace DomainObjecsSalg.Sales
{
    public class UsersMap : ClassMap<Users>
    {
        UsersMap()
        {
            Id(x => x.Id);
            Map(x => x.Navn);
            Map(x => x.Rolle);
            Map(x => x.Slettet);
       }
    }
}
