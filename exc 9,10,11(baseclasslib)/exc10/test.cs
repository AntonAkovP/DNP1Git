using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;

namespace exc10
{
    class test
    {
        static void Main(string[] args)
        {
            /*Person will = new Person("Will", "Smith", 123456);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"C:\Temp\SerializationTest.bin", FileMode.Create, FileAccess.Write, FileShare.None);

            formatter.Serialize(stream, will);

            stream.Close();

            Stream readStream = new FileStream(@"C:\Temp\SerializationTest.bin", FileMode.Open, FileAccess.Read, FileShare.None);

            Person readPerson = (Person)formatter.Deserialize(readStream);
            readStream.Close();

            Console.WriteLine(readPerson);
            Console.ReadKey();*/

            Person will = new Person("Will", "Smith", 123456);
            IFormatter formatter = new SoapFormatter();
            Stream stream = new FileStream(@"C:\Temp\SoapSerializationTest.bin", FileMode.Create, FileAccess.Write, FileShare.None);

            formatter.Serialize(stream, will);

            stream.Close();

            Stream readStream = new FileStream(@"C:\Temp\SoapSerializationTest.bin", FileMode.Open, FileAccess.Read, FileShare.None);

            Person readPerson = (Person)formatter.Deserialize(readStream);
            readStream.Close();

            Console.WriteLine(readPerson);
            Console.ReadKey();
        }
    }
}
