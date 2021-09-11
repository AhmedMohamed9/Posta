using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Posta.Forms;
using Posta.model;
namespace Posta
{
    public partial class Form1 : Form
    {
        private Form activeform;
        private model.HadaryEntities1 db = new HadaryEntities1();
        public Form1()
        {
            InitializeComponent();
            openchildForm(new Forms.Orders());
        }
        private void openchildForm(Form childform/*,object btnsender*/)
        {
            if (activeform != null)
            {
                activeform.Close();
            }
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childform);
            this.panelDesktop.Tag = childform;
            childform.BringToFront();
            childform.Show();
           
        }
        private void btnORders_Click(object sender, EventArgs e)
        {
            this.labelHeader.Text = "الاوردرات";
            panel1.BackColor = Color.FromArgb(25,51,76);
            openchildForm(new Forms.Orders());
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.labelHeader.Text = "العملاء";
            panel1.BackColor = Color.FromArgb(76, 25, 25);
            openchildForm(new Forms.customer());
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.labelHeader.Text = "حسابات";
            panel1.BackColor = Color.FromArgb(76, 74, 25);
            openchildForm(new Forms.Calculations());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog op = new FolderBrowserDialog();

            if (op.ShowDialog() == DialogResult.OK)
            {

                string path = op.SelectedPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".mdf";

                db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction,
                    @"exec baku2 @dis = N'" + path + "'");
                MessageBox.Show("تم نسخ الداتا بنجاح","Back up",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.labelHeader.Text = "مندوبين الشحن";
            panel1.BackColor = Color.FromArgb(74, 25, 76);
            openchildForm(new Forms.Employee());
            
        }
    }
}
