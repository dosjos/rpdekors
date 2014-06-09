using System;

namespace DomainObjectsSalg.Sales
{
    public class UserLogg
    {
       public int Id { get; set; }
       public int UsersId { get; set; }
        public virtual Users Users { get; set; }
       public DateTime Brukstid { get; set; }
    }
}
