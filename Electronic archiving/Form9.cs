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
    public partial class Form9 : Form
    {
        string newPermission = "";
        public Form9()
        {
            InitializeComponent();
        }

        private void closeBotton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (newPermission == "")
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
                        using (SqlCommand cmd = new SqlCommand("UPDATE Users SET Permission = @Password WHERE userName = @name", cn))
                        {
                            cmd.Parameters.AddWithValue("@Password", newPermission);
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            newPermission = "permission 1";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            newPermission = "premission 2";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            newPermission = "premission 3";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            newPermission = "permission 4";
        }
    }
}
