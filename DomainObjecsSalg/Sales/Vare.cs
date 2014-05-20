using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjectsSalg.Sales
{
    public class Vare
    {
        virtual public int Id { get; set; }
        virtual public int Pris { get; set; }
        virtual public string Navn { get; set; }
        virtual public Boolean CurrentlyInUse { get; set; }
        virtual public int Rank { get; set; }
        virtual public System.Drawing.Color Farge { get; set; }

    }
}
