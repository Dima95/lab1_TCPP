using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace first
{
    public partial class Form1 : Form
    {
        Calculation c = new Calculation();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            chart1.ChartAreas[0].AxisX.Minimum = 0.5;
            chart1.ChartAreas[0].AxisX.Maximum = 1.5;
            chart1.ChartAreas[0].AxisX.IsLabelAutoFit = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();            
            if (textBox1.Text == "") return;
            try
            {
                c = (Calculation)Int64.Parse(textBox1.Text);
                label1.Text = c.ToString();
                chart1.Series["Nulls"].Points.AddY(c.GetsNulls);
                chart1.Series["Units"].Points.AddY(c.GetUnits);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть, будь ласка, ціле число ", "Помилка");
                textBox1.Text = "";
                label1.Text = "";
            }
            catch (OverflowException)
            {
                MessageBox.Show("Ввдено завелике число ", "Помилка");
                textBox1.Text = "";
                label1.Text = "";                
            }
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
    class Calculation
    {
        private Int64 numb;
        private Int64 Units = 0,Nulls = 0;
        Stack<Char> NumbChar = new Stack<Char>();
        public Calculation() { }
        private Calculation(Int64 i)
        {
            numb = i;
            Convert();
        }
        public Int64 GetUnits
        {
            get { return Units; }
        }
        public Int64 GetsNulls
        {
            get { return Nulls; } 
        }
        void ToInt64(Int64 n)
        {
            numb = n;
        }
        public void Convert()
        {
            if (numb == 0)
            {
                NumbChar.Push('0');
                return;
            }
            Int64 n = Math.Abs(numb); 
            Int64 N;
            while (n >= 1)
            {
                N = n % 2;
                if (N == 0) { NumbChar.Push('0'); Nulls++; }
                else { NumbChar.Push('1'); Units++; }
                n /= 2; 
            }
        } 
        public static explicit operator Calculation(Int64 n)
        {
            return new Calculation(n);
        }
        public override string ToString()
        {
            String s = null;   
            foreach (Char x in NumbChar) s += x;
            if (numb >= 0)
                return s;
            else return String.Format("{0}{1}","-",s); 
        }
    }
}
