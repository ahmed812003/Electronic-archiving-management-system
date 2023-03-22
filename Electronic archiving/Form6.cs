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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void closeBotton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                this.label3.Visible = true;
            }
            else
            {
                this.label3.Visible = false ;
                string name = textBox1.Text;
                using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                {
                    cn.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE Image SET description = @value1 WHERE name = @photoName;", cn))
                    {
                        command.Parameters.AddWithValue("@PhotoName", GetPhoto.nameOfPhoto);
                        command.Parameters.AddWithValue("@value1", name);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Update done succesfully .");
                    this.Close();
                }
            }
            
        }
    }
}
