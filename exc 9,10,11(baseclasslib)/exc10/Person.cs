using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace exc10
{
    [Serializable]
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Ssn { get; set; }

        public Person(string fn, string ln, int sn)
        {
            FirstName = fn;
            LastName = ln;
            Ssn = sn;
        }

        public override string ToString()
        {
            return FirstName + ' ' + LastName + ' ' + Ssn;
        }

       
    }
}
