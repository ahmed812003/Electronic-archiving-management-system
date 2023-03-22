using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Electronic_archiving
{
    public partial class Save : Form
    {
        List<string> Permissions = new List<string>();
        Image image;

        public Save()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    image = Image.FromFile(ofd.FileName);
                    pictureBox1.Image = image;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string description = textBox1.Text;
            DateTime date = dateTimePicker1.Value;
            if (name == "")
            {
                MessageBox.Show("Sorry , Please Enter Name Of Photo");
            }
            else if (image == null)
            {
                MessageBox.Show("Sorry , Please Choose Photo From Your Pc Using Load");
            }
            else
            {
                int rows = -1;
                try
                {
                    using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                    {
                        cn.Open();
                        using (SqlCommand cmd = new SqlCommand("insert into Image (name , image , date , description) values (@name, @image , @date , @description)", cn))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(image, typeof(byte[]));
                            cmd.Parameters.AddWithValue("@image", bytes);
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.Parameters.AddWithValue("@description", description);

                            rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                                MessageBox.Show("Photo inserted successfully.");
                            else
                                MessageBox.Show("No Photo inserted.");
                            pictureBox1.Image = null;
                            textBox1.Text = "";
                            textBox2.Text = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                int id = -1;
                if(rows != -1)
                {

                    try
                    {
                        using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                        {
                            cn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT id FROM Image WHERE name = @name", cn))
                            {
                                cmd.Parameters.AddWithValue("@name", name);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        id = reader.GetInt32(0);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    for (int i = 0; i < listView1.CheckedIndices.Count; i++)
                    {
                        int idx = listView1.CheckedIndices[i];
                        string data = listView1.Items[idx].SubItems[0].Text;
                        try
                        {
                            using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                            {
                                cn.Open();
                                using (SqlCommand cmd = new SqlCommand("insert into Image_User(Permission , imageName , id) VALUES (@Permission, @image_name , @id)", cn))
                                {
                                    cmd.Parameters.AddWithValue("@Permission", data);
                                    cmd.Parameters.AddWithValue("@image_name", name);
                                    cmd.Parameters.AddWithValue("@id", id);
                                    rows = cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    try
                    {
                        using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                        {
                            cn.Open();
                            using (SqlCommand cmd = new SqlCommand("insert into Image_User(Permission , imageName , id) VALUES (@Permission, @image_name , @id)", cn))
                            {
                                cmd.Parameters.AddWithValue("@Permission", Login.userPermission);
                                cmd.Parameters.AddWithValue("@image_name", name);
                                cmd.Parameters.AddWithValue("@id", id);
                                rows = cmd.ExecuteNonQuery();
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

        private void Save_Load(object sender, EventArgs e)
        {
            Permissions.Add("permission 1");
            Permissions.Add("permission 2");
            Permissions.Add("permission 3");
            Permissions.Add("permission 4");
            listView1.CheckBoxes = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Users",-2, HorizontalAlignment.Left);
            foreach (var permission in Permissions)
            {
                if(permission != Login.userPermission)
                {
                    ListViewItem item = new ListViewItem(permission);
                    listView1.Items.Add(item);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
