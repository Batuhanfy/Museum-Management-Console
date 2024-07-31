using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MuzeProjesi.functions
{
    internal class Login
    {
        public static DataContex connection = new DataContex();

        public static string mail_global;
        public static double money;
        public static int user_id;


        public static void DoLogin()
        {
            // Kullanıcı giriş yapana kadar false, giriş yapınca true 
            bool IsLoginSuccessfull = false;


            // Kullanıcı giriş yapmadıysa döngüye devam et
            while (!IsLoginSuccessfull)
            {

                // Ekranı Yeniler
                Console.Clear();

                // Ekran Boyutu Değişkenleri
                int windowWidth = Console.WindowWidth;
                int windowHeight = Console.WindowHeight;

                // Ekranın Ortasını Belirten Koordinat Tanımları
                int startX = (windowWidth - 60) / 2;
                int startY = (windowHeight - 20) / 2;

                // ----------------------------------------------------------- //
                // KULLANICI GİRİŞİ ÇERÇEVESİ

                // Menünün Sol Üstü (Başlangıcı) Ekranın startX ve startY konumunda olsun
                Console.SetCursorPosition(startX, startY);

                // Arkaplan DarkGreen
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;

                // Çerçeve çizimi
                Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
                Console.SetCursorPosition(startX, startY + 1);
                Console.WriteLine("║               MÜZEYE HOŞGELDİNİZ! GİRİŞ YAPINIZ           ║");
                Console.SetCursorPosition(startX, startY + 2);
                Console.WriteLine("╠═══════════════════════════════════════════════════════════╣");
                Console.SetCursorPosition(startX, startY + 3);
                Console.WriteLine("║  E-Posta Adresiniz     :                                  ║");
                Console.SetCursorPosition(startX, startY + 4);
                Console.WriteLine("║                                                           ║");
                Console.SetCursorPosition(startX, startY + 5);
                Console.WriteLine("║  Müze Erişim Şifreniz  :                                  ║");
                Console.SetCursorPosition(startX, startY + 6);
                Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");

                // Console arkaplanını eski haline döndürmek için
                Console.ResetColor();


                // -------------------------------------------------- // 
                // Butonları Çiz
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(startX, startY + 8);
                Console.WriteLine("╔════════════╗");
                Console.SetCursorPosition(startX, startY + 9);
                Console.WriteLine("║  GİRİŞ YAP ║");
                Console.SetCursorPosition(startX, startY + 10);
                Console.WriteLine("╚════════════╝");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(startX + 47, startY + 8);
                Console.WriteLine("╔════════════╗");
                Console.SetCursorPosition(startX + 47, startY + 9);
                Console.WriteLine("║  KAYIT OL  ║");
                Console.SetCursorPosition(startX + 47, startY + 10);
                Console.WriteLine("╚════════════╝");

                // -------------------------------------------------- // 

                // Kullanıcıdan Veri Girişi (E-MAİL / PASSWORD)

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(startX + 27, startY + 3);
                string username = Console.ReadLine();

                Console.SetCursorPosition(startX + 27, startY + 5);
                string password = Console.ReadLine();

                // -------------------------------------------------- // 

                // Global Olarak Tanımlanan ve daha sonra kullanılacak olan mail bilgisini atadık.
                mail_global = username;


                // -------------------------------------------------- // 

                // Kullanıcı Veri Girişi Yapıldıktan Sonra Buton Kontrollerine Geçilir.

                //  Kullanıcı Herhangi Bir Butona Basıp Basmadığını Belirten
                bool IsClickButton = false;

                // Hangi butonun şuan seçili olduğunu belirtir ( 1= Giriş Yap / 2=Kayıt Ol)                
                int IsSelected = 1;

                while (!IsClickButton)
                {
                    // Seçili olan butonun rengini belirgin yapan işlem

                    switch (IsSelected)
                    {

                        case 1:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(startX, startY + 8);
                            Console.WriteLine("╔════════════╗");
                            Console.SetCursorPosition(startX, startY + 9);
                            Console.WriteLine("║  GİRİŞ YAP ║");
                            Console.SetCursorPosition(startX, startY + 10);
                            Console.WriteLine("╚════════════╝");

                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(startX + 47, startY + 8);
                            Console.WriteLine("╔════════════╗");
                            Console.SetCursorPosition(startX + 47, startY + 9);
                            Console.WriteLine("║  KAYIT OL  ║");
                            Console.SetCursorPosition(startX + 47, startY + 10);
                            Console.WriteLine("╚════════════╝");
                            break;

                        case 2:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(startX, startY + 8);
                            Console.WriteLine("╔════════════╗");
                            Console.SetCursorPosition(startX, startY + 9);
                            Console.WriteLine("║  GİRİŞ YAP ║");
                            Console.SetCursorPosition(startX, startY + 10);
                            Console.WriteLine("╚════════════╝");

                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(startX + 47, startY + 8);
                            Console.WriteLine("╔════════════╗");
                            Console.SetCursorPosition(startX + 47, startY + 9);
                            Console.WriteLine("║  KAYIT OL  ║");
                            Console.SetCursorPosition(startX + 47, startY + 10);
                            Console.WriteLine("╚════════════╝");
                            break;
                    }


                    // Kullanıcıdan Veri Girişi ( Buton Seçimi ) ( SAĞ SOL YÖN TUŞLARI )
                    ConsoleKeyInfo key = Console.ReadKey(true);


                    IsClickButton = (key.Key == ConsoleKey.Enter ? true : false);


                    if (key.Key == ConsoleKey.Enter)
                    {
                        // Butona Tıklanıldı
                        IsClickButton = true;
                    }
                    else
                    {
                        IsClickButton = false;
                        if (IsSelected == 1)
                        {
                            IsSelected = 2;
                        }
                        else
                        {
                            IsSelected = 1;
                        }
                    }
                }
                // GİRİŞ YAP veya KAYIT OL butonları çağırıldığında.
                switch (IsSelected)
                {

                    //   LoginCheck Fonksiyonu ile username,password kontrol edilir

                    //   LoginCheck  0 gelirse -> Kayıtsız kullanıcı (böyle bir kullanıcı yok)
                    //   LoginCheck  1' gelirse -> Normal vatandaş girişi
                    //   LoginCheck  2' gelirse -> Yönetici/Memur girişi

                    case 1:

                        if (LoginCheck(username, password) == 1)
                        {
                            IsLoginSuccessfull = true;
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(startX, startY + 13);

                            Console.WriteLine("Giriş Bilgileri Doğrulandı Giriş Yapılıyor...");

                            Thread.Sleep(2000);
                            IsLoginSuccessfull = true;
                            Console.Clear();

                            Program.AnaMenu();
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }
                        else if (LoginCheck(username, password) == 2)
                        {
                            IsLoginSuccessfull = true;
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(startX, startY + 13);
                            Console.WriteLine("Bilgiler Doğrulandı Giriş Yapılıyor...");

                            Thread.Sleep(2000);
                            IsLoginSuccessfull = true;
                            Console.Clear();

                            Program.YetkiliAnaMenu();
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(startX, startY + 13);
                            Console.WriteLine("Giriş Bilgileri Hatalı");

                            Console.SetCursorPosition(startX, startY + 14);
                            Console.WriteLine("Tekrar denemek için enter'e basınız.");
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }

                        break;
                    case 2:
                        RegisterSystem();
                        break;
                    default:
                        Console.WriteLine("Tuş işleme hatası.");
                        Console.ReadLine();

                        break;
                }

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkGreen;

                Console.ReadLine();


            }
        }

        public static void uyariMesaj(string mesaj)
        {
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;
            int startX = (windowWidth - 60) / 2;
            int startY = (windowHeight - 20) / 2;


            Console.SetCursorPosition(startX, startY + 13);
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 14);
            Console.WriteLine("║" + mesaj);
            Console.SetCursorPosition(startX + 60, startY + 14);
            Console.WriteLine("║");
            Console.SetCursorPosition(startX, startY + 15);
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.SetCursorPosition(startX, startY + 16);

            Thread.Sleep(3000);
            Console.SetCursorPosition(startX, startY + 13);
            Console.WriteLine("                                                               ");
            Console.SetCursorPosition(startX, startY + 14);
            Console.WriteLine("                                                               ");
            Console.SetCursorPosition(startX, startY + 14);
            Console.WriteLine("                                                               ");
            Console.SetCursorPosition(startX, startY + 15);
            Console.WriteLine("                                                               ");
            Console.SetCursorPosition(startX, startY + 16);

        }
        public static bool RegisterSystem()
        {
            // --------------------------------- //
            Console.Clear();

            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;

            // --------------------------------- //

            // ÇERÇEVE 

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║             KAYIT SİSTEMİNE HOŞGELDİNİZ              ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║  Isim                  :                             ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("║                                                      ║");
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine("║  E-Mail                :                             ║");
            Console.SetCursorPosition(startX, startY + 6);
            Console.WriteLine("║                                                      ║");
            Console.SetCursorPosition(startX, startY + 7);
            Console.WriteLine("║  Password              :                             ║");
            Console.SetCursorPosition(startX, startY + 8);
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            // --------------------------------- //
            Console.SetCursorPosition(startX + 27, startY + 3);
            string username = Console.ReadLine();

            Console.SetCursorPosition(startX + 27, startY + 5);
            string email = Console.ReadLine();

            Console.SetCursorPosition(startX + 27, startY + 7);
            string password = Console.ReadLine();
            // --------------------------------- //

            if (username != "" && email != "" && password != "")
            {



                // REGİSTER (KAYIT OLMA SQL KODLARI)
                try
                {
                    string musteriEkle =
                        $"insert into Personals(Name, Email, Password) values('{username}', '{email}', '{password}')\r\n";


                    // --------------------------------- //
                    connection.db.Open();
                    SqlCommand insertSql = new SqlCommand(musteriEkle, connection.db);
                    insertSql.ExecuteNonQuery();
                    connection.db.Close();
                    // --------------------------------- //

                    Console.SetCursorPosition(startX + 0, startY + 10);
                    Console.WriteLine("Üye kaydınız başarılı olmuştur.");

                    return true;
                }
                catch (Exception ex)
                {

                    Console.SetCursorPosition(startX + 0, startY + 10);
                    Console.WriteLine("Personel Kaydı Başarısız. Sebebi: " + ex.Message);
                    return false;
                }
            }
            else
            {
                Console.SetCursorPosition(startX + 0, startY + 10);

                Console.WriteLine("Tüm bilgilerinizi eksiksiz doldurunuz.");
                return false;
            }
            Console.ReadLine();
        }


        public static int LoginCheck(string email, string password)
        {
            try
            {
                connection.db.Open();

                // --------------------------------- //
                // Uygulama Live ortamına geçirilirken kaldırılacak bilgiler.
                if (email == "admin")
                {
                    connection.db.Close();
                    return 2;
                }
                if (email == "user")
                {
                    connection.db.Close();
                    return 1;
                }
                // --------------------------------- //

                // Kullanıcının Şifre Kontrolü Sağlanacak 

                SqlCommand IsUserCheck = new SqlCommand(
                    $"select Email, Password from Personals where Email = '{email}' and Password = '{password}'",
                    connection.db
                );
                SqlDataReader sqlDataReader = IsUserCheck.ExecuteReader();

                // --------------------------------- //

                // Kullanıcı Bulunursa
                if (sqlDataReader.HasRows)
                {

                    /// Balance - Money ( Para  Kontrolü ) 

                    DataContex MoneyCheckConnection = new DataContex();
                    MoneyCheckConnection.db.Open();
                    string MoneySqlstring = $"select Id,Balance from Personals where Email = '{Login.mail_global}'";
                    SqlCommand MoneySql = new SqlCommand(MoneySqlstring, MoneyCheckConnection.db);
                    SqlDataReader MoneySqlReader = MoneySql.ExecuteReader();

                    // Veritabanından Gelen Balance Verisi
                    if (MoneySqlReader.Read())
                    {
                        // Login.cs içerisinden 'money' ve 'user_id' global verilerine veri atama işlemi gerçekleşir.
                        Login.money = Double.Parse("" + MoneySqlReader["balance"]);
                        Login.user_id = int.Parse("" + MoneySqlReader["Id"]);
                        // ------


                    }
                    MoneyCheckConnection.db.Close();






                    sqlDataReader.Close();

                    SqlCommand IsUserAdmin = new SqlCommand(
                        $"select Email, yetki from Personals where Email = '{email}' and yetki = 1",
                        connection.db
                    );
                    SqlDataReader IsUserAdmins = IsUserAdmin.ExecuteReader();

                    if (IsUserAdmins.HasRows)
                    {
                        // --------------------------------- //
                        // Kullanıcı mevcut & Yönetici
                        IsUserAdmins.Close();
                        connection.db.Close();
                        return 2;
                        // --------------------------------- //
                    }
                    else
                    {
                        // --------------------------------- //
                        // Kullanıcı mevcut & Admin/Yönetici değil
                        IsUserAdmins.Close();
                        connection.db.Close();
                        return 1;
                        // --------------------------------- //
                    }
                }
                else
                {
                    sqlDataReader.Close();
                    connection.db.Close();
                    return 0;
                }
            }
            catch (Exception es)
            {
                // --------------------------------- //
                Console.WriteLine("Hata: " + es.Message);
                if (connection.db.State == System.Data.ConnectionState.Open)
                {
                    connection.db.Close();
                }
                return 0;
                // --------------------------------- //
            }
        }

    }
}
