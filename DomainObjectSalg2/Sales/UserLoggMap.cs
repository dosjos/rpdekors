﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace DomainObjecsSalg2.Sales
{
    public class UserLoggMap : ClassMap<UserLogg>
    {
        UserLoggMap()
        {
            Id(x => x.Id);
            Map(z => z.Brukstid);
            Map(c => c.UserId);
        }
    }
}
