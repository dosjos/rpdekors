using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjecsSalg2.Sales
{
    public class Users
    {
        virtual public int Id { get; set; }
        virtual public string Navn { get; set; }
        virtual public string Rolle { get; set; }
        virtual public Boolean Slettet { get; set; }
    }
}
