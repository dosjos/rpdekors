using System;

namespace DomainObjectsSalg.Sales
{
    public class Vare
    {
        public int Id { get; set; }
        public int Pris { get; set; }
        public string Navn { get; set; }
        public Boolean CurrentlyInUse { get; set; }
        public int Rank { get; set; }
        public int Farge { get; set; }
    }
}
