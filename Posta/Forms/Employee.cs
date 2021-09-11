using Posta.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Posta.Forms
{
    public partial class Employee : Form
    {
        HadaryEntities1 db = new HadaryEntities1();
        public Employee()
        {
            InitializeComponent();
            getAllCus();
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "المحافظة";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkinputs())
            {
                var city = new model.Citiezen()
                {
                    Name = textBoxآِName.Text,
                    
                  
                };
                db.Citiezens.Add(city);
                db.SaveChanges();
                getAllCus();
                clear();
            }
        }
        private void getAllCus()
        {
            this.dataGridView1.DataSource = db.Citiezens.ToList();
        }
        private bool checkinputs()
        {
            if (textBoxآِName.Text == "")
            {
                MessageBox.Show("ادخل اسم المحافظة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxآِName.Focus();
                return false;
            }
           
         

            return true;
        }
        private void clear()
        {
            this.textBoxآِName.Text = string.Empty;
        
            this.textBox3.Text = string.Empty;
            this.textBoxآِName.Focus();
        }

        //private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
        //}

        //private void textBoxPhone2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
        //}

        private void buttonُExitUpdate_Click(object sender, EventArgs e)
        {
            buttonUpdate.Visible = false;
            buttonُExitUpdate.Visible = false;
            clear();
            label6.Text = "اضافةعميل";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (checkinputs())
            {
                int cityId = Int32.Parse(textBox3.Text);
                var city = db.Citiezens.Where(x => x.id == cityId).FirstOrDefault();
                city.Name = textBoxآِName.Text;

               db.Entry(city).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("تم التعديل", "تم التعديل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                buttonUpdate.Visible = false;
                buttonُExitUpdate.Visible = false;
                getAllCus();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (this.dataGridView1.CurrentRow != null)
            {
                label6.Text = "تعديل بيانات العميل";
                textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBoxآِName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                buttonUpdate.Visible = true;
                buttonُExitUpdate.Visible = true;

            }
            else
            {
                MessageBox.Show("اختار المحافظة الى عاوز تعدل بياناتها", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                label6.Text = "تعديل بيانات المحافظة";
                textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBoxآِName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                buttonUpdate.Visible = true;
                buttonُExitUpdate.Visible = true;

            }
            else
            {
                MessageBox.Show("اختار العميل الى عاوز تعدل بياناته", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                var empId = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                if (DialogResult.Yes == MessageBox.Show("متاكد من حذف " + this.dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    // do what u want
                    var emp = db.Citiezens.Find(Int32.Parse(empId));
                    db.Citiezens.Remove(emp);
                    db.SaveChanges();
                    getAllCus();
                    clear();
                }
            }
            else
            {
                MessageBox.Show("اختار المحافظة الى عاوز تحذفها");
            }
        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }
    }
}
