using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DomainObjectsSalg.Sales;

namespace CafeTerminal.DataAccess
{
    public class SalgDbContext : DbContext
    {

        public DbSet<Svinn> Svinn { get; set; }

        public SalgDbContext()
            :base("SalgDatabase")
        {
            
        }

    }
}
