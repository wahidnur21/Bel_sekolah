using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using Component;
using Database;
using System.Threading;

namespace bel_skolah
{
    class Program
    {
        public static AccessDatabaseHelper DB = new AccessDatabaseHelper("./urip.accdb");
   
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Kotak Kepala = new Kotak();
            Kepala.SetXY(0, 0);
            Kepala.SetWidthAndHeight(119, 4);
            Kepala.SetBackColor (ConsoleColor.Black);
            Kepala.SetForeColor(ConsoleColor.Green);
            Kepala.Tampil();

            Kotak Kaki = new Kotak();
            Kaki.SetXY(0, 25);
            Kaki.SetWidthAndHeight(119, 4);
            Kaki.SetBackColor(ConsoleColor.Black);
            Kaki.SetForeColor(ConsoleColor.Green);
            Kaki.Tampil();

            Kotak Kiri = new Kotak();
            Kiri.SetXY(0, 5);
            Kiri.SetWidthAndHeight(30, 19);
            Kiri.SetBackColor(ConsoleColor.Black);
            Kiri.SetForeColor(ConsoleColor.Green);
            Kiri.Tampil();

            Kotak Kanan = new Kotak();
            Kanan.SetXY(31, 5);
            Kanan.SetWidthAndHeight(88, 19);
            Kanan.SetBackColor(ConsoleColor.Black);
            Kanan.SetForeColor(ConsoleColor.Green);
            Kanan.Tampil();


            Tulisan NamaAplikasi = new Tulisan();
            NamaAplikasi.Text = "~: APLIKASI BEL SEKOLAH :~";
            NamaAplikasi.SetXY(11,1);
            NamaAplikasi.Length = 100;
            NamaAplikasi.SetForeColor(ConsoleColor.Blue);
            NamaAplikasi.BackColor = ConsoleColor.Black;
            NamaAplikasi.TampilTengah();

            Tulisan Skolah = new Tulisan();
            Skolah.Text = "WEARNESS EDUCATION CENTER MADIUN";
            Skolah.SetXY(0, 2);
            Skolah.Length = 123;
            Skolah.SetForeColor(ConsoleColor.Blue );
            Skolah.BackColor = ConsoleColor.Black;
            Skolah.TampilTengah();

            Tulisan Alamat = new Tulisan();
            Alamat.Text = "jl.Thamrin 35A Kota Madiun";
            Alamat .SetXY(0, 3);
            Alamat .Length = 123;
            Alamat .SetForeColor(ConsoleColor.Blue);
            Alamat .BackColor = ConsoleColor.Black;
            Alamat .TampilTengah();

            Tulisan bawah = new Tulisan();
            bawah.SetText("IHSAN NUR WAHID").SetXY(0, 26).SetLength(119).SetForeColor(ConsoleColor.DarkBlue).TampilTengah();
            bawah.SetText("INFORMATIKA (II)").SetXY(0, 27).SetLength(120).SetForeColor(ConsoleColor.DarkBlue).TampilTengah();
            bawah.SetText("088").SetXY(0, 28).SetLength(120).SetForeColor(ConsoleColor.DarkGreen).TampilTengah();
            bawah.SetText("@1h2_n").SetXY(110, 28).SetLength(120).SetForeColor(ConsoleColor.DarkRed).Tampil();

            Menu menu = new Menu("JALANKAN", "LIHAT DATA", "TAMBAH DATA", "EDIT DATA", "HAPUS DATA", "KELUAR");
            menu.SetXY(5, 10);
            menu.ForeColor = ConsoleColor.DarkYellow;
            menu.SelectedBackColor = ConsoleColor.DarkCyan;
            menu.SelectedForeColor = ConsoleColor.Black;
            menu.Tampil(); 

            logo();

            bool IsProgramJalan = true;

