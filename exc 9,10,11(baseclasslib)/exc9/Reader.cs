using System;
using System.Threading;
using System.IO;


namespace exc9
{
    class Reader
    {
        string fileName;
        public string data;

        public Reader(string fn) { fileName = fn; }

        public void Read()
        {
            FileStream s = new FileStream(fileName, FileMode.Open);
            StreamReader r = new StreamReader(s);
            data = r.ReadToEnd();
            r.Close();
            s.Close();
        }
    }
}