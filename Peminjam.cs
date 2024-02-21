using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using Uts.NET;

namespace Perpustakaan
{
    public partial class Form_peminjam : Form
    {
        private readonly string connectionString = "Data Source=DESKTOP-G96JCI6;Initial Catalog=DB_Perpustakaan;Integrated Security=True";
        private string namaPeminjam;

        public Form_peminjam(string namaPeminjam)
        {
            InitializeComponent();
            this.namaPeminjam = namaPeminjam;
        }

        // Menampilkan data buku pada DataGridView
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
                    MessageBox.Show("Terjadi kesalahan saat menampilkan data buku: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Tombol untuk meminjam buku
        private void button_meminjam_buku_Click(object sender, EventArgs e)
        {
            MeminjamBuku();
        }

        // Metode untuk meminjam buku
        private void MeminjamBuku()
        {
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
                        insertCommand.Parameters.AddWithValue("@namaPeminjam", namaPeminjam);
                        insertCommand.ExecuteNonQuery();

                        // Refresh DataGridView
                        TampilkanDataBuku();

                        MessageBox.Show("Buku berhasil dipinjam.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan saat meminjam buku: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih buku yang akan dipinjam.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                            MessageBox.Show("Buku berhasil dikembalikan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Buku tidak dapat dikembalikan karena belum dipinjam.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan saat mengembalikan buku: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih buku yang akan dikembalikan.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Tombol untuk keluar dari aplikasi
        private void button_keluar_Click(object sender, EventArgs e)
        {
            Form_login loginForm = new Form_login();

            // Menampilkan form login dan menyembunyikan form saat ini
            loginForm.Show();
            this.Hide();
        }

        private void button_cari_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_cari.Text))
            {
                MessageBox.Show("Masukkan judul buku untuk melakukan pencarian.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
                    string query = $"SELECT no, judul, tahun_terbit, status FROM Buku WHERE judul LIKE '%{judulBukuCari}%'";
                    SqlCommand command = new SqlCommand(query, connection);

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
                MessageBox.Show("Terjadi kesalahan saat melakukan pencarian buku: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
