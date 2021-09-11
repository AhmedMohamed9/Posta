using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Posta.model;
namespace Posta.Forms
{
    public partial class customer : Form
    {
        HadaryEntities1 db = new HadaryEntities1();
        public customer()
        {
            InitializeComponent();
            getAllCus();
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "الاسم";
            dataGridView1.Columns[2].HeaderText = "العنوان";
            dataGridView1.Columns[3].HeaderText = "رقم التليفون";
            dataGridView1.Columns[4].HeaderText = "رقم التليفون 2";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkinputs())
            {
                var cus = new model.custimer()
                {
                    name = textBoxآِName.Text,
                    address = textBoxplace.Text,
                    phone1 = textBoxPhone.Text,
                    phone2 = textBoxPhone2.Text
                };
                db.custimers.Add(cus);
                db.SaveChanges();
                getAllCus();
                clear();
            }
          
        }
        private void getAllCus()
        {
            this.dataGridView1.DataSource = db.custimers.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            this.textBoxآِName.Text = string.Empty;
            this.textBoxplace.Text = string.Empty;
            this.textBoxPhone.Text = string.Empty;
            this.textBoxPhone2.Text = string.Empty;
            this.textBox3.Text = string.Empty;
            this.textBoxآِName.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count>0)
            {
                var cusId = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                if (DialogResult.Yes == MessageBox.Show("متاكد من حذف " + this.dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    // do what u want
                    var cus = db.custimers.Find(Int32.Parse(cusId));
                    db.custimers.Remove(cus);
                    db.SaveChanges();
                    getAllCus();
                    clear();
                }
            }
            else
            {
                MessageBox.Show("اختار العميل الى عاوز تحذفه");
            }
           
            
        }

        private bool checkinputs()
        {
            if (textBoxآِName.Text == "")
            {
                MessageBox.Show("ادخل اسم العميل", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxآِName.Focus();
                return false;
            }
            if (textBoxplace.Text == "")
            {
                MessageBox.Show("ادخل عنوان العميل", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxplace.Focus();
                return false;
            }
            if (textBoxPhone.Text == "")
            {
                MessageBox.Show("ادخل رقم هاتق العميل", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPhone.Focus();
                return false;
            }
            
            return true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow !=null)
            {
                label6.Text = "تعديل بيانات العميل";
                textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBoxآِName.Text  = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBoxplace.Text  = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBoxPhone.Text  = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBoxPhone2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                buttonUpdate.Visible = true;
                buttonُExitUpdate.Visible = true;
                
            }
            else
            {
                MessageBox.Show("اختار العميل الى عاوز تعدل بياناته","تحذير",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
           

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (checkinputs())
            {
                int cusId = Int32.Parse(textBox3.Text);
                var customer = db.custimers.Where(x => x.id ==cusId).FirstOrDefault();
                customer.name = textBoxآِName.Text;
                customer.address = textBoxplace.Text;
                customer.phone1 = textBoxPhone.Text;
                customer.phone2 = textBoxPhone2.Text;
               
                
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("تم التعديل","تم التعديل",MessageBoxButtons.OK,MessageBoxIcon.Information);
                clear();
                buttonUpdate.Visible = false;
                buttonُExitUpdate.Visible = false;
                getAllCus();
            }
        }

        private void buttonُExitUpdate_Click(object sender, EventArgs e)
        {
            buttonUpdate.Visible = false;
            buttonُExitUpdate.Visible = false;
            clear();
            label6.Text = "اضافةعميل";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                label6.Text = "تعديل بيانات العميل";
                textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBoxآِName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBoxplace.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBoxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBoxPhone2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                buttonUpdate.Visible = true;
                buttonُExitUpdate.Visible = true;

            }
            else
            {
                MessageBox.Show("اختار العميل الى عاوز تعدل بياناته", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
        }

        private void textBoxPhone2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonCusORder_Click(object sender, EventArgs e)
        {
            int cusid =Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
           
            Form ordr = new Orders(cusid);
            



            ordr.ShowDialog();
            
        }
    }
}
