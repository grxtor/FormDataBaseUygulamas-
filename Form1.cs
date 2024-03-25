using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace WindowsFormsApp15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            VerileriYenile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {

                string connectionString = @"Data Source=DESKTOP-6DMTNPM;Initial Catalog=a;Integrated Security=True";
                SqlConnection baglanti = new SqlConnection(connectionString);

                try
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO [dbo].[Table] ([Soyad], [ad]) VALUES (@Soyad,@ad)", baglanti);

                    komut.Parameters.AddWithValue("@Soyad", textBox2.Text);
                    komut.Parameters.AddWithValue("@ad", textBox1.Text);

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt yapıldı");
                    VerileriYenile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("TextBox'lar boş olamaz");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-6DMTNPM;Initial Catalog=a;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sqlQuery = "SELECT * FROM [dbo].[Table]";

                try
                {
                    connection.Open();

                    SqlDataAdapter da = new SqlDataAdapter(sqlQuery, connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridView1.SelectedRows[0];
            if (dataGridView1.SelectedRows.Count > 0 || dataGridView1.SelectedRows[0].Cells == null || selectedRow.Cells["Id"].Value == DBNull.Value)
            {
                
                int selectedRowId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                string connectionString = @"Data Source=DESKTOP-6DMTNPM;Initial Catalog=a;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string sqlQuery = "DELETE FROM [dbo].[Table] WHERE Id = @Id";

                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        // Parametreyi güvenli bir şekilde ekleyin
                        command.Parameters.AddWithValue("@Id", selectedRowId);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Kayıt silindi.");

                        VerileriYenile();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata oluştu: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir kayıt seçin.");
            }
        }

        private void VerileriYenile()
        {
            string connectionString = @"Data Source=DESKTOP-6DMTNPM;Initial Catalog=a;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // SqlDataAdapter ve DataTable kullanarak veri çekme
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [dbo].[Table]", connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // DataGridView kontrolünde verileri gösterme
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }


    }
}
