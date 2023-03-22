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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace Electronic_archiving
{
    public partial class GetPhoto : Form
    {
        public static List<string> photos = new List<string>();
        public static string nameOfPhoto;
        
        public GetPhoto()
        {
            InitializeComponent();
        }
       
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text == "Search for name of photo .....")
            {
                textBox1.Text = "";
                Font fnt = new Font("Century Gothic", 12.0F);
                textBox1.Font = fnt;
                textBox1.ForeColor = Color.SteelBlue;

            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                textBox1.ForeColor = Color.DarkGray;
                textBox1.Text = "Search for name of photo .....";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            nameOfPhoto = textBox1.Text;
            if (nameOfPhoto == "")
            {
                MessageBox.Show("Sorry , Please Enter Name Of Photo");

            }
            else
            {
                if (!photos.Contains(nameOfPhoto))
                {
                    MessageBox.Show("Image not found.");
                }
                else
                {
                    Form3 u2 = new Form3();
                    u2.Show();
                }
            }
            textBox1.ForeColor = Color.DarkGray;
            textBox1.Text = "Search for name of photo .....";
        }

        private void GetPhoto_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT imageName FROM Image_User WHERE Permission =  @prefix ", cn))
                {
                    cmd.Parameters.AddWithValue("@prefix",Login.userPermission);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        while (reader.Read())
                        {
                            photos.Add(reader.GetString(0));
                            collection.Add(reader.GetString(0));
                        }
                        textBox1.AutoCompleteCustomSource = collection;
                    }
                }
            }
            dataGridView1.Columns.Add("name", "Name");
            dataGridView1.Columns["name"].ValueType = typeof(string);
            dataGridView1.Columns.Add("description", "Description");
            dataGridView1.Columns["description"].ValueType = typeof(string);
            dataGridView1.Columns.Add("date", "Date");
            dataGridView1.Columns["date"].ValueType = typeof(DateTime);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*dataGridView1.DataSource = null;*/
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            string text = textBox1.Text;
            if (text != "")
            {
                for (int i = 0; i < photos.Count; i++)
                {
                    if (Is_Prefix(photos[i], text))
                    {
                        string query = "SELECT name,description,date FROM Image WHERE name = @photo_name";
                        try
                        {
                            using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                            {
                                cn.Open();
                                using (SqlCommand cmd = new SqlCommand(query, cn))
                                {
                                    cmd.Parameters.AddWithValue("@photo_name", photos[i]);
                                    using (SqlDataReader rdr = cmd.ExecuteReader())
                                    {
                                        while (rdr.Read())
                                        {
                                            var name = rdr.GetString(0);
                                            var description = rdr.GetString(1);
                                            var date = rdr.GetDateTime(2);
                                            dataGridView1.Rows.Add(name, description, date);
                                            //The 0 stands for "the 0'th column", so the first column of the result. 
                                        }
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

        private bool Is_Prefix (string s1 , string s2)
        {
            int size = Math.Min(s1.Length, s2.Length);
            for (int i=0; i<size; i++)
            {
                if (s1[i] != s2[i])
                {
                    return false;
                }
            }
            return true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selected = dataGridView1.Rows[index];
            textBox1.Text = selected.Cells[0].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selected = dataGridView1.Rows[index];
            textBox1.Text = selected.Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}





