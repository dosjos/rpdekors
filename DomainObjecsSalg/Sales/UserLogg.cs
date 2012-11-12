using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjecsSalg.Sales
{
    public class UserLogg
    {
        virtual public int Id { get; set; }
        virtual public int UserId { get; set; }
        virtual public DateTime Brukstid { get; set; }
    }
}
