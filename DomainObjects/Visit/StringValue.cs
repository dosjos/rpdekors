using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjects.Visit
{
    public class StringValue
    {
        public StringValue(string s)
        {
            Value = s;
        }
        public string Value { get { return _value; } set { _value = value; } }
        string _value;
    }

}
