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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Electronic_archiving
{
    public partial class AddUser : Form
    {
        string permission = "";
        public AddUser()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text == "" || this.textBox2.Text == "" || permission =="" || this.textBox3.Text == "" || this.textBox4.Text == "")
            {
                label3.Visible = true;
            }
            else
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                    {
                        cn.Open();
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES (@first , @second , @name , @password , @permission)", cn))
                        {
                            cmd.Parameters.AddWithValue("@first", this.textBox4.Text);
                            cmd.Parameters.AddWithValue("@second", this.textBox3.Text);
                            cmd.Parameters.AddWithValue("@name", this.textBox1.Text);
                            cmd.Parameters.AddWithValue("@password", this.textBox2.Text);
                            cmd.Parameters.AddWithValue("@permission", this.permission);
                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                                MessageBox.Show("user inserted successfully.");
                            else
                                MessageBox.Show("No user inserted.");
                            this.textBox1.Text = "";
                            this.textBox2.Text = "";
                            this.textBox3.Text = "";
                            this.textBox4.Text = "";
                            this.radioButton1.Checked = false;
                            this.radioButton2.Checked = false;
                            this.radioButton3.Checked = false;
                            this.radioButton4.Checked = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            permission = "permission 1";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            permission = "permission 2";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            permission = "permission 3";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            permission = "permission 4";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

    }
}
