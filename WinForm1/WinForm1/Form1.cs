using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNP;
using System.Xml.Linq;
using System.Threading;

namespace WinForm1
{
    public partial class Form1 : Form
    {
        private static Boolean runTest = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void addB_Click(object sender, EventArgs e)
        {
            try
            {
                String res = Convert.ToString(MathLib.Sum(Convert.ToDouble(nBox1.Text), Convert.ToDouble(nBox2.Text)));
                resBox.Items.Insert(0, nBox1.Text + " + " + nBox2.Text + " = " + res);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid numbers", "Error converting numbers",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void clrB_Click(object sender, EventArgs e)
        {
            nBox1.Clear();
            nBox2.Clear();
        }

        private void subB_Click(object sender, EventArgs e)
        {
            try
            {
                String res = Convert.ToString(MathLib.Sub(Convert.ToDouble(nBox1.Text), Convert.ToDouble(nBox2.Text)));
                resBox.Items.Insert(0, nBox1.Text + " - " + nBox2.Text + " = " + res);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid numbers", "Error converting numbers",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void mulB_Click(object sender, EventArgs e)
        {
            try
            {
                String res = Convert.ToString(MathLib.Mul(Convert.ToDouble(nBox1.Text), Convert.ToDouble(nBox2.Text)));
                resBox.Items.Insert(0, nBox1.Text + " * " + nBox2.Text + " = " + res);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid numbers", "Error converting numbers",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void divB_Click(object sender, EventArgs e)
        {
            try
            {
                String res = Convert.ToString(MathLib.Div(Convert.ToDouble(nBox1.Text), Convert.ToDouble(nBox2.Text)));
                resBox.Items.Insert(0, nBox1.Text + " / " + nBox2.Text + " = " + res);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid numbers", "Error converting numbers",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearB_Click(object sender, EventArgs e)
        {
            resBox.Items.Clear();
        }

        private void saveB_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDial = new SaveFileDialog();
            saveDial.Filter = "XML files(.xml)|*.xml|all Files(*.*)|*.*";
            saveDial.Title = "Save an Xml File";
            saveDial.ShowDialog();

            if (saveDial.FileName != "")
            {
                XElement element = new XElement("Items");
                foreach (String item in resBox.Items)
                {
                    element.Add(new XElement("item", item));
                }
                XDocument document = new XDocument();
                document.Add(element);

                switch (saveDial.FilterIndex)
                {
                    case 1:
                        document.Save(saveDial.FileName, SaveOptions.DisableFormatting);
                        break;
                    case 2:
                        document.Save(saveDial.FileName+".xml", SaveOptions.DisableFormatting);
                        break;
                }
                    
            }

        }

        private void loadB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDial = new OpenFileDialog();

            openDial.InitialDirectory = "C:\\Temp\\";
            openDial.Filter = "XML files(.xml)|*.xml";
            openDial.RestoreDirectory = true;
            if (openDial.ShowDialog() == DialogResult.OK)
            {
                XDocument document = XDocument.Load(openDial.FileName);
                resBox.Items.Clear();
                foreach (var elem in document.Root.Elements("item"))
                    resBox.Items.Add(elem.Value);
                
            }
        }

        private void stressB_Click(object sender, EventArgs e)
        {
            runTest = true;

            Thread newTestThread = new Thread(testThread);
            newTestThread.IsBackground = true;
            newTestThread.Start();
        }

        private void stopB_Click(object sender, EventArgs e)
        {
            runTest = false;
        }
        private void addItemResBox(int Index, String item)
        {
            resBox.Items.Insert(Index, item);
        }
        private void testThread()
        {
            while(runTest)
            {
                Thread.Sleep(1000);
                Random rand = new Random();

                int n1 = rand.Next(100);
                int n2 = rand.Next(1,100);//so you don't devide by 0
                double res;
                switch(rand.Next(0,4))
                {
                    case 0:
                        res = MathLib.Sum(n1, n2);
                        this.Invoke((MethodInvoker)(() => resBox.Items.Insert(0, n1 + " + " + n2 + " = " + res)));
                        break;
                    case 1:
                        res = MathLib.Sub(n1, n2);
                        this.Invoke((MethodInvoker)(() => resBox.Items.Insert(0, n1 + " - " + n2 + " = " + res)));
                        break;
                    case 2:
                        res = MathLib.Div(n1, n2);
                        this.Invoke((MethodInvoker)(() => resBox.Items.Insert(0, n1 + " / " + n2 + " = " + res)));
                        break;
                    case 3:
                        res = MathLib.Mul(n1, n2);
                        this.Invoke((MethodInvoker)(() => resBox.Items.Insert(0, n1 + " * " + n2 + " = " + res)));
                        break;
                }


            }
        }
    }
}
