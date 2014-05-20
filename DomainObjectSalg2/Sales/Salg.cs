using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjecsSalg2.Sales
{
    public class Salg
    {
        virtual public int Id { get; set; }
        virtual public int VareId { get; set; }
        virtual public DateTime SlagsTid { get; set; }
        virtual public int Pris { get; set; }
    }
}
