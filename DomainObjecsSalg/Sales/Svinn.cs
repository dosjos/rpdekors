using System;

namespace DomainObjectsSalg.Sales
{
    class Svinn
    {
        public int SvinnId { get; set; }
        public string Navn { get; set; }
        public decimal Sum { get; set; }
        public string Kommentar { get; set; }
        //[Datatype(Datatype.Date)]
        public DateTime DatoTidspunkt { get; set; }
        public DateTime RegistrertTidspunkt { get; set; }
    }
}
