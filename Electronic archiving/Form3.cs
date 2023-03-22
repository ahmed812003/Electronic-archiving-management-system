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

namespace Electronic_archiving
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT image FROM Image WHERE name = @name", cn))
                    {
                        cmd.Parameters.AddWithValue("@name", GetPhoto.nameOfPhoto);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] picData = reader["image"] as byte[] ?? null;
                                if (picData != null)
                                {
                                    using (MemoryStream ms = new MemoryStream(picData))
                                    {
                                        System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                                        pictureBox1.Image = image;
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Image not found.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Image not found.");
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

        private void closeBotton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please Choose Photo");
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp|GIF Image|*.gif";
                saveFileDialog.Title = "Save an Image File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    System.IO.FileStream fs =
                       (System.IO.FileStream)saveFileDialog.OpenFile();
                    switch (saveFileDialog.FilterIndex)
                    {
                        case 1:
                            this.pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case 2:
                            this.pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Png);
                            break;

                        case 3:
                            this.pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case 4:
                            this.pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                    }
                    fs.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please Choose Photo");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection(@"Server=DESKTOP-E3GO5E9\SQLEXPRESS ; DataBase=Archive ;Integrated Security= true;"))
                {
                    cn.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Image_User WHERE imageName = @PhotoName", cn))
                    {
                        command.Parameters.AddWithValue("@PhotoName", GetPhoto.nameOfPhoto);
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand("DELETE FROM Image WHERE name = @PhotoName", cn))
                    {
                        command.Parameters.AddWithValue("@PhotoName", GetPhoto.nameOfPhoto);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Photo Deleted successfully.");
                }
            }
        }

    }
}
