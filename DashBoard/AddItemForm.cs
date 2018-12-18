using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using DataAccessLayer;

namespace DashBoard
{
    public partial class AddItemForm : Form 
    {
        public int id { get; set; } = 0;
        MainForm main = new MainForm();
        public AddItemForm()
        {
            InitializeComponent();
            main = (MainForm)Application.OpenForms["MainForm"];
        }
        public AddItemForm(int id)
        {
            this.id = id;
            InitializeComponent();
            main = (MainForm)Application.OpenForms["MainForm"];
            using (MyContext db = new MyContext())
            {
                var item = db.Items.Where(i => i.Id == id).FirstOrDefault();
                txtItem.Text = item.Name;
                txtCode.Text = item.BarCode;
                txtPrice.Text = item.Price.ToString();
            }
            
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPrice.Text.Trim() != "" && txtCode.Text.Trim() != "" && txtItem.Text.Trim() != "")
            {
                if (Regex.IsMatch(txtPrice.Text, @"^[0-9]\d{0,9}(\.\d{1,2})?%?$"))
                {
                    if (Regex.IsMatch(txtCode.Text, @"^[0-9]\d{0,9}"))
                    {
                        if (id == 0)
                        {
                            MessageBox.Show(BL.InsertItem(new Item()
                            {
                                BarCode = txtCode.Text,
                                Name = txtItem.Text,
                                Price = Convert.ToDecimal(txtPrice.Text)
                            }));

                            //clear text
                            txtCode.Text = txtItem.Text = txtPrice.Text = string.Empty;
                            RefreshGrid();
                        }
                        else
                        {
                            MessageBox.Show(BL.UpdateItem(new Item()
                            {
                                Id = id,
                                BarCode = txtCode.Text,
                                Name = txtItem.Text,
                                Price = Convert.ToDecimal(txtPrice.Text)
                            }));
                            id = 0;
                            main.Invoke(new MethodInvoker( RefreshGrid));
                            this.Close();
                        }
                       
                    }else
                    {
                        MessageBox.Show("برجاء ادخال الكود صحيح");
                    }
                }
                else
                {
                    MessageBox.Show("برجاء ادخال السعر صحيح");
                }
            }
            else
            {
                MessageBox.Show("برجاء ملئ كل الخانات الفارغه");
            }
        }

        private void RefreshGrid()
        {
            using (MyContext db = new MyContext())
            {
                main.GridViewItems1.DataSource = db.Items.ToList();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim() != "" || txtItem.Text.Trim() != "" || txtPrice.Text.Trim() != "")
            {
                DialogResult ExitCheck;
                ExitCheck = MessageBox.Show("إذا خرجت الآن لن يتم حفظ البيانات الحاليه", "تأكيد خروج", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (ExitCheck == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void AddItemForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //RefreshGrid();
        }
    }
}
