using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Settings
{
    public class Settings
    {
        virtual public string Type { get; set; }
        virtual public string Value { get; set; }
        virtual public int Id { get; set; }
    }
}