            while (IsProgramJalan)
            {
                ConsoleKeyInfo Tombol = Console.ReadKey(true);

                if (Tombol.Key == ConsoleKey.DownArrow)
                {
                    //tombol kebawah
                    menu.Next();
                    menu.Tampil();
                }
                else if (Tombol.Key == ConsoleKey.UpArrow)
                {
                    //tombol keatas
                    menu.Prev();
                    menu.Tampil();

                }
                else if (Tombol.Key == ConsoleKey.Enter)
                {
                    //enter
                    int MenuTerpilih = menu.SelectedIndex;

                    if (MenuTerpilih == 0)
                    {
                        //menu jalankan
                        Jalankan();
                    }
                    else if (MenuTerpilih == 1)
                    {
                        //menu lihat data
                        LihatData();
                    }
                    else if (MenuTerpilih == 2)
                    {
                        //menu tambah data
                        TambahData();
                    }
                    else if (MenuTerpilih == 3)
                    {
                        //menu lihat data
                        EditData();
                    }
                    else if (MenuTerpilih == 4)
                    {
                        //menu lihat data
                        HapusData();
                    }
                    else if (MenuTerpilih == 5)
                    {
                        //menu lihat data
                        IsProgramJalan = false ;
                    }
                }
                
            }

            Console.ReadKey();

        }


        static void Jalankan()
        {
            new Clear(32, 6, 78, 17).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".: BEL SEKOLAH SEDANG BERJALAN :.").SetLength(79);
            Judul.TampilTengah();

            Tulisan HariSekarang = new Tulisan().SetXY(33, 9);
            Tulisan JamSekarang = new Tulisan().SetXY(33, 10);

            String QSelect = "SELECT * FROM tb_jadwal WHERE hari=@hari AND jam=@jam;";

            DB.Connect();

            bool Play = true;

