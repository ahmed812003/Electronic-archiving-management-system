using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Electronic_archiving
{
    public partial class Login : Form
    {
        Image image;
        SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;");
        public static string userName, password;
        public static string userPermission="";
        public Login()
        {
            InitializeComponent();
        }

        private void closeBotton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            panel3.BackColor = Color.White;
            textBox2.BackColor = SystemColors.Control;
            panel4.BackColor = SystemColors.Control;
            userError.Visible = false;
            if (textBox2.Text == "")
            {
                passError.Visible = true;
                errorMsg.Text = "Please fill the Form !";
            }

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox1.BackColor = SystemColors.Control;
            panel3.BackColor = SystemColors.Control;
            textBox2.BackColor = Color.White;
            panel4.BackColor = Color.White;
            passError.Visible = false;
            if (textBox1.Text == "")
            {
                userError.Visible = true;
                errorMsg.Text = "Please fill the Form !";
            }
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                userError.Visible = true;
                errorMsg.Text = "Please fill the Form !";
            }
            else
            {
                userError.Visible = false;
                errorMsg.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                passError.Visible = true;
                errorMsg.Text = "Please fill the Form !";
            }
            else
            {
                passError.Visible = false;
                errorMsg.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" && textBox2.Text == "")
            {
                userError.Visible = true;
                passError.Visible = true;
                errorMsg.Text = "Please fill the Form !";
            }
            else if(textBox1.Text == "")
            {
                userError.Visible = true;
                errorMsg.Text = "Please fill the Form !";
            }
            else if(textBox2.Text == "")
            {
                passError.Visible = true;
                errorMsg.Text = "Please fill the Form !";
            }
            else
            {
                userName = textBox1.Text;
                password = textBox2.Text;
                
                try
                {
                    using (SqlConnection connection = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE userName = @name AND password = @password", connection))
                        {
                            command.Parameters.AddWithValue("@name", userName);
                            command.Parameters.AddWithValue("@password", password);
                            int count = (int)command.ExecuteScalar();
                            if (count > 0)
                            {
                                try
                                {
                                    using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                                    {
                                        cn.Open();
                                        using (SqlCommand cmd = new SqlCommand("SELECT permission FROM Users WHERE userName = @name", cn))
                                        {
                                            cmd.Parameters.AddWithValue("@name", Login.userName);
                                            using (SqlDataReader reader = cmd.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    userPermission = reader.GetString(0);
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message + "1");
                                }
                                Form2 f2 = new Form2();
                                f2.Show();
                                this.Visible = false;
                            }
                            else
                            {
                                errorMsg.Text = "Invalid Username or Password !";
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    errorMsg.Text = "Invalid Username or Password !";
                }
            }

        }
   
    
    }
}
