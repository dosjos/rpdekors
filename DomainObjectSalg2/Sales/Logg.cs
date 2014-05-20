using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjecsSalg2.Sales
{
    public class Logg
    {
        virtual public int Id { get; set; }
        virtual public DateTime LoggTid { get; set; }
        virtual public string Text { get; set; }
    }
}
