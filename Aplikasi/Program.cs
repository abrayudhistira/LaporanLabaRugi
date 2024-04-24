using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Net.Security;
using System.Collections;
using System.IO;
using System.Xml.Linq;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace LaporanLabaRugi
{
    internal class Program
    {
        private const string Username = "admin";
        private const string Password = "admin";

        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                Console.WriteLine("Masukkan Username :");
                string usernameInput = Console.ReadLine();
                Console.WriteLine("Masukkan Password : ");
                string passwordInput = Console.ReadLine();

                if (Authenticate(usernameInput, passwordInput))
                {
                    Console.WriteLine("Login Berhasil!\n");
                }
                else
                {
                    Console.WriteLine("Username atau Password Salah. Silahkan Coba Lagi.\n");
                }
                try
                {
                    Console.WriteLine("Masukkan database tujuan : ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database : ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data Source = MSI\\ABRA; " + "initial Catalog = {0};User ID = SA; Password = 30d31bca";
                                conn = new SqlConnection(string.Format(strKoneksi, db));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu Utama");
                                        Console.WriteLine("1. Kelola Laporan Rugi Laba");
                                        Console.WriteLine("2. Cetak Laporan Rugi Laba");
                                        Console.WriteLine("3. Cetak SPT Tahunan");
                                        Console.WriteLine("4. Lihat Grafik Pengeluaran");
                                        Console.WriteLine("5. Keluar");
                                        Console.Write("\nEnter your choice (1-5): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Kelola Laporan Rugi Laba");
                                                    Console.WriteLine();
                                                    pr.KelolaLaporanRugiLaba(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Cetak Laporan Rugi Laba");
                                                    Console.WriteLine();
                                                    pr.CetakLaporanRugiLaba(conn);
                                                }
                                                break;
                                            case '3':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Cetak SPT Tahunan");
                                                    Console.WriteLine();
                                                    pr.CetakSPTTahunan(conn);
                                                }
                                                break;
                                            case '4':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Lihat Grafik Pengeluaran");
                                                    pr.ViewGrafikPengeluaran(conn);
                                                }
                                                break;
                                            case '5':
                                                conn.Close();
                                                Console.Clear();
                                                Main(new String[0]);
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Tersebut\n");
                    Console.ResetColor();
                }
            }
        }
        static bool Authenticate(string username, string password)
        {
            return username == Username && password == Password;
        }
        public void KelolaLaporanRugiLaba(SqlConnection con)
        {
            Program prg1 = new Program();
            Console.WriteLine("Menu Kelola Laporan Rugi Laba");
            Console.WriteLine("1. Tambah Data");
            Console.WriteLine("2. Hapus Data");
            Console.WriteLine("3. Update Data");
            Console.WriteLine("4. Lihat Laporan Rugi Laba");
            Console.WriteLine("5. Cari Data (per Periode)");
            Console.WriteLine("6. Keluar");
            Console.Write("\nEnter your choice (1-5): ");
            char chr = Convert.ToChar(Console.ReadLine());
            switch (chr)
            {
                case '1':
                    Console.Clear();
                    Console.WriteLine("Input Data \n");
                    Console.WriteLine("Masukkan Periode : ");
                    decimal Periode = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan NPWP : ");
                    decimal NPWP = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Pajak : ");
                    decimal Pajak = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Pendapatan : ");
                    decimal Pendapatan = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("HPP \n");
                    Console.WriteLine("Masukkan Persediaan Awal : ");
                    decimal Persediaan_Awal = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Pembelian : ");
                    decimal Pembelian = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Persediaan Akhir : ");
                    decimal Persediaan_Akhir = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Gaji Karyawan : ");
                    decimal Gaji_Karyawan = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Laba Usaha \n");
                    Console.WriteLine("Masukkan Laba Bruto Usaha: ");
                    decimal LabaBrutoUsaha = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Laba Usaha Sebelum Pajak : ");
                    decimal LabaUsahaSebelumPajak = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Laba Usaha Sesudah Pajak : ");
                    decimal LabaUsahaSesudahPajak = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Biaya Operasional \n");
                    Console.WriteLine("Masukkan Biaya Sewa : ");
                    decimal Biaya_Sewa = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Pembelian ATK : ");
                    decimal Pembelian_ATK = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Transportasi : ");
                    decimal Transportasi = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Listrik : ");
                    decimal Listrik = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Biaya Penyusutan : ");
                    decimal Biaya_Penyusutan = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Biaya Lain : ");
                    decimal Biaya_Lain = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Biaya Telepon : ");
                    decimal Biaya_Telepon = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Biaya Perawatan : ");
                    decimal Biaya_Perawatan = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Masukkan Bunga Bank : ");
                    decimal Bunga_Bank = Convert.ToDecimal(Console.ReadLine());
                    try
                    {
                        prg1.TambahData(Periode,NPWP,Pajak,Pendapatan,Persediaan_Awal,Pembelian,Persediaan_Akhir,Gaji_Karyawan,LabaBrutoUsaha,LabaUsahaSebelumPajak,LabaUsahaSesudahPajak,Biaya_Sewa,Pembelian_ATK,Transportasi,Listrik,Biaya_Penyusutan,
                                        Biaya_Lain,Biaya_Telepon,Biaya_Perawatan,Bunga_Bank,con);
                    } 
                    catch
                    {
                        Console.WriteLine("\nAnda tidak mempunyai akses untuk menambah data");
                    }
                    break;
                case '2':
                    Console.Clear();
                    Console.WriteLine("Masukkan Periode yg ingin dihapus : ");
                    decimal Period = Convert.ToDecimal(Console.ReadLine());
                    try
                    {
                        prg1.HapusData(Period, con);
                    }
                    catch
                    {
                        Console.WriteLine("\nAnda tidak mempunyai akses untuk menghapus data");
                    }
                    break;
                case '3':
                    try
                    {
                        prg1.UpdateData(con);
                    }
                    catch
                    {
                        Console.WriteLine("\nAnda tidak mempunyai akses untuk mengupdate data");
                    }
                    break;
                case '4':
                    prg1.LihatLaporan(con);
                    break;
                case '5':
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Pencarian Laporan Laba Rugi\n");
                        Console.WriteLine("Masukkan Tahun Periode yang akan dicari: \n");
                        string searchQuery = Console.ReadLine();
                        prg1.search(searchQuery, con);
                    }
                    catch
                    {
                        Console.WriteLine("\nAnda tidak mempunyai akses untuk mencari data");
                    }
                    break;
                case '6':
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
        public void TambahData(decimal Periode,decimal NPWP,decimal Pajak,decimal Pendapatan, decimal Persediaan_Awal,decimal Pembelian, 
            decimal Persediaan_Akhir,decimal Gaji_Karyawan,decimal LabaBrutoUsaha,decimal LabaUsahaSebelumPajak,decimal LabaUSahaSesudahPajak,decimal Biaya_Sewa,decimal Pembelian_ATK,decimal Transportasi,decimal Listrik,decimal Biaya_Penyusutan,
            decimal Biaya_Lain,decimal Biaya_Telepon,decimal Biaya_Perawatan,decimal Bunga_Bank, SqlConnection con)
        {
            string str = "";

            str = "INSERT INTO Periode (Periode) VALUES (@prd); " +
                     "INSERT INTO NPWP (NPWP) VALUES (@npwp); " +
                     "INSERT INTO Pajak (Pajak) VALUES (@pajak); " +
                     "INSERT INTO Pendapatan (Pendapatan) VALUES (@pendapatan); " +
                     "INSERT INTO HPP (Persediaan_Awal, Pembelian, Persediaan_Akhir) VALUES (@persediaanAwal, @pembelian, @persediaanAkhir); " +
                     "INSERT INTO Gaji (Gaji_Karyawan) VALUES (@gajiKaryawan); " +
                     "INSERT INTO LabaUsaha (LabaBrutoUsaha, LabaUsahaSebelumPajak, LabaUsahaSesudahPajak) VALUES (@labaBrutoUsaha, @labaUsahaSebelumPajak, @labaUsahaSesudahPajak); " +
                     "INSERT INTO BiayaOperasional (Biaya_Sewa, Pembelian_ATK, Transportasi, Listrik, Biaya_Penyusutan, Biaya_Lain, Biaya_Telepon, Biaya_Perawatan, Bunga_Bank) VALUES (@biayaSewa, @pembelianATK, @transportasi, @listrik, @biayaPenyusutan, @biayaLain, @biayaTelepon, @biayaPerawatan, @bungaBank);";


            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("@prd", Periode));
            cmd.Parameters.Add(new SqlParameter("@npwp", NPWP));
            cmd.Parameters.Add(new SqlParameter("@pajak", Pajak));
            cmd.Parameters.Add(new SqlParameter("@pendapatan", Pendapatan));
            cmd.Parameters.Add(new SqlParameter("@persediaanAwal", Persediaan_Awal));
            cmd.Parameters.Add(new SqlParameter("@pembelian", Pembelian));
            cmd.Parameters.Add(new SqlParameter("@persediaanAkhir", Persediaan_Akhir));
            cmd.Parameters.Add(new SqlParameter("@gajiKaryawan", Gaji_Karyawan));
            cmd.Parameters.Add(new SqlParameter("@labaBrutoUsaha", LabaBrutoUsaha));
            cmd.Parameters.Add(new SqlParameter("@labaUsahaSebelumPajak", LabaUsahaSebelumPajak));
            cmd.Parameters.Add(new SqlParameter("@labaUsahaSesudahPajak", LabaUSahaSesudahPajak));
            cmd.Parameters.Add(new SqlParameter("@biayaSewa", Biaya_Sewa));
            cmd.Parameters.Add(new SqlParameter("@pembelianATK", Pembelian_ATK));
            cmd.Parameters.Add(new SqlParameter("@transportasi", Transportasi));
            cmd.Parameters.Add(new SqlParameter("@listrik", Listrik));
            cmd.Parameters.Add(new SqlParameter("@biayaPenyusutan", Biaya_Penyusutan));
            cmd.Parameters.Add(new SqlParameter("@biayaLain", Biaya_Lain));
            cmd.Parameters.Add(new SqlParameter("@biayaTelepon", Biaya_Telepon));
            cmd.Parameters.Add(new SqlParameter("@biayaPerawatan", Biaya_Perawatan));
            cmd.Parameters.Add(new SqlParameter("@bungaBank", Bunga_Bank));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");

        }
        public void HapusData(decimal Period, SqlConnection con)
        {
            string str = "DELETE FROM Periode WHERE Periode=@prd;";

            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("prd", Period));
            
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Dihapus");
        }
        public void UpdateData(SqlConnection con)
        {

            Console.Clear();
            Console.WriteLine("Update Data \n");
            Console.WriteLine("Masukkan Tahun Periode yang akan Dirubah : ");
            decimal Prde = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Tahun Periode Baru : ");
            decimal Periode = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan NPWP : ");
            decimal NPWP = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Pajak : ");
            decimal Pajak = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Pendapatan : ");
            decimal Pendapatan = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("HPP \n");
            Console.WriteLine("Masukkan Persediaan Awal : ");
            decimal Persediaan_Awal = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Pembelian : ");
            decimal Pembelian = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Persediaan Akhir : ");
            decimal Persediaan_Akhir = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Gaji Karyawan : ");
            decimal Gaji_Karyawan = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Laba Usaha \n");
            Console.WriteLine("Masukkan Laba Bruto Usaha: ");
            decimal LabaBrutoUsaha = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Laba Usaha Sebelum Pajak : ");
            decimal LabaUsahaSebelumPajak = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Laba Usaha Sesudah Pajak : ");
            decimal LabaUsahaSesudahPajak = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Biaya Operasional \n");
            Console.WriteLine("Masukkan Biaya Sewa : ");
            decimal Biaya_Sewa = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Pembelian ATK : ");
            decimal Pembelian_ATK = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Transportasi : ");
            decimal Transportasi = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Listrik : ");
            decimal Listrik = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Biaya Penyusutan : ");
            decimal Biaya_Penyusutan = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Biaya Lain : ");
            decimal Biaya_Lain = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Biaya Telepon : ");
            decimal Biaya_Telepon = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Biaya Perawatan : ");
            decimal Biaya_Perawatan = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Masukkan Bunga Bank : ");
            decimal Bunga_Bank = Convert.ToDecimal(Console.ReadLine());

            string str = "";
            str = "update Periode set Periode = @prd, NPWP = @npwp, Pajak = @pajak, Pendapatan = @pendapatan, " +
                "Persediaan_Awal = @persediaanAwal, Pembelian = @pembelian, Persediaan_Akhir = @persediaanAkhir, " +
                "Gaji_Karyawan = @gajiKaryawan, LabaBrutoUsaha = @labaBrutoUsaha, LabaUsahaSebelumPajak = @labaUsahaSebelumPajak, " +
                "LabaUsahaSesudahPajak = @labaUsahaSesudahPajak, Biaya_Sewa = @biayaSewa, Pembelian_ATK = @pembelianATK, " +
                "Transportasi = @transportasi, Listrik = @listrik, Biaya_Penyusutan = @biayaPenyusutan, Biaya_Lain = @biayaLain, " +
                "Biaya_Telepon = @biayaTelepon, Biaya_Perawatan = @biayaPerawatan, Bunga_Bank = @bungaBank WHERE ID_Periode = @idprd ";

            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("idprd", Prde));
            cmd.Parameters.Add(new SqlParameter("prd", Periode));
            cmd.Parameters.Add(new SqlParameter("@npwp", NPWP));
            cmd.Parameters.Add(new SqlParameter("@pajak", Pajak));
            cmd.Parameters.Add(new SqlParameter("@pendapatan", Pendapatan));
            cmd.Parameters.Add(new SqlParameter("@persediaanAwal", Persediaan_Awal));
            cmd.Parameters.Add(new SqlParameter("@pembelian", Pembelian));
            cmd.Parameters.Add(new SqlParameter("@persediaanAkhir", Persediaan_Akhir));
            cmd.Parameters.Add(new SqlParameter("@gajiKaryawan", Gaji_Karyawan));
            cmd.Parameters.Add(new SqlParameter("@labaBrutoUsaha", LabaBrutoUsaha));
            cmd.Parameters.Add(new SqlParameter("@labaUsahaSebelumPajak", LabaUsahaSebelumPajak));
            cmd.Parameters.Add(new SqlParameter("@labaUsahaSesudahPajak", LabaUsahaSesudahPajak));
            cmd.Parameters.Add(new SqlParameter("@biayaSewa", Biaya_Sewa));
            cmd.Parameters.Add(new SqlParameter("@pembelianATK", Pembelian_ATK));
            cmd.Parameters.Add(new SqlParameter("@transportasi", Transportasi));
            cmd.Parameters.Add(new SqlParameter("@listrik", Listrik));
            cmd.Parameters.Add(new SqlParameter("@biayaPenyusutan", Biaya_Penyusutan));
            cmd.Parameters.Add(new SqlParameter("@biayaLain", Biaya_Lain));
            cmd.Parameters.Add(new SqlParameter("@biayaTelepon", Biaya_Telepon));
            cmd.Parameters.Add(new SqlParameter("@biayaPerawatan", Biaya_Perawatan));
            cmd.Parameters.Add(new SqlParameter("@bungaBank", Bunga_Bank));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil DiUpdate");
        }
        public void LihatLaporan(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("select * from Gaji;" +
                "select * from BiayaOperasional;" +
                "select * from HPP;" +
                "select * from LabaUsaha;" +
                "select * from NPWP;" +
                "select * from Pajak;" +
                "select * from Pendapatan;" +
                "select * from Periode;", con);
            SqlDataReader r = cmd.ExecuteReader();
            DisplayTable(r);
            /*while (r.Read())
            {
               for(int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }*/
            r.Close();
        }
        public void search(string query, SqlConnection con)
        {
            string str = "select * from BiayaOperasional;\" +\r\n                " +
                "\"select * from Gaji;\" +\r\n                " +
                "\"select * from HPP;\" +\r\n                " +
                "\"select * from LabaUsaha;\" +\r\n               " +
                " \"select * from NPWP;\" +\r\n               " +
                " \"select * from Pajak;\" +\r\n                " +
                "\"select * from Pendapatan;\" +\r\n                " +
                "\"select * from Periode; WHERE Periode LIKE @searchQuery";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@searchQuery", "%" + query + "%");

            SqlDataReader r = cmd.ExecuteReader();
            DisplayTable(r);
            r.Close();
        }
        public void CetakLaporanRugiLaba( SqlConnection con)
        {
            /*string pdfPath = "Laporan_Rugi_Laba.pdf";

            using (PdfWriter writer = new PdfWriter(pdfPath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);
                    Paragraph header = new Paragraph("Laporan Rugi Laba").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    document.Add(header);

                    Table table = new Table(8).UseAllAvailableWidth();
                    table.AddHeaderCell("ID_Laporan");
                    table.AddHeaderCell("Tanggal");
                    table.AddHeaderCell("Pendapatan");
                    table.AddHeaderCell("HPP");
                    table.AddHeaderCell("Gaji Karyawan");
                    table.AddHeaderCell("Biaya Operasional");
                    table.AddHeaderCell("Pajak");
                    table.AddHeaderCell("Laba Bersih");

                    SqlCommand cmd = new SqlCommand("SELECT * FROM LaporanRugiLaba", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        table.AddCell(reader["ID_Laporan"].ToString());
                        table.AddCell(reader["Tanggal"].ToString());
                        table.AddCell(reader["Pendapatan"].ToString());
                        table.AddCell(reader["HPP"].ToString());
                        table.AddCell(reader["Gaji_Karyawan"].ToString());
                        table.AddCell(reader["Biaya_Operasional"].ToString());
                        table.AddCell(reader["Pajak"].ToString());
                        table.AddCell(reader["Laba_Bersih"].ToString());
                    }

                    reader.Close();

                    document.Add(table);
                }
            }

            Console.WriteLine($"PDF berhasil dibuat di lokasi: {pdfPath}");*/
        }
        public void CetakSPTTahunan(SqlConnection con)
        {

        }
        public void ViewGrafikPengeluaran(SqlConnection con)
        {

        }
        public void DisplayTable(DbDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{reader.GetName(i),-15}");
            }
            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i],-15}");
                }
                Console.WriteLine();
            }
        }
    }
}
