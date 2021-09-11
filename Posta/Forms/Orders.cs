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
    public partial class Orders : Form
    {
        HadaryEntities1 db = new HadaryEntities1();
        int cstid;
        
        public Orders(int custid=0)
        {
            InitializeComponent();
            
            cstid = custid;
            getAllOrders();
            this.metroComboBox1.DataSource = db.custimers.ToList();
            metroComboBox1.DisplayMember = "name";
            metroComboBox1.ValueMember = "id";
            this.metroComboBox2.DataSource = db.Citiezens.ToList();
            metroComboBox2.DisplayMember = "Name";
            metroComboBox2.ValueMember = "id";

            var cites= db.Citiezens.ToList();
            cites.Insert(0,new Citiezen { id = 0, Name = "كل المحافظات" });
            this.metroComboBox3.DataSource = cites;
            metroComboBox3.DisplayMember = "Name";
            metroComboBox3.ValueMember = "id";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[1].HeaderText = "الاسم";
            dataGridView1.Columns[2].HeaderText = "اسم المستلم";
            dataGridView1.Columns[3].HeaderText = "عنوان التسليم";
            dataGridView1.Columns[4].HeaderText = "تاريخ";
            dataGridView1.Columns[5].HeaderText = "السعر";
            dataGridView1.Columns[6].HeaderText = "سعر التوصيل";
            dataGridView1.Columns[7].HeaderText = "تليفون";
            dataGridView1.Columns[8].HeaderText = "تليفون2";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow !=null)
            {
                var ordId = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                if (DialogResult.Yes == MessageBox.Show("متاكد من حذف " + this.dataGridView1.CurrentRow.Cells[1].Value.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    // do what u want
                    var ord = db.Orders.Find(Int32.Parse(ordId));
                    db.Orders.Remove(ord);
                    db.SaveChanges();
                    getAllOrders();
                    clear();
                }
            }
            else
            {
                MessageBox.Show("اختار العميل الى عاوز تحذفه");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkinputs())
            {
                var order = new Order()
                {
                    Cus_ID = Int32.Parse(metroComboBox1.SelectedValue.ToString()),
                    City_id=Int32.Parse(metroComboBox2.SelectedValue.ToString()),
                    place = textBoxplace.Text,
                    date = dateTimePicker1.Value,
                    price = decimal.Parse(textBoxMoney.Text),
                    Deliver_Price = decimal.Parse(textBoxMoneyDeliver.Text),
                    Note = textBoxNotes.Text,
                    recived_Name=textBoxRecived.Text,
                    recived_phone1=textBoxPhone1.Text,
                    recived_phone2=textBoxphone2.Text
                };
               
                    db.Orders.Add(order);
                    db.SaveChanges();
                getAllOrders();
                clear();
            }
        }
        private bool checkinputs()
        {
            decimal money;
            if (metroComboBox1.SelectedValue == null)
            {
                MessageBox.Show("اختار العميل", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                metroComboBox1.Focus();
                return false;
            }
           
            if (textBoxplace.Text == "")
            {
                MessageBox.Show("ادخل عنوان التسليم", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxplace.Focus();
                return false;
            }
            if (textBoxRecived.Text == "")
            {
                MessageBox.Show("ادخل اسم التسليم", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxRecived.Focus();
                return false;
            }
            if (textBoxPhone1.Text == "")
            {
                MessageBox.Show("ادخل رقم المستلم", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPhone1.Focus();
                return false;
            }
            
            if (textBoxMoney.Text == ""||!Decimal.TryParse(textBoxMoney.Text,out money))
            {
                MessageBox.Show("ادخل قيمة الشحنه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxMoney.Focus();
                return false;
            }
            if (textBoxMoneyDeliver.Text == "" || !Decimal.TryParse(textBoxMoneyDeliver.Text, out money))
            {
                MessageBox.Show("ادخل قيمة التوصيل", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxMoneyDeliver.Focus();
                return false;
            }
           

            return true;
        }
        public void getAllOrders()
        {
           var query = from ord in db.Orders
                    orderby ord.id descending
                    select new { 
                        ord.id, ord.custimer.name, ord.recived_Name,city= ord.Citiezen.Name, ord.date, ord.price, ord.Deliver_Price, ord.recived_phone1, ord.recived_phone2,ord.Note,ord.place };

            if (cstid != 0)
            {
                query = from ordrs in db.Orders
                        where ordrs.Cus_ID == cstid
                        orderby ordrs.id descending
                        select new { ordrs.id, ordrs.custimer.name, ordrs.recived_Name, city = ordrs.Citiezen.Name, ordrs.date, ordrs.price, ordrs.Deliver_Price, ordrs.recived_phone1, ordrs.recived_phone2,ordrs.Note,ordrs.place };
            }
            var res= query.ToList();
            this.dataGridView1.DataSource = res;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
            
         
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxMoneyDeliver_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EditRow();
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            EditRow();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditRow();
        }
        private void EditRow()
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                label6.Text = "تعديل بيانات الاوردر";
                textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                metroComboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBoxRecived.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                metroComboBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBoxplace.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                this.dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                textBoxMoney.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBoxMoneyDeliver.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBoxPhone1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBoxphone2.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                textBoxNotes.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                buttonUpdate.Visible = true;
                buttonُExitUpdate.Visible = true;

            }
            else
            {
                MessageBox.Show("اختار العميل الى عاوز تعدل بياناته", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void clear()
        {
            textBox3.Text = "";
            textBoxplace.Text = "";
            textBoxMoney.Text = "";
            textBoxMoneyDeliver.Text = "";
            textBoxNotes.Text = "";
            textBoxRecived.Text = "";
            textBoxPhone1.Text = "";
            textBoxphone2.Text = "";
           
            buttonUpdate.Visible = false;
            buttonُExitUpdate.Visible = false;
            label6.Text = "أضافة عميل";
        }

        private void buttonُExitUpdate_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void dataGridView1_DoubleClick_1(object sender, EventArgs e)
        {
            EditRow();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            search();
            metroComboBox3.SelectedIndex = 0;
        }
        private void search()
        {
            if (textBoxSearch.Text != "")
            {
                string word = textBoxSearch.Text;
                var querysearch = from ord in db.Orders
                                  where ord.custimer.name.Contains(word) || ord.place.Contains(word) || ord.recived_Name.Contains(word)
                                  || ord.recived_phone1.Contains(word) || ord.recived_phone2.Contains(word)
                                  select new { ord.id, ord.custimer.name, ord.recived_Name, city = ord.Citiezen.Name, ord.date, ord.price, ord.Deliver_Price, ord.recived_phone1, ord.recived_phone2 };
               
                this.dataGridView1.DataSource = querysearch.ToList();
                button5.Visible = true;
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPhone1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46;
        }

        private void textBoxphone2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (checkinputs())
            {
                int ord_id = Int32.Parse(textBox3.Text);
                var ord = db.Orders.Where(s=>s.id==ord_id).FirstOrDefault();


                ord.Cus_ID = Int32.Parse(metroComboBox1.SelectedValue.ToString());
                ord.City_id = Int32.Parse(metroComboBox2.SelectedValue.ToString());
                ord.place = textBoxplace.Text;
                ord.date = dateTimePicker1.Value;
                ord.price = decimal.Parse(textBoxMoney.Text);
                ord.Deliver_Price = decimal.Parse(textBoxMoneyDeliver.Text);
                ord.Note = textBoxNotes.Text;
                ord.recived_Name = textBoxRecived.Text;
                ord.recived_phone1 = textBoxPhone1.Text;
                ord.recived_phone2 = textBoxphone2.Text;
                db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("تم التعديل بنجاح");
                
                getAllOrders();
                clear();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            getAllOrders();
            textBoxSearch.Text = string.Empty;
            metroComboBox3.SelectedIndex = 0;
        }

        private void Orders_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void metroComboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == string.Empty)
            {
                if (metroComboBox3.SelectedValue.ToString() == "0")
                {
                    getAllOrders();
                }
                else
                {
                  int City_id = 0;
                    if (int.TryParse(metroComboBox3.SelectedValue.ToString(), out City_id));

                        var query = from ord in db.Orders
                                where ord.City_id == City_id
                                    orderby ord.id descending
                                select new
                                {
                                    ord.id,
                                    ord.custimer.name,
                                    ord.recived_Name,
                                    city = ord.Citiezen.Name,
                                    ord.date,
                                    ord.price,
                                    ord.Deliver_Price,
                                    ord.recived_phone1,
                                    ord.recived_phone2,
                                    ord.Note,
                                    ord.place
                                };

                    if (cstid != 0)
                    {
                        query = from ordrs in db.Orders
                                where ordrs.Cus_ID == cstid
                                orderby ordrs.id descending
                                select new { ordrs.id, ordrs.custimer.name, ordrs.recived_Name, city = ordrs.Citiezen.Name, ordrs.date, ordrs.price, ordrs.Deliver_Price, ordrs.recived_phone1, ordrs.recived_phone2, ordrs.Note, ordrs.place };
                    }
                    var res = query.ToList();
                    this.dataGridView1.DataSource = res;
                }
            }
            else
            {
                if (metroComboBox3.SelectedValue.ToString() == "0")
                {
                    search();
                }
                else
                {
                    string word = textBoxSearch.Text;
                    int City_id = 0;
                    if (int.TryParse(metroComboBox3.SelectedValue.ToString(), out City_id)) ;
                    var orders_city = db.Orders.Where(s => s.City_id == City_id).ToList();
                    var querysearch = from ord in orders_city
                                      where ord.custimer.name.Contains(word) || ord.place.Contains(word) || ord.recived_Name.Contains(word)
                                      || ord.recived_phone1.Contains(word) || ord.recived_phone2.Contains(word)
                                      select new { ord.id, ord.custimer.name, ord.recived_Name, city = ord.Citiezen.Name, ord.date, ord.price, ord.Deliver_Price, ord.recived_phone1, ord.recived_phone2 };

                    this.dataGridView1.DataSource = querysearch.ToList();
                    button5.Visible = true;
                }

            }
        }
    }
}
