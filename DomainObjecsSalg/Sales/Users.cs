﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjecsSalg.Sales
{
    public class Users
    {
        virtual public int Id { get; set; }
        virtual public string Navn { get; set; }
        virtual public string Rolle { get; set; }
    }
}
