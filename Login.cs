using Perpustakaan;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Uts.NET
{
    public partial class Form_login : Form
    {
        private readonly string connectionString = "Data Source=DESKTOP-G96JCI6;Initial Catalog=DB_Perpustakaan;Integrated Security=True";
        private bool isAdminLogin = false;

        public Form_login()
        {
            InitializeComponent();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            // Set the login type to user
            isAdminLogin = false;

            // Perform login handling
            HandleLogin();
        }

        private void button_admin_Click(object sender, EventArgs e)
        {
            // Set the login type to admin
            isAdminLogin = true;

            // Perform login handling
            HandleLogin();
        }

        private void HandleLogin()
        {
            // Membuat koneksi ke database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Membuat adapter untuk eksekusi query
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM Login WHERE Nama = @Nama AND Password = @Password", connection))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@Nama", textBox_nama.Text);
                    sda.SelectCommand.Parameters.AddWithValue("@Password", textBox_password.Text);

                    // Membuat DataTable untuk menampung hasil query
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // Handle the login result
                    if (dt.Rows.Count > 0)
                    {
                        int count = Convert.ToInt32(dt.Rows[0][0]);

                        if (!isAdminLogin && count >= 1 && count <= 30)
                        {
                            // If logged in as a regular user, show Form_peminjam
                            this.Hide();
                            Form_peminjam panggil = new Form_peminjam(textBox_nama.Text);
                            panggil.Show();
                        }
                        else if (isAdminLogin && textBox_nama.Text.ToLower() == "admin" && textBox_password.Text == "31")
                        {
                            // If logged in as admin with the specific name and password, show Petugas
                            this.Hide();
                            Petugas panggil = new Petugas();
                            panggil.Show();
                        }
                        else
                        {
                            MessageBox.Show("Nama atau password salah atau Anda tidak memiliki akses", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mohon isi nama dan password anda", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
