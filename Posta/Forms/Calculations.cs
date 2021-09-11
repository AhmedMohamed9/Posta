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
    public partial class Calculations : Form
    {
        HadaryEntities1 db = new HadaryEntities1();
        public Calculations()
        {
            InitializeComponent();
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var start = metroDateTime1.Value.Date;
            var end = metroDateTime2.Value.Date;
            var ordrs = (from ord in db.Orders
                         where ord.date >= start && ord.date<=end

                         select new
                         {
                             ord.id,
                             ord.custimer.name,
                             ord.recived_Name,
                             ord.place,
                             ord.date,
                             ord.price,
                             ord.Deliver_Price,
                             ord.recived_phone1,
                             ord.recived_phone2
                         }).ToList();
            var count = ordrs.Count();
            var sum = ordrs.Select(s => s.Deliver_Price).Sum();
            dataGridView1.DataSource = ordrs;
            textBox1.Text = count.ToString();
            textBox2.Text = sum.ToString();
            set_datagrid();
        }
        private void set_datagrid()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "الاسم";
            dataGridView1.Columns[2].HeaderText = "اسم المستلم";
            dataGridView1.Columns[3].HeaderText = "عنوان التسليم";
            dataGridView1.Columns[4].HeaderText = "تاريخ";
            dataGridView1.Columns[5].HeaderText = "السعر";
            dataGridView1.Columns[6].HeaderText = "سعر التوصيل";
            dataGridView1.Columns[7].HeaderText = "تليفون";
            dataGridView1.Columns[8].HeaderText = "تليفون2";
        }
    }
}
