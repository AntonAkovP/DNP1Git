using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace exc10XML
{
    [Serializable]
    public class Person : IXmlSerializable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Ssn { get; set; }

        public Person()
        {
            FirstName = "";
            LastName = "";
            Ssn = 0;
        }
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            FirstName = reader.GetAttribute("FirstName");
            LastName = reader.GetAttribute("LastName");
            Ssn = Convert.ToInt32(reader.GetAttribute("Ssn"));

        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("FirstName", FirstName);
            writer.WriteAttributeString("LastName", LastName);
            writer.WriteAttributeString("Ssn", Ssn.ToString());
        }
    }
}
