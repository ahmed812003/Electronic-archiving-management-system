using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Electronic_archiving
{
    public partial class UpdateUser : Form
    {
        string  firstName="" , secondName="" , password=""  , Permission="" ;
        public static string username = "";

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox4.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                Form7 f = new Form7();
                f.Show();
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
                this.textBox5.Text = "";
            }          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                Form8 f = new Form8();
                f.Show();
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
                this.textBox5.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                Form9 f = new Form9();
                f.Show();
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
                this.textBox5.Text = "";
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox4.UseSystemPasswordChar = false;
        }


        public UpdateUser()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //connection
                using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                {
                    cn.Open();
                    //query
                    using (SqlCommand cmd = new SqlCommand("SELECT firstName , secondName , password , Permission FROM Users WHERE userName = @name", cn))
                    {
                        username = this.textBox1.Text;
                        cmd.Parameters.AddWithValue("@name", this.textBox1.Text);
                        // reader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                firstName = reader.GetString(0);
                                secondName = reader.GetString(1);
                                password = reader.GetString(2);
                                Permission = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            if(firstName =="" || secondName=="" || Permission=="" || password == "")
            {
                this.label10.Visible = true;
            }
            else
            {
                this.label10.Visible = false;
                textBox2.Text = firstName;
                textBox3.Text = secondName;
                textBox4.Text = password;
                textBox5.Text = Permission;
            }
        }
    }
}
