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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void closeBotton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text == "" || this.textBox2.Text == "")
            {
                this.label3.Visible = true;
            }
            else
            {
                this.label3.Visible = false;
                try
                {
                    // connection
                    using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                    {
                        cn.Open();
                        // query
                        using (SqlCommand cmd = new SqlCommand("UPDATE Users SET firstName = @FirstName, secondName = @SecondName WHERE userName = @name", cn))
                        {
                            cmd.Parameters.AddWithValue("@FirstName", this.textBox1.Text);
                            cmd.Parameters.AddWithValue("@SecondName", this.textBox2.Text);
                            cmd.Parameters.AddWithValue("@name", UpdateUser.username);
                            int rows = cmd.ExecuteNonQuery();
                            if (rows!=0)
                            {
                                MessageBox.Show("Update done succesfully .");
                                this.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
