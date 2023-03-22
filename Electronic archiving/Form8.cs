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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void closeBotton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text == "")
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
                        using (SqlCommand cmd = new SqlCommand("UPDATE Users SET password = @Password WHERE userName = @name", cn))
                        {
                            cmd.Parameters.AddWithValue("@Password", this.textBox1.Text);
                            cmd.Parameters.AddWithValue("@name", UpdateUser.username);
                            int rows = cmd.ExecuteNonQuery();
                            if (rows != 0)
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
