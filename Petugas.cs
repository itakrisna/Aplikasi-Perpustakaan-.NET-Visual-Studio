using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Uts.NET;

namespace Perpustakaan
{
    public partial class Petugas : Form
    {
        private readonly string connectionString = "Data Source=DESKTOP-G96JCI6;Initial Catalog=DB_Perpustakaan;Integrated Security=True";

        public Petugas()
        {
            InitializeComponent();
        }

        // Tombol untuk melihat data buku
        private void button_melihat_buku_Click(object sender, EventArgs e)
        {
            TampilkanDataBuku();
        }

        // Metode untuk menampilkan data buku pada DataGridView
        private void TampilkanDataBuku()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Query untuk mengambil data buku
                    string query = "SELECT * FROM Buku";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Memeriksa apakah kolom 'status' sudah ada
                    if (!dt.Columns.Contains("status"))
                    {
                        // Jika tidak, tambahkan kolom 'status'
                        dt.Columns.Add("status", typeof(string));

                        // Setel nilai awal status menggunakan LINQ
                        dt.AsEnumerable().ToList().ForEach(row => row.SetField("status", "Tersedia"));
                    }

                    // Menetapkan DataGridView dengan sumber data
                    dataGridView_tampilkan_data_buku.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Tombol untuk mendaftarkan peminjam
        private void button_mendaftarkan_peminjam_Click(object sender, EventArgs e)
        {
            MendaftarkanPinjaman();
        }

        // Metode untuk mendaftarkan peminjam dan meminjam buku
        private void MendaftarkanPinjaman()
        {
            string namaPeminjam = textBox_nama_peminjam.Text;

            // Memastikan ada baris yang dipilih di DataGridView
            if (dataGridView_tampilkan_data_buku.SelectedRows.Count > 0)
            {
                int nomorBuku = Convert.ToInt32(dataGridView_tampilkan_data_buku.SelectedRows[0].Cells["no"].Value);
                string judulBuku = dataGridView_tampilkan_data_buku.SelectedRows[0].Cells["judul"].Value.ToString();
                int tahunTerbit = Convert.ToInt32(dataGridView_tampilkan_data_buku.SelectedRows[0].Cells["tahun_terbit"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Update status di tabel 'Buku'
                        string updateQuery = "UPDATE Buku SET status = 'dipinjam' WHERE no = @nomorBuku";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@nomorBuku", nomorBuku);
                        updateCommand.ExecuteNonQuery();

                        // Masukkan data ke dalam tabel 'peminjam'
                        string insertQuery = "INSERT INTO peminjam (no, judul, tahun_terbit, nama_peminjam, status) VALUES (@nomorBuku, @judulBuku, @tahunTerbit, @namaPeminjam, 'Dipinjam')";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@nomorBuku", nomorBuku);
                        insertCommand.Parameters.AddWithValue("@judulBuku", judulBuku);
                        insertCommand.Parameters.AddWithValue("@tahunTerbit", tahunTerbit);
                        insertCommand.Parameters.AddWithValue("@namaPeminjam", namaPeminjam); // Gunakan nilai dari textBox_nama_peminjam
                        insertCommand.ExecuteNonQuery();

                        // Refresh DataGridView
                        TampilkanDataBuku();

                        MessageBox.Show("Buku berhasil dipinjam.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih buku yang akan dipinjam.");
            }
        }

        // Tombol untuk mengembalikan buku
        private void button_mengembalikan_buku_Click(object sender, EventArgs e)
        {
            MengembalikanBuku();
        }

        // Metode untuk mengembalikan buku
        private void MengembalikanBuku()
        {
            // Memastikan ada baris yang dipilih di DataGridView
            if (dataGridView_tampilkan_data_buku.SelectedRows.Count > 0)
            {
                int nomorBuku = Convert.ToInt32(dataGridView_tampilkan_data_buku.SelectedRows[0].Cells["no"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Update status di tabel 'Buku' menjadi 'Tersedia'
                        string updateBukuQuery = "UPDATE Buku SET status = 'tersedia' WHERE no = @nomorBuku";
                        SqlCommand updateBukuCommand = new SqlCommand(updateBukuQuery, connection);
                        updateBukuCommand.Parameters.AddWithValue("@nomorBuku", nomorBuku);
                        updateBukuCommand.ExecuteNonQuery();

                        // Update status di tabel 'peminjam' menjadi 'Selesai'
                        string updatePeminjamQuery = "UPDATE peminjam SET status = 'Selesai' WHERE no = @nomorBuku AND status = 'dipinjam'";
                        SqlCommand updatePeminjamCommand = new SqlCommand(updatePeminjamQuery, connection);
                        updatePeminjamCommand.Parameters.AddWithValue("@nomorBuku", nomorBuku);
                        int rowsAffected = updatePeminjamCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Refresh DataGridView
                            TampilkanDataBuku();

                            MessageBox.Show("Buku berhasil dikembalikan.");
                        }
                        else
                        {
                            MessageBox.Show("Buku tidak dapat dikembalikan karena belum dipinjam.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih buku yang akan dikembalikan.");
            }
        }

        // Tombol untuk melakukan pencarian buku
        private void button_cari_Click(object sender, EventArgs e)
        {
            CariBuku();
        }

        // Metode untuk melakukan pencarian buku berdasarkan judul
        private void CariBuku()
        {
            try
            {
                string judulBukuCari = textBox_cari.Text.Trim();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SqlCommand untuk mencari buku berdasarkan judul
                    string query = "SELECT no, judul, tahun_terbit, status FROM Buku WHERE judul LIKE @judulBuku";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@judulBuku", "%" + judulBukuCari + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Simpan teks saat ini di textBox_cari
                    string currentText = textBox_cari.Text;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Tidak ada buku yang ditemukan dengan judul yang diberikan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Setel DataSource
                        dataGridView_tampilkan_data_buku.DataSource = dt;
                    }

                    // Kembalikan teks di textBox_cari
                    textBox_cari.Text = currentText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Tombol untuk melihat data peminjaman
        private void button_melihat_pinjaman_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SqlCommand untuk mengambil data peminjam
                    string query = "SELECT * FROM dbo.peminjam";
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Setel DataSource DataGridView dengan DataTable
                    dataGridView_tampilkan_data_buku.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Tombol untuk keluar dari aplikasi
        private void button_keluar_Click(object sender, EventArgs e)
        {
            Form_login loginForm = new Form_login();

            // Tampilkan form login dan sembunyikan form saat ini
            loginForm.Show();
            this.Hide();
        }
    }
}
