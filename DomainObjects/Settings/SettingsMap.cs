using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace DomainObjects.Settings
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
