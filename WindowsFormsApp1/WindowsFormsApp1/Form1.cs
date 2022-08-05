using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Query_lambda(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var data = db.ABS.Where(p => p.BDATE >= new DateTime(2022, 1, 1) && p.BDATE <= new DateTime(2022, 1, 31));
            //var hours = data.First().TOL_HOURS;
            //MessageBox.Show(hours.ToString());
            dataGridView1.DataSource = data;
        }

        private void Query_SQLite(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var data = from a in db.ABS
                       join b in db.BASE on a.NOBR equals b.NOBR
                       where a.BDATE >= new DateTime(2022, 1, 1) && a.BDATE <= new DateTime(2022, 1, 31)
                       select new TestDto { 員工編號 = a.NOBR, 員工姓名 = b.NAME_C, 請假日期 = a.BDATE, 開始時間 = a.BTIME, 結束時間 = a.ETIME };
            dataGridView1.DataSource = data;
        }

        private void ADO(object sender, EventArgs e)
        {
            var sqlconnection = new SqlConnection(Properties.Settings.Default.EMCCNConnectionString);
            using (sqlconnection)
            {
                if (sqlconnection.State != ConnectionState.Open)
                    sqlconnection.Open();

                //SqlCommand sqlCommand = new SqlCommand(@"select * from ABS 
                //                    where BDATE>='20220101' and BDATE<='20220131'
                //                    and NOBR='H000003'", sqlconnection);
                //data太大會卡住
                SqlCommand sqlCommand = new SqlCommand(textBox1.Text, sqlconnection);

                var dr = sqlCommand.ExecuteReader();
                var dataTable = new DataTable("ABS");
                dataTable.Load(dr);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void CleanALL(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();  //全部刪除
        }
        private void CleanROWS(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();   //預留標題
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //建構子
            Form2 f2 = new Form2(textBox2.Text);
            f2.Show();
            //this.Hide();
        }
        public Form1(string sMsg)
        {
            InitializeComponent();
            textBox2.Text = sMsg;
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
    public class TestDto
    {   
        public string 員工編號 { get; set; }
        public string 員工姓名 { get; set; }
        public DateTime 請假日期 { get; set; }
        public string 開始時間 { get; set; }
        public string 結束時間 { get; set; }
    }
}
