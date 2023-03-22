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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Save save = new Save();
            save.TopLevel = false;
            panel3.Controls.Add(save);
            save.BringToFront();
            save.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetPhoto get = new GetPhoto();
            get.TopLevel = false;
            panel3.Controls.Add(get);
            get.BringToFront();
            get.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void CloseBtn_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddUser n = new AddUser();
            n.TopLevel = false;
            panel3.Controls.Add(n);
            n.BringToFront();
            n.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if(Login.userPermission == "permission 1")
            {
                this.button6.Visible = true;
            }
            else
            {
                this.button6.Visible = false;
            }
            string firstName = "";
            string secondName = "";
            try
            {
                //connection
                using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                {
                    cn.Open();
                    //query
                    using (SqlCommand cmd = new SqlCommand("SELECT firstName , secondName FROM Users WHERE userName = @name", cn))
                    {
                        cmd.Parameters.AddWithValue("@name", Login.userName);
                        // reader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                firstName = reader.GetString(0);
                                secondName = reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.label1.Text = label1.Text + firstName + " " + secondName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateUser p = new UpdateUser();
            p.TopLevel = false;
            panel3.Controls.Add(p);
            p.BringToFront();
            p.Show();

        }
    }
}
