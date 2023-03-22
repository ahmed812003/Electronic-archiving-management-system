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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void closeBotton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
            {
                cn.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Image SET date = @value1 WHERE name = @photoName;", cn))
                {
                    command.Parameters.AddWithValue("@PhotoName", GetPhoto.nameOfPhoto);
                    command.Parameters.AddWithValue("@value1", date);
                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Update done succesfully .");
        }
    }
}
