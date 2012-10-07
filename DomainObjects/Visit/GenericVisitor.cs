using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Visit
{
    public class GenericVisitor
    {
        public virtual DateTime VisitTime { get; set; }
        public virtual int Id { get; set; }
        public virtual String Type { get; set; }

        public GenericVisitor()
        {
            VisitTime = DateTime.Now;
        }
    }
}
