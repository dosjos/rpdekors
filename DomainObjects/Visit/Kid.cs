using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

namespace CafeTerminal.DomainObjects
{
    public class Kid
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Postcode { get; set; }
        public virtual string Gender { get; set; }

        /*Optional*/
        public virtual string TLF { get; set; }
        public virtual string Email { get; set; }
        public virtual int Age { get; set; }
        public virtual string Ethnisity { get; set; }

    }
}
