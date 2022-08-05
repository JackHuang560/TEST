using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2(string sMsg)
        {
            InitializeComponent();
            textBox1.Text = sMsg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(textBox1.Text);
            f1.Show();
        }
    }
}
