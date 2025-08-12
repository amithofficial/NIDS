using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace Regression.Linear
{
    public partial class adminlogin : Form
    {
        BaseConnection con=new BaseConnection();

        public adminlogin()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
                //string query = "select * from login where username='" + textBox2.Text + "'";
                //SqlDataReader sd = con.ret_dr(query);
                //if (sd.Read())
                //{
                //    if ((textBox1.Text == sd[0].ToString()) && textBox2.Text == sd[1].ToString())
                //    {

                   if ((textBox1.Text == "admin") && textBox2.Text == "admin")
                  {

                        Form1 obj = new Form1();
                        ActiveForm.Hide();
                        obj.Show();
                    }
                //}
                else
                {
                    MessageBox.Show("Invalid Password or username...");
                //    textBox1.Text = "";
                //    textBox2.Text = "";
                }
            
        }

        private void adminlogin_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            RegisterForm obj = new RegisterForm();
            obj.Show();
        }

        
       

  
    }
}
