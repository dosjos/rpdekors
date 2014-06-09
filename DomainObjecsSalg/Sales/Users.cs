using System;

namespace DomainObjectsSalg.Sales
{
    public class Users
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Rolle { get; set; }
        public Boolean Slettet { get; set; }
    }
}