            while (Play)
            {
                DateTime Sekarang = DateTime.Now;

                HariSekarang.SetText("HARI SEKARANG : " + Sekarang.ToString("dddd"));
                HariSekarang.Tampil();

                JamSekarang.SetText("JAM SEKARANG  : " + Sekarang.ToString("HH:mm:ss"));
                JamSekarang.Tampil();

                DataTable DT = DB.RunQuery(QSelect,
                    new OleDbParameter("@hari", Sekarang.ToString("dddd")),
                    new OleDbParameter("@jam", Sekarang.ToString("HH:mm")));

                if (DT.Rows.Count > 0)
                {
                    Audio HAAA = new Audio();
                    HAAA.File = "./Suara/" + DT.Rows[0]["Sound"];
                    HAAA.Play();

                    new Tulisan().SetXY(31, 14).SetText("BEL TELAH BERBUNYI!!!").SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();
                    new Tulisan().SetXY(31, 16).SetText(DT.Rows[0]["Ket"].ToString()).SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();

                    Play = false;
                }

                Thread.Sleep(1000);
            }
        }

        static void LihatData()
        {
            new Clear(32, 6, 78, 17).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText("~: LIHAT DATA JADWAL :~").SetLength(79);
            Judul.TampilTengah();

            DB.Connect();
            DataTable DT = DB.RunQuery("SELECT * FROM tb_jadwal;");
            DB.Disconnect();

            new Tulisan("┌────┬─────────────┬────────┬──────────────────────────────┐").SetXY(34,10).Tampil();
            new Tulisan("│ NO.│     HARI    │  JAM   │  KETERANGAN                  │").SetXY(34,11).Tampil();
            new Tulisan("├────┼─────────────┼────────┼──────────────────────────────┤").SetXY(34,12).Tampil();

            for (int i = 0; i < DT.Rows.Count; i++)
            {

                string ID = DT.Rows[i]["id"].ToString();
                string Hari = DT.Rows[i]["Hari"].ToString();
                string Jam = DT.Rows[i]["Jam"].ToString();
                string Keterangan = DT.Rows[i]["ket"].ToString();

                String isi = String.Format("|{0, -4}|{1, -13}|{2, -8}|{3, -30}|", ID, Hari, Jam, Keterangan);
                new Tulisan(isi).SetXY(34, 13 + i).Tampil();
            }
            new Tulisan("└────┴─────────────┴────────┴──────────────────────────────┘").SetXY(34, 13 + DT.Rows.Count).Tampil();
            
        }

    static void TambahData()
    {
            new Clear(32, 6, 78, 17).Tampil();

            Tulisan Baru = new Tulisan();
            Baru.SetXY(31, 7).SetText("~: TAMBAH DATA JADWAL :~").SetLength(79);
            Baru.TampilTengah();

            Inputan HariInput = new Inputan();
            HariInput.Text = "Masukkan Hari      : ";
            HariInput.SetXY(33, 9);

            Inputan JamInput = new Inputan();
            JamInput.Text = "Masukkan Jam       : ";
            JamInput.SetXY(33, 10);

            Inputan KetInput = new Inputan();
            KetInput.Text = "Masukkan Keterangan: ";
            KetInput.SetXY(33, 11);
                //
                Pilihan SoundInput = new Pilihan();
                SoundInput.SetPilihans(
                "5 Menit Akhir Istirahat I.wav",
                "5 Menit Akhir Istirahat II.wav",
                "5 Menit Akhir Kegiatan Keagamaan.wav",
                "5 Menit Awal Kegiatan Keagamaan.wav",
                "5 Menit Awal Upacara.wav",
                "Akhir Pekan 1.wav",
                "Akhir Pekan 2.wav",
                "Akhir Pelajaran A.wav",
                "Akhir Pelajaran B.wav",
                "awal jam ke 1.wav",
                "Istirahat I.wav",
                "Istirahat II.wav",
                "Pelajaran ke 1.wav",
                "Pelajaran ke 2.wav",
                "Pelajaran ke 3.wav",
                "Pelajaran ke 4.wav",
                "Pelajaran ke 5.wav",
                "Pelajaran ke 6.wav",
                "Pelajaran ke 7.wav",
                "Pelajaran ke 8.wav",
                "Pelajaran ke 9.wav",
                "pembuka.wav");

                SoundInput.Text = "Masukkan Audio : ";
                SoundInput.SetXY(33, 13);


            string Hari = HariInput.Read();
            string Jam = JamInput.Read();
            string Ket = KetInput.Read();
            string Sound = SoundInput.Read();

                DB.Connect();
                DB.RunNonQuery("INSERT INTO tb_jadwal ( hari, jam, ket, sound ) VALUES (@hari, @jam, @ket, @sound);",
                    new OleDbParameter("@hari", Hari),
                    new OleDbParameter("@jam", Jam),
                    new OleDbParameter("@ket", Ket),
                    new OleDbParameter("@sound", Sound)
                    );
                DB.Disconnect();
                new Tulisan().SetXY(31, 18).SetText("DATA BERHASIL DISIMPAN").SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();
        }

    static void EditData()
    {
            new Clear(32, 6, 78, 17).Tampil();

            Tulisan Edit = new Tulisan();
            Edit.SetXY(31, 7).SetText("~: EDIT DATA BARU :~").SetLength(79);
            Edit.TampilTengah();

            Inputan IDInputDirubah = new Inputan();
            IDInputDirubah.Text = "Masukkan Id Jadwal Yang Ingin Dimasukkan : ";
            IDInputDirubah.SetXY(33, 9);


            Inputan HariInput = new Inputan();
            HariInput.Text = "Masukkan Hari      : ";
            HariInput.SetXY(33, 11);

            Inputan JamInput = new Inputan();
            JamInput.Text = "Masukkan Jam       : ";
            JamInput.SetXY(33, 12);

            Inputan KetInput = new Inputan();
            KetInput.Text = "Masukkan Keterangan: ";
            KetInput.SetXY(33, 13);

            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(
                "5 Menit Akhir Istirahat I.wav",
                "5 Menit Akhir Istirahat II.wav",
                "5 Menit Akhir Kegiatan Keagamaan.wav",
                "5 Menit Awal Kegiatan Keagamaan.wav",
                "5 Menit Awal Upacara.wav",
                "Akhir Pekan 1.wav",
                "Akhir Pekan 2.wav",
                "Akhir Pelajaran A.wav",
                "Akhir Pelajaran B.wav",
                "awal jam ke 1.wav",
                "Istirahat I.wav",
                "Istirahat II.wav",
                "Pelajaran ke 1.wav",
                "Pelajaran ke 2.wav",
                "Pelajaran ke 3.wav",
                "Pelajaran ke 4.wav",
                "Pelajaran ke 5.wav",
                "Pelajaran ke 6.wav",
                "Pelajaran ke 7.wav",
                "Pelajaran ke 8.wav",
                "Pelajaran ke 9.wav",
                "pembuka.wav");


            SoundInput.Text = "Masukkan Audio     : ";
            SoundInput.SetXY(33, 14);


            string IDRubah = IDInputDirubah.Read();
            string Hari = HariInput.Read();
            string Jam = JamInput.Read();
            string Ket = KetInput.Read();
            string Sound = SoundInput.Read();

            DB.Connect();
            DB.RunNonQuery("UPDATE tb_jadwal SET hari=@hari, jam=@jam, ket=@ket, sound=@sound WHERE id=@id;",
                new OleDbParameter("@hari", Hari),
                new OleDbParameter("@jam", Jam),
                new OleDbParameter("@ket", Ket),
                new OleDbParameter("@sound", Sound),
                new OleDbParameter("@id", IDRubah)
                );
            DB.Disconnect();

            new Tulisan().SetXY(31, 18).SetText("DATA BERHASIL DI UPDATE").SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();


        }
    static void HapusData()
    {
            new Clear(32, 6, 78, 17).Tampil();

            Tulisan Hapus = new Tulisan();
            Hapus.SetXY(31, 7).SetText("~: HAPUS DATA :~").SetLength(79);
            Hapus.TampilTengah();

            Inputan IDInput = new Inputan();
            IDInput.Text = "Masukkan ID Yang Akan Di Hapus : ";
            IDInput.SetXY(33, 9);
            string ID = IDInput.Read();
            
            DB.Connect();
            DB.RunNonQuery("DELETE FROM tb_jadwal WHERE id=@id;",
                new OleDbParameter("@id", ID));
            DB.Disconnect();


            new Tulisan().SetXY(31, 12).SetText("DATA BERHASIL DI HAPUSSS!!!").SetBackColor(ConsoleColor.DarkRed).SetLength(79).TampilTengah();

        }
        static void logo()
        {

            new Tulisan("▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄").SetXY(31, 6).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan("████░███░██░▄▄▄██░█████░▄▄▀██░▄▄▄░██░▄▀▄░██░▄▄▄█").SetXY(31, 7).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan("████░█░█░██░▄▄▄██░█████░█████░███░██░█░█░██░▄▄▄█").SetXY(31, 8).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan("████▄▀▄▀▄██░▀▀▀██░▀▀░██░▀▀▄██░▀▀▀░██░███░██░▀▀▀█").SetXY(31, 9).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan("▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀").SetXY(31, 10).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
          
          

            new Tulisan(" . . . . . . . . . . :0: . . . . . . . . . . ").SetXY(31, 11).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan("..  ..............  ^???^  ..............  ..").SetXY(31, 12).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan("....    ......... .!???JJ!. .........    ....").SetXY(31, 13).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan(". .!0~:.      .. ^7?7~~~7?7^ ...     .:~0!. .").SetXY(31, 14).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan(".. .~YYJ7~^:....~??7.   :???!....:^!7JYY~. ..").SetXY(31, 15).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan(".... :7J?JJJ?7~.^7??~:::!??7^.~77?JJJJ?: ....").SetXY(31, 16).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan("..... ....~J??!  .!???????!.  !7??~.... .....").SetXY(31, 17).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan(".......  .~J??7.  .!?????!.  .!7??~.  .......").SetXY(31, 18).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan("........ :?J???~...!?????!...~77???: ........").SetXY(31, 19).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan("......... :????88!8???????888888??: .........").SetXY(31, 20).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan(".......... ^?77!!1h24n???88888!!8^ ..........").SetXY(31, 21).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan(".......... .~~^^^^^^^^^^^^^^^^~~^............").SetXY(31, 22).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();
            new Tulisan(". . . . .  .JJ??7!888888888!7?JJ?.. . . . . .").SetXY(31, 23).SetLength(88).SetForeColor(ConsoleColor.DarkYellow).TampilTengah();

        }


    }
}
