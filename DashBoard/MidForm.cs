using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;
using BussinessLayer;
namespace DashBoard
{
    public partial class MidForm : Form
    {
        MainForm main = new MainForm();
        public MidForm()
        {
            main = (MainForm)Application.OpenForms["MainForm"];
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (MyContext db= new MyContext())
            {
                Item item = db.Items.FirstOrDefault(x => x.Id == main.Id);
                try
                {
                    BL.DeleteItem(item);
                    MessageBox.Show($"{item.Name} تم حذف");
                    main.GridViewItems1.DataSource = db.Items.ToList();
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("حدث خطأ");
                    this.Close();
                }
                main.Id = 0;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
           Task.Run(() => new AddItemForm(main.Id).ShowDialog());
            
        }

    }
}
