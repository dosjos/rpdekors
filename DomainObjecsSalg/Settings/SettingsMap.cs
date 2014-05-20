using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace DomainObjectsSalg.Settings
{
    public class SettingsMap : ClassMap<Settings>
    {
        public SettingsMap(){
            Id(x => x.Id);
            Map(x => x.Type).Unique();
            Map(x => x.Value);
        }
    }
}
