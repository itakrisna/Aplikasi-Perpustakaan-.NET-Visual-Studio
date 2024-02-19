Aplikasi Perpustakaan dengan LINQ, database dan otentikasi 

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/e53dc132-a0d8-402d-84db-1dc53e6f3863)
Use Case

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/4e0eeec8-25fe-4028-992b-824c5d462a8d)

Tombol  Login: Menuju form Peminjam
Tombol  Admin: Menuju form Petugas
 
 ![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/4f1146c6-47ae-4ff9-b5e6-0699c5f93ed0)

Tombol Melihat Buku: Mengambil data buku dari database dan menampilkannya dalam DataGridView. Menambahkan kolom "status" jika tidak ada dan menetapkan nilai awal menggunakan LINQ. 

Tombol Mendaftarkan Peminjam: Mendaftarkan peminjam dan memperbarui status buku yang dipinjam di database. Menyisipkan baris baru untuk peminjam dan buku yang dipinjam. 

Tombol Mengembalikan Buku: Mengembalikan buku yang dipinjam dan memperbarui status pada tabel "Buku" dan "peminjam". Menyegarkan DataGridView untuk mencerminkan data yang diperbarui. 

Tombol Cari Buku: Mencari buku berdasarkan judul yang diberikan menggunakan query SQL LIKE. 

Tombol Melihat Peminjam: Mengambil data pinjaman dari tabel "peminjam" dan menampilkannya dalam DataGridView. 

Tombol Keluar: Keluar dan kembali ke formulir login. 

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/c92f63e0-5710-497e-90b3-91896dc901cf)

Tombol Melihat Buku: Mengambil data buku dari database dan menampilkannya dalam DataGridView. Menambahkan kolom "status" jika tidak ada dan menetapkan nilai awal menggunakan LINQ. 

Tombol Meminjam Buku:  Meminjam Buku dan memperbarui status buku yang dipinjam di database. Menyisipkan baris baru untuk peminjam dan buku yang dipinjam. 

Tombol Mengembalikan Buku: Mengembalikan buku yang dipinjam dan memperbarui status pada tabel "Buku" dan "peminjam". Menyegarkan DataGridView untuk mencerminkan data yang diperbarui. 

Tombol Cari Buku: Mencari buku berdasarkan judul yang diberikan menggunakan query SQL LIKE. 

Tombol Keluar: Keluar dan kembali ke formulir login. 

Panduan penggunaan aplikasi
 
 ![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/3a83561f-ff14-4eb2-b666-0701d71840ad)

Untuk tombol login menggunakan nama dan password 1 sampai 30 
Untuk tombol admin menggunakan nama admin dan password 31 

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/61e3b8c3-3c9a-44cd-8d94-5ae2a4c1f25e)

Apabila nama dan password nya tidak ada akan ada perhatian untuk mengisi nama dan password

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/23709145-4390-4ad7-aad1-ba8008118da6)

Akan ada notifikasi anda tidak memiliki akses sebagai admin apabila mencoba login lewat admin 

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/0a8f92fc-1d15-4023-8dec-d0cad78639d4)

From petugas
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/0e4ebfe0-44b4-4c5e-b96c-72985322268f)

Akan muncul daftar buku apabila dipencet button melihat buku
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/8adc0149-7d8f-4496-a1fd-9425cef26e91)

Akan ada notifikasi pilih buku yang akan dipinjam apabila belum memilih buku tetapi memencet button mendaftarkan pinjaman
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/fab49abc-c404-46d8-851e-b295c895aa59)

Akan ada notifikasi mohon isi nama peminjam apabila belum memilih buku tetami memencet button mendaftarkan pinjaman
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/3f09885e-3e9c-4081-93e9-a8978f19accf)

Akan ada notifikasi buku berhasil dipinjam dan mengubah status tersedia jadi dipinjam apabila sudan memilih buku, mengisi nama peminjam dan memencet button mendaftarkan pinjaman
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/67a67255-1bb6-48c8-838b-37395f5677d0)

Apabila dipencet button melihat pinjaman maka muncul daftar peminjam
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/b8b0cb43-e9de-4da1-a12a-0b3a4a37e8ab)

Akan ada notifikasi  pilih buku yang akan di kembalikan apabila memencet button mengembalikan buku tetami belum memilih buku yang akan dikembalikan
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/378c42a3-49f5-429b-80fc-f020409ff897)

Akan ada notifikasi buku berhasil dikembalikan apabila sudah memilih buku yang akan dikembalikan
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/8861ffb2-da61-4a28-9fc0-18d23866bc53)

Apabila sudah memilih buku yang akan dikembalikan dan memencet button mengembalikan buku maka statusnya dari dipinjam berubah menjadi selesai dan muncul notifikasi buku berhasil dikembalikan
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/4d06bc3c-7f67-4500-a6f8-27d027d4d3f0)

Akan muncul notifikasi buku tidak dapat dikembalikan karena belum dipinjam apabila buku sudah dikembalikan dan statusnya selesai
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/b55bc822-a40d-4324-bf87-dd426c4e81c8)

Akan muncul notifikasi tidak ada buku yang ditemukan dengan judul yang diberikan apabila judul buku yang ditulis di cari buku tidak ada di database
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/5c0c5a1a-2d80-465c-9912-73e0df963465)

Apabila judul buku yang ditulis di cari buku ada didatabase maka akan dimunculkan ketika dipencet button cari
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/68612b10-db34-48d1-b9c8-11a36e3331ac)

Apabila dipencet button keluar maka akan kembali ke form login
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/370ff019-c038-4158-a51e-14aad97165bd)

Form Peminjam
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/57106af9-a71f-47d0-9b2b-f937166d6ca8)

Akan muncul daftar buku apabila dipencet button melihat buku

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/8b47873c-b78f-48e1-9d93-ea392094c8c1)

Akan ada notifikasi pilih buku yang akan dipinjam apabila belum memilih buku tetapi memencet button meminjam buku

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/f655b8be-55fb-47ba-9e90-a1f8aa85842d)

Akan ada notifikasi buku berhasil dipinjam dan mengubah status tersedia jadi dipinjam apabila sudan memilih buku dan memencet button meminjam buku
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/01e6ee9b-acc2-430b-9d83-987eca6721fb)

Akan ada notifikasi pilih buku yang akan dikembalikan apabila belum memilih buku tetapi memencet button mengembalikan buku
 
![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/3149c246-55e3-43a8-b701-febabedee167)

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/7cb07d43-7b56-4801-a95f-260948b4c233)

Akan ada notifikasi buku berhasil dikembalikan apabila sudah memilih buku yang akan dikembalikan dan memencet tombol mengembalikan buku

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/0d195234-e78e-4d32-9fc6-6f3db9bcae9c)

Akan ada notifikasi  masukan judul buku untuk pencarian apabila belum menuliskan juduk buku tetapi memencet button cari

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/c7d7630e-7bff-4e38-a047-356ed0ec7a4d)

Apabila judul buku yang ditulis di cari buku ada didatabase maka akan dimunculkan ketika dipencet button cari

![image](https://github.com/itakrisna/Aplikasi-Perpustakaan-.NET-Visual-Studio/assets/152336076/492e9de9-c7df-4cd0-81be-535d931a2bf6)

Apabila dipencet button keluar maka akan kembali ke form login
