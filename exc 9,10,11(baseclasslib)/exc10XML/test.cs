using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace exc10XML
{
    class test
    {
        static void Main(string[] args)
        {
            Person will = new Person("Will", "Smith", 123456);
            XmlSerializer serializer = new XmlSerializer(typeof(Person)) ;
            TextWriter writer = new StreamWriter(@"C:\Temp\XmlSerializationTest.xml");

            serializer.Serialize(writer, will);

            writer.Close();

            Stream readStream = new FileStream(@"C:\Temp\XmlSerializationTest.xml", FileMode.Open, FileAccess.Read, FileShare.None);

            Person readPerson = (Person)serializer.Deserialize(readStream);
            readStream.Close();

            Console.WriteLine(readPerson);
            Console.ReadKey();
        }
    }
}
