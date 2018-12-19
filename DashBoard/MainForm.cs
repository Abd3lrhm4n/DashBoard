using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using DataAccessLayer;

namespace DashBoard
{
    public partial class MainForm : Form
    {
        string pathToFile = "";

        public MainForm()
        {
            Thread t = new Thread(new ThreadStart(LoadingForm));
            t.Start();
            this.Hide();
            InitializeComponent();
            t.Abort();
            this.Show();
        }

        public void LoadingForm()
        {
            Application.Run(new LoadingForm());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
          
            using (MyContext ctx = new MyContext())
            {
                try
                {
                    if (ctx.Items.Any())
                    {
                        GridViewItems.DataSource = ctx.Items.ToList();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Excel File";
            theDialog.Filter = "Excel FILE|*.xlsx";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(theDialog.FileName.ToString());
                textBox1.Text = theDialog.FileName.ToString();
                pathToFile = theDialog.FileName;//doesn't need .tostring because .filename returns a string// saves the location of the selected object

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ExcelData().ReadExcel(pathToFile, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new AddItemForm().ShowDialog();
        }

        public int Id { get; set; } = 0;
        public string name { get; set; }
        public string barcode { get; set; }
        public string price { get; set; }


        private void GirdViewItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridViewItems.Rows.Count > 0)
            {
                Id = Convert.ToInt32(GridViewItems.Rows[e.RowIndex].Cells[0].Value);
                if (Id > 0)
                {
                    name = GridViewItems.Rows[e.RowIndex].Cells[2].Value.ToString();
                    barcode = GridViewItems.Rows[e.RowIndex].Cells[1].Value.ToString();
                    price = GridViewItems.Rows[e.RowIndex].Cells[3].Value.ToString();

                    new MidForm().ShowDialog();
                }
            }


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            using (MyContext db = new MyContext())
            {
                GridViewItems.DataSource = db.Items.Where(x => x.Name.Contains(textBox2.Text) || x.BarCode.Contains(textBox2.Text)).ToList();
            }
        }

        private void إضافةصنفجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddItemForm().ShowDialog();
        }
    }
}
