using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using MuzeProjesi.functions;

namespace MuzeProjesi
{
    internal class Program
    {
        // Veritabanı Bağlantısı İçin Global Bir Connection String Oluşturduk.
        static string connectionString = "";
        static bool IsLogin = false;

        static void Main(string[] args)
        {
            // Proje ilk açıldığında kullanıcıdan giriş isteyen fonksiyon
            // functions - > Login.cs
            Console.BackgroundColor = ConsoleColor.White;

            Login.DoLogin();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }

        public static void AnaMenu()
        {
            // Ekran Boyutu Değişkenleri

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 60) / 2;
            int startY = (windowHeight - 20) / 2;

            while (true)
            {
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                // --------------------------------- //
                // ÇERÇEVE
                Console.SetCursorPosition(startX, startY);
                Console.WriteLine(
                    "╔══════════════════════════════════════════════════════════════╗"
                );
                Console.SetCursorPosition(startX, startY + 1);
                Console.WriteLine(
                    "║           MÜZEMİZE HOŞGELDİNİZ DEĞERLİ KULLANICIMIZ          ║"
                );
                Console.SetCursorPosition(startX, startY + 2);
                Console.WriteLine(
                    "╠══════════════════════════════════════════════════════════════╣"
                );
                Console.SetCursorPosition(startX, startY + 3);
                Console.WriteLine(
                    "║  1. Bilet Al                                                 ║"
                );
                Console.SetCursorPosition(startX, startY + 4);
                Console.WriteLine(
                    "║  2. Biletlerimi Görüntüle                                    ║"
                );
                Console.SetCursorPosition(startX, startY + 5);
                Console.WriteLine(
                    "║  3. Bilet İptali                                             ║"
                );
                Console.SetCursorPosition(startX, startY + 6);
                Console.WriteLine(
                    "║  4. Bakiyeniz                                                ║"
                );
                Console.SetCursorPosition(startX, startY + 7);
                Console.WriteLine(
                    "║  5. Bakiye Yükle                                             ║"
                );
                Console.SetCursorPosition(startX, startY + 8);
                Console.WriteLine(
                    "╠══════════════════════════════════════════════════════════════╣"
                );
                Console.SetCursorPosition(startX, startY + 9);
                Console.WriteLine(
                    "║  Seçiminizi yapın:                                           ║"
                );
                Console.SetCursorPosition(startX, startY + 10);
                Console.WriteLine(
                    "╚══════════════════════════════════════════════════════════════╝"
                 );

                // --------------------------------- //

                // Cursor'un Getirileceği Yer - >  ' Seçiminizi Yapın: '
                Console.SetCursorPosition(startX + 22, startY + 9);

                int choice = 0;
                bool secimkontrol = int.TryParse(Console.ReadLine(), out choice);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                // Kullanıcıdan menüden bir öğe seçmesi bekleniyor..
                 switch (choice)
                {
                    case 1:
                        BuyTicket();
                        break;
                    case 2:
                        ListMyTickets();
                        break;
                    case 3:
                        CancelMyTicket();
                        break;
                    case 4:
                        MyBalance();
                        break;
                    case 5:
                        DepositBalance();
                        break;
                    default:
                        Console.SetCursorPosition(startX, startY + 14);
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Geçersiz seçim!");
                        Console.BackgroundColor = ConsoleColor.Black;

                        break;
                }

                // Her işlemden sonra anamenüye dönmek için yazılan mesaj.
                Console.SetCursorPosition(startX, startY + 15);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine("İşleme devam etmek için bir tuşa basınız.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkGreen;

                Console.ReadLine();
            }
        }

        public static void BuyTicket()
        {
            // ------------------------------------------------- //
            // Bilet Alma Fonksiyonu ( Memur / Yetkili tarafından. )
            // ------------------------------------------------- //
            Console.Clear();

            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 60) / 2;
            int startY = (windowHeight - 25) / 2;

            // ÇERÇEVE KODLARI

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine(
                "╔══════════════════════════════════════════════════════════════════╗"
            );
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine(
                "║                       Müze Salonlarımız                          ║"
            );
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine(
                "╠══════════════════════════════════════════════════════════════════╣"
            );

            // Müzeleri getiren veritabanı bağlantısı
            DataContex context = new DataContex();
            try
            {
                context.db.Open();

                // Sorgu Komutları
                string sorgu = "select * from MuzeSalonlari";
                SqlCommand sql = new SqlCommand(sorgu, context.db);
                SqlDataReader reader = sql.ExecuteReader();

                // Veriyi Oku
                while (reader.Read())
                {
                    // Tüm Salonları Listeleme Arayüzü
                    Console.SetCursorPosition(startX, startY + 3);
                    Console.WriteLine(
                        "║    Salon ID         :                                            ║"
                    );
                    Console.SetCursorPosition(startX + 30, startY + 3);
                    Console.WriteLine(reader[0]);
                    // ---------
                    Console.SetCursorPosition(startX, startY + 4);
                    Console.WriteLine(
                        "║    Salon Adı        :                                            ║"
                    );
                    Console.SetCursorPosition(startX + 30, startY + 4);
                    Console.WriteLine(reader[1]);
                    // ---------
                    Console.SetCursorPosition(startX, startY + 5);
                    Console.WriteLine(
                        "║    Salon Ücreti     :                                            ║"
                    );
                    Console.SetCursorPosition(startX + 30, startY + 5);
                    Console.WriteLine(reader[2]);

                    Console.SetCursorPosition(startX, startY + 6);
                    Console.WriteLine(
                        "╠══════════════════════════════════════════════════════════════════╣"
                    );

                    // Her Listelemeden Sonra 4 Blok Aşağısından Devam Ediyor
                    startY += 4;
                }

                // ------------------------------------------------------------//
                // Liste Tamamlandığında
                Console.SetCursorPosition(startX, startY + 6);
                Console.WriteLine(
                    "╚══════════════════════════════════════════════════════════════════╝"
                );
                Console.SetCursorPosition(startX, startY + 3);
                ;
                Console.WriteLine(
                    "║                                                                  ║"
                );
                Console.SetCursorPosition(startX, startY + 4);
                Console.WriteLine(
                    "║                                                                  ║"
                );
                Console.SetCursorPosition(startX + 9, startY + 4);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Hangi Salondan Bilet Almak İstiyorsunuz:");
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(startX, startY + 5);
                Console.WriteLine(
                    "║                                                                  ║"
                );

                context.db.Close();

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                // ------------------------------------------------------------//



                /// Balance - Money ( Para  Kontrolü )

                // Veritabanı bağlantısını aç
                DataContex MoneyCheckConnection = new DataContex();
                MoneyCheckConnection.db.Open();

                // SQL sorgusu oluştur ve çalıştır
                string MoneySqlstring = $"EXEC CheckBalance '{Login.mail_global}'";
                SqlCommand MoneySql = new SqlCommand(MoneySqlstring, MoneyCheckConnection.db);
                SqlDataReader MoneySqlReader = MoneySql.ExecuteReader();

                // Veritabanından gelen Balance verisi
                if (MoneySqlReader.Read())
                {
                    // Login.cs içerisinden 'money' ve 'user_id' global verilerine veri atama işlemi gerçekleşir.
                    Login.money = Double.Parse("" + MoneySqlReader["Balance"]);
                    Login.user_id = int.Parse("" + MoneySqlReader["Id"]);

                    // Bakiyeyi ekrana yazdır
                    Console.SetCursorPosition(startX + 20, startY + 3);
                    Console.WriteLine("Bakiyeniz: " + MoneySqlReader["Balance"] + " TL");
                }

                // Veritabanı bağlantısını kapat
                MoneyCheckConnection.db.Close();

                // Kullanıcıdan Hangi Salona Bilet Alacağını Girmesi İçin 'Double' Veri Beklenir
                Console.SetCursorPosition(startX + 54, startY + 4);
                int InputHall = 0;
                bool InputSelectedHall = int.TryParse(Console.ReadLine(), out InputHall);

                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;

                int startX2 = (windowWidth - 25) / 2;
                int startY2 = (windowHeight - 10) / 2;

                // ------------------------------------------------------------//

                // Seçilen Müzeye Göre Fiyat - Bakiye Karşılaştırması
                DataContex CheckBalanceHall = new DataContex();
                CheckBalanceHall.db.Open();
                string hallpricesqlstring = $"select price from Halls where HallID='{InputHall}'";
                SqlCommand hallpricesql = new SqlCommand(hallpricesqlstring, CheckBalanceHall.db);
                SqlDataReader hallpricesqlreader = hallpricesql.ExecuteReader();
                double pricetheticket;

                while (hallpricesqlreader.Read())
                {
                    // Bakiye Yetersizse

                    pricetheticket = Double.Parse("" + hallpricesqlreader["price"]);
                    if (Double.Parse("" + hallpricesqlreader["price"]) > Login.money)
                    {
                        Console.SetCursorPosition(startX2, startY2);

                        Console.WriteLine("Bakiyeniz Yetersiz.");
                    }
                    // Bakiye Yeterli
                    else
                    {
                        Console.SetCursorPosition(startX2, startY2);
                        Console.WriteLine("Bakiyeniz Yeterli. Bilet Alınıyor");

                        // Bilet Alma Fonksiyonu ( Bilet tablosuna veri girişi )
                        Thread.Sleep(2000);
                        DataContex BuyTicketfromUser = new DataContex();
                        try
                        {
                            // Veritabanı bağlantısını aç
                            BuyTicketfromUser.db.Open();

                            // SQL sorgusu oluştur ve çalıştır
                            string BuyTicketfromUserSqlText =
                                $"EXEC InsertTicket '{Login.user_id}', '{InputHall}'";

                            SqlCommand BuyTicketfromUserSql = new SqlCommand(
                                BuyTicketfromUserSqlText,
                                BuyTicketfromUser.db
                            );
                            BuyTicketfromUserSql.ExecuteNonQuery();

                            // Bilet alımını bildir
                            Console.SetCursorPosition(startX2, startY2 + 2);
                            Console.WriteLine("Başarıyla Bilet Aldınız.");
                            Thread.Sleep(2000);

                            // Bakiyeyi güncelle
                            double newbalance = Login.money - pricetheticket;

                            // SQL sorgusu oluştur ve çalıştır
                            string BuyTicketfromUserSqlText2 =
                                $"EXEC UpdateBalance '{Login.user_id}', {newbalance}";

                            SqlCommand BuyTicketfromUserSql2 = new SqlCommand(
                                BuyTicketfromUserSqlText2,
                                BuyTicketfromUser.db
                            );
                            BuyTicketfromUserSql2.ExecuteNonQuery();

                            // Veritabanı bağlantısını kapat
                            BuyTicketfromUser.db.Close();
                        }
                        catch (Exception es)
                        {
                            Console.WriteLine("Hata: " + es.Message);
                        }

                        Console.SetCursorPosition(startX2, startY2 + 2);
                        Console.WriteLine("Menüye Dönmek İçin Enter Basınız..");
                        Console.ReadLine();
                        AnaMenu();
                    }
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    // ------------------------------------------------------------//
                }
                Console.SetCursorPosition(startX2 - 5, startY2 + 2);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
                Thread.Sleep(1000);
                AnaMenu();
                CheckBalanceHall.db.Close();
                Thread.Sleep(1500);
                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }
        }

        public static void ListMyTickets()
        {
            // ------------------------------------------------- //
            // Bilet Görüntüleme Fonksiyonu
            // ------------------------------------------------- //
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 60) / 2;
            int startY = (windowHeight - 30) / 2;

            // ÇERÇEVE KODLARI
            Console.SetCursorPosition(startX, startY);
            Console.WriteLine(
                "╔══════════════════════════════════════════════════════════════════╗"
            );
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine(
                "║                 Aktif     Biletleriniz  (24 Saat)                ║"
            );
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine(
                "╠══════════════════════════════════════════════════════════════════╣"
            );

            // Müzeleri getiren veritabanı bağlantısı
            DataContex context = new DataContex();

            context.db.Open();

            // Sorgu Komutları
            string sorgu =
                $"select T.TicketID,T.HallID,H.HallName,H.Price from Tickets as T inner join Halls as H on(T.HallID=H.HallID) where T.VisitorID='{Login.user_id}' and T.IsDelete=0 and T.PurchaseTime >= DATEADD(DAY, -1, GETDATE()) ";
            SqlCommand sql = new SqlCommand(sorgu, context.db);
            SqlDataReader reader = sql.ExecuteReader();

            // Veriyi Oku
            while (reader.Read())
            {
                // Tüm Salonları Listeleme Arayüzü
                Console.SetCursorPosition(startX, startY + 3);
                Console.WriteLine(
                    "║    Salon ID         :                                            ║"
                );
                Console.SetCursorPosition(startX + 30, startY + 3);
                Console.WriteLine(reader[1]);
                // ---------
                Console.SetCursorPosition(startX, startY + 4);
                Console.WriteLine(
                    "║    Salon Adı        :                                            ║"
                );
                Console.SetCursorPosition(startX + 30, startY + 4);
                Console.WriteLine(reader[2]);
                // ---------
                Console.SetCursorPosition(startX, startY + 5);
                Console.WriteLine(
                    "║    Salon Ücreti     :                                            ║"
                );
                Console.SetCursorPosition(startX + 30, startY + 5);
                Console.WriteLine(reader[3]);
                // ---------
                Console.SetCursorPosition(startX, startY + 6);
                Console.WriteLine(
                    "║    Biletinizin ID   :                                            ║"
                );
                Console.SetCursorPosition(startX + 30, startY + 6);
                Console.WriteLine(reader[0]);

                Console.SetCursorPosition(startX, startY + 7);
                Console.WriteLine(
                    "╠══════════════════════════════════════════════════════════════════╣"
                );

                // Her Listelemeden Sonra 4 Blok Aşağısından Devam Ediyor
                startY += 5;
            }

            int startX2 = (windowWidth - 25) / 2;
            int startY2 = (windowHeight - 10) / 2;
            Console.ReadLine();
            Console.Clear();
            Console.SetCursorPosition(startX2 - 5, startY2 + 2);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            AnaMenu();
            Thread.Sleep(1500);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void MyBalance()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║            Hesap Bilgileri - Bakiye            ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║   Toplam Bakiyeniz :                           ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string getMyBalance = $"select balance from Personals where Id='{Login.user_id}'";

                context.db.Open();
                SqlCommand getMyBalanceSql = new SqlCommand(getMyBalance, context.db);
                SqlDataReader sqlDataReader = getMyBalanceSql.ExecuteReader();

                Console.SetCursorPosition(startX + 25, startY + 3);
                if (sqlDataReader.Read())
                {
                    Console.WriteLine(sqlDataReader["Balance"] + " TL");
                }
                else
                {
                    Console.WriteLine("-");
                }
                int startX2 = (windowWidth - 25) / 2;
                int startY2 = (windowHeight - 10) / 2;
                Console.ReadLine();
                Console.Clear();
                Console.SetCursorPosition(startX2 - 5, startY2 + 2);
                Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
                Thread.Sleep(1000);
                AnaMenu();
                Thread.Sleep(1500);

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;

                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
            ////////////////////////
        }

        public static void DepositBalance()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║                  Bakiye Yükle                  ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║   Yüklemek İstediğiniz Bakiye       :          ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("║   Banka Kartı (0) / Kredi Kartı (1) :          ║");
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 40, startY + 3);

            // Kullanıcının kaç TL bakiye yüklemek istediğini alır
            double bakiye = 0;
            bool bakiyekontrol = double.TryParse(Console.ReadLine(), out bakiye);

            Console.SetCursorPosition(startX + 40, startY + 4);

            // Kullanıcının Kredi kartı veya Banka kartı
            int kartsecimi = 0;
            bool kartsecimikontrol = int.TryParse(Console.ReadLine(), out kartsecimi);

            if (kartsecimi != 0 && kartsecimi != 1)
            {
                Console.SetCursorPosition(startX, startY + 7);

                Console.WriteLine("Kartı düzgün seçiniz.");

                Console.ReadLine();

                Console.Clear();
                Console.SetCursorPosition(startX + 5, startY + 7);
                Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
                Thread.Sleep(1000);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;

                AnaMenu();
                return;
            }

            if (bakiye < 0 || bakiye == 0)
            {
                Console.SetCursorPosition(startX + 20, startY + 7);

                Console.WriteLine("Bakiyeyi Doğru Oranda Giriniz.");
                Console.ReadLine();

                Console.Clear();
                Console.SetCursorPosition(startX + 5, startY + 7);
                Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
                Thread.Sleep(1000);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;

                AnaMenu();
                return;
            }

            // ------------------
            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                Login.money += bakiye;
                string UpdateTicketSQL =
                    $"update Personals set Balance='{Login.money}' where Id = '{Login.user_id}'";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(UpdateTicketSQL, context.db);
                insertSql.ExecuteNonQuery();

                Console.SetCursorPosition(startX + 5, startY + 9);
                Console.WriteLine("Başarıyla Bakiyeniz Güncellendi. ");
                Thread.Sleep(1000);
                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }

            int startX2 = (windowWidth - 25) / 2;
            int startY2 = (windowHeight - 10) / 2;
            Console.ReadLine();

            Console.Clear();
            Console.SetCursorPosition(startX2 - 5, startY2 + 2);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;

            AnaMenu();
            Thread.Sleep(1500);

            ////////////////////////
        }

        public static void CancelMyTicket()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 40) / 2;
            int startY = (windowHeight - 20) / 2;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║           Bilet İptal Etme Modülü      ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║   Biletinizin ID Bilgisi :             ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 30, startY + 3);
            int ticketID = 0;

            bool tickedIdControl = int.TryParse(Console.ReadLine(), out ticketID);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ////////////////////////
            ///
            if (ticketID == 0 || tickedIdControl == false)
            {
                Console.SetCursorPosition(startX - 5, startY + 2);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("Hatalı giriş.");
                AnaMenu();
            }

            int startX2 = (windowWidth - 25) / 2;
            int startY2 = (windowHeight - 10) / 2;

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;

            DataContex context = new DataContex();

            // BÖYLE BİR BİLET VAR MI KONTROL ET


            // SQL sorgusu oluştur
            string CheckSQL = $"EXEC CheckTicket '{ticketID}', '{Login.user_id}'";

            // Veritabanı bağlantısını aç
            context.db.Open();

            // SQL komutunu oluştur ve çalıştır
            SqlCommand CheckSqlCommand = new SqlCommand(CheckSQL, context.db);
            SqlDataReader insertSqlreader = CheckSqlCommand.ExecuteReader();

            // Sonuçları kontrol et
            if (insertSqlreader.Read() && insertSqlreader.HasRows)
            {
                // Başarıyla bilet iptal edildi
                Console.SetCursorPosition(startX2, startY2);
                Console.WriteLine("Başarıyla Bilet İptal Edildi.");
                Thread.Sleep(1000);
            }
            else
            {
                // Bilet bulunamadı
                Console.SetCursorPosition(startX2, startY2 + 2);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Biletiniz bulunamadı.");
                Thread.Sleep(1000);
                AnaMenu();
            }

            // Veritabanı bağlantısını kapat
            context.db.Close();

            try
            {
                string UpdateTicketSQL =
                    $"update tickets set IsDelete = 1 where TicketID = '{ticketID}' and VisitorID='{Login.user_id}'";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(UpdateTicketSQL, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX2, startY2);
                Console.WriteLine("Başarıyla Bilet İptal Edildi. ");
                Thread.Sleep(1000);
                context.db.Close();

                // BİLET PARASINI ÇEK
                context.db.Open();

                double pricethecancelticket = 0;
                string priceticket =
                    $"select H.Price from Tickets as T inner join Halls as H on(T.HallID=H.HallID) where T.TicketID='{ticketID}'";
                SqlCommand priceticketsql = new SqlCommand(priceticket, context.db);
                SqlDataReader priceticketsqlreader = priceticketsql.ExecuteReader();
                if (priceticketsqlreader.Read())
                {
                    Console.SetCursorPosition(startX2, startY2 + 3);

                    Console.WriteLine("Para İadesi Yapılıyor: " + priceticketsqlreader[0] + " TL");
                    pricethecancelticket = double.Parse("" + priceticketsqlreader[0]);
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.SetCursorPosition(startX2, startY2 + 2);

                    Console.WriteLine("İptal edilen biletin fiyatı okunamadı. ");
                }

                context.db.Close();

                // BİLETİN PARASINI BAKİYEDEN DÜŞ
                context.db.Open();

                double newbalance = Login.money + pricethecancelticket;

                string BuyTicketfromUserSqlText2 =
                    $"update Personals set Balance = {newbalance} where Id='{Login.user_id}'";

                SqlCommand BuyTicketfromUserSql2 = new SqlCommand(
                    BuyTicketfromUserSqlText2,
                    context.db
                );
                BuyTicketfromUserSql2.ExecuteNonQuery();

                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }

            ////////////////////////

            Console.SetCursorPosition(startX2 - 5, startY2 + 2);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            AnaMenu();
        }

        public static void YetkiliAnaMenu()
        {
            // Ekran Boyutu Değişkenleri
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;

            while (true)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(startX, startY);
                Console.WriteLine("╔═════════════════════════════════════════╗");
                Console.SetCursorPosition(startX, startY + 1);
                Console.WriteLine("║           MÜZE YÖNETİM SİSTEMİ          ║");
                Console.SetCursorPosition(startX, startY + 2);
                Console.WriteLine("╠═════════════════════════════════════════╣");
                Console.SetCursorPosition(startX, startY + 3);
                Console.WriteLine("║  1. Ziyaretçi Ekle                      ║");
                Console.SetCursorPosition(startX, startY + 4);
                Console.WriteLine("║  2. Ziyaretçi Güncelle                  ║");
                Console.SetCursorPosition(startX, startY + 5);
                Console.WriteLine("║  3. Ziyaretçi Sil                       ║");
                Console.SetCursorPosition(startX, startY + 6);
                Console.WriteLine("║  4. Salon Ekle                          ║");
                Console.SetCursorPosition(startX, startY + 7);
                Console.WriteLine("║  5. Salon Güncelle                      ║");
                Console.SetCursorPosition(startX, startY + 8);
                Console.WriteLine("║  6. Salon Sil                           ║");
                Console.SetCursorPosition(startX, startY + 9);
                Console.WriteLine("║  7. Bilet Ekle                          ║");
                Console.SetCursorPosition(startX, startY + 10);
                Console.WriteLine("║  8. Bilet Güncelle                      ║");
                Console.SetCursorPosition(startX, startY + 11);
                Console.WriteLine("║  9. Bilet Sil                           ║");
                Console.SetCursorPosition(startX, startY + 12);
                Console.WriteLine("║  10. Eser Ekle                          ║");
                Console.SetCursorPosition(startX, startY + 13);
                Console.WriteLine("║  11. Eser Güncelle                      ║");
                Console.SetCursorPosition(startX, startY + 14);
                Console.WriteLine("║  12. Eser Sil                           ║");
                Console.SetCursorPosition(startX, startY + 15);
                Console.WriteLine("║  13. Tüm Eserleri Görüntüle             ║");
                Console.SetCursorPosition(startX, startY + 16);
                Console.WriteLine("║  14. Kullanıcı Paneline Geç             ║");
                Console.SetCursorPosition(startX, startY + 17);
                Console.WriteLine("╠═════════════════════════════════════════╣");
                Console.SetCursorPosition(startX, startY + 18);
                Console.Write("║  Seçiminizi yapın:                      ║");
                Console.SetCursorPosition(startX, startY + 19);
                Console.WriteLine("╚═════════════════════════════════════════╝");

                // Cursor'un Getirileceği Yer - >  ' Seçiminizi Yapın: '
                Console.SetCursorPosition(startX + 22, startY + 18);

                int choice = 0;

                bool choices = int.TryParse(Console.ReadLine(), out choice);

                if (choices == false)
                {
                    YetkiliAnaMenu();
                    return;
                }

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
                switch (choice)
                {
                    case 1:
                        AddVisitor();
                        break;
                    case 2:
                        UpdateVisitor();
                        break;
                    case 3:
                        DeleteVisitor();
                        break;
                    case 4:
                        AddHall();
                        break;
                    case 5:
                        UpdateHall();
                        break;
                    case 6:
                        DeleteHall();
                        break;
                    case 7:
                        AddTicket();
                        break;
                    case 8:
                        UpdateTicket();
                        break;
                    case 9:
                        DeleteTicket();
                        break;
                    case 10:
                        AddExhibit();
                        break;
                    case 11:
                        UpdateExhibit();
                        break;
                    case 12:
                        DeleteExhibit();
                        break;
                    case 13:
                        ListExhibit();
                        break;
                    case 14:
                        AnaMenu();
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }
                Console.SetCursorPosition(startX + 5, startY + 15);

                Console.WriteLine("İşleme devam etmek için bir tuşa basınız.");
                Console.ReadLine();
            }
        }

        static void ListExhibit()
        {
            // Bu fonksiyon tüm sanat eserlerini görüntülemeye yarar.

            Console.Clear();

            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 60) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine(
                "╔════════════════════════════════════════════════════════════════════╗"
            );
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine(
                "║                           TÜM ESERLER                              ║"
            );
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine(
                "╠════════════════════════════════════════════════════════════════════╣"
            );

            DataContex context = new DataContex();
            try
            {
                context.db.Open();

                // SQL Komutu ile GetAllExhibits prosedürü (proceduresi) çağrılır.
                SqlCommand getAllExhibit = new SqlCommand("GetAllExhibits", context.db);
                getAllExhibit.CommandType = CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = getAllExhibit.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Console.SetCursorPosition(startX, startY + 3);
                    Console.WriteLine(
                        "║    Eserin Adı         :                                            ║"
                    );
                    Console.SetCursorPosition(startX + 25, startY + 3);
                    Console.WriteLine(sqlDataReader["ExhibitName"]);

                    Console.SetCursorPosition(startX, startY + 4);
                    Console.WriteLine(
                        "║    Eserin Açıklaması  :                                            ║"
                    );
                    Console.SetCursorPosition(startX + 25, startY + 4);
                    Console.WriteLine(sqlDataReader["Description"]);

                    Console.SetCursorPosition(startX, startY + 4);
                    Console.WriteLine(
                        "║    Eserin Salonu     :                                             ║"
                    );
                    Console.SetCursorPosition(startX + 25, startY + 4);
                    Console.WriteLine(sqlDataReader["HallName"]);

                    Console.SetCursorPosition(startX, startY + 5);
                    Console.WriteLine(
                        "║    Salonun Ücreti    :                                             ║"
                    );
                    Console.SetCursorPosition(startX + 25, startY + 5);
                    Console.WriteLine(sqlDataReader["Price"]);

                    Console.SetCursorPosition(startX, startY + 6);
                    Console.WriteLine(
                        "╠════════════════════════════════════════════════════════════════════╣"
                    );

                    // Her sanat eserinden sonra 4 blok aşağıdan devam etsin.
                    startY += 4;
                }

                context.db.Close();
                Thread.Sleep(1000);
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }

            Console.SetCursorPosition(startX + 20, startY + 4);
            Console.WriteLine("Tüm eserler listelendi.");
            Console.SetCursorPosition(startX, startY + 6);
            Console.WriteLine(
                "║                                                                    ║"
            );
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine(
                "╚════════════════════════════════════════════════════════════════════╝"
            );

            // Çerçeve Sonu



            // Kullanıcıdan
            Console.SetCursorPosition(startX + 25, startY + 3);
            string VisitorName = Console.ReadLine();
            Console.SetCursorPosition(startX + 25, startY + 4);
            string VisitorMail = Console.ReadLine();
        }

        static void AddVisitor()
        {
            Console.Clear();

            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║            Ziyaretçi Ekleme Modülü             ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║    Ziyaretçi Adı:                              ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("║    Ziyaretçi Email:                            ║");
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 25, startY + 3);
            string VisitorName = Console.ReadLine();
            Console.SetCursorPosition(startX + 25, startY + 4);
            string VisitorMail = Console.ReadLine();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            DataContex context = new DataContex();
            try
            {
                string Ziyaretci_Ekle =
                    $"insert into Visitors(Name,Email) Values('{VisitorName}','{VisitorMail}')";
                context.db.Open();
                SqlCommand insertSql = new SqlCommand(Ziyaretci_Ekle, context.db);
                insertSql.ExecuteNonQuery();

                Console.SetCursorPosition(startX + 15, startY + 7);

                Console.Clear();

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(startX + 12, startY + 2);
                Console.WriteLine("Başarıyla eklendi.");
                Thread.Sleep(1000);
                Console.SetCursorPosition(startX + 5, startY + 4);

                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");

                Console.Read();
                YetkiliAnaMenu();

                Console.WriteLine("Başarıyla Eklendi.");
                Thread.Sleep(2000);

                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }
        }

        static void UpdateVisitor()
        {
            Console.Clear();

            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║            Ziyaretçi Güncelleme Modülü         ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║    Ziyaretçi ID:                               ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("║    Ziyaretçi Adı:                              ║");
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine("║    Ziyaretçi Email:                            ║");
            Console.SetCursorPosition(startX, startY + 6);
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 25, startY + 3);
            int visitorID = int.Parse(Console.ReadLine());
            Console.SetCursorPosition(startX + 25, startY + 4);

            string name = Console.ReadLine();
            Console.SetCursorPosition(startX + 25, startY + 5);

            string email = Console.ReadLine();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string UpdateVisitors =
                    $"update Visitors set Name='{name}',Email='{email}' where VisitorID={visitorID}";
                context.db.Open();
                SqlCommand insertSql = new SqlCommand(UpdateVisitors, context.db);
                insertSql.ExecuteNonQuery();

                Console.Clear();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(startX + 10, startY + 8);
                Console.WriteLine("Başarıyla Güncellendi.");
                Thread.Sleep(1000);
                Console.SetCursorPosition(startX + 10, startY + 10);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");

                Console.Read();
                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }

            Console.Clear();

            Console.SetCursorPosition(startX, startY + 10);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();
        }

        static void DeleteVisitor()
        {
            Console.Clear();

            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║            Ziyaretçi Silme Modülü              ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║    Ziyaretçi ID:                               ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 25, startY + 3);
            int visitorID = 0;
            bool visitorId_input = int.TryParse(Console.ReadLine(), out visitorID);

            if (visitorId_input == false)
            {
                Console.SetCursorPosition(startX + 15, startY + 7);

                Console.WriteLine("Değer int olmadığı için işlem devam etmiyor.");
                Console.ReadLine();
                return;
            }
            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string DeleteVisitors =
                    $"update Visitors set IsDelete=1 where VisitorID={visitorID}";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(DeleteVisitors, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX + 15, startY + 7);

                Console.WriteLine("Başarıyla Silindi.");
                Thread.Sleep(1000);
                Console.SetCursorPosition(startX + 15, startY + 9);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");
                context.db.Close();
            }
            catch (Exception es)
            {
                Console.SetCursorPosition(startX + 15, startY + 7);

                Console.WriteLine("Hata: " + es.Message);
            }

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.ReadLine();

            ////////////////////////

            Console.SetCursorPosition(startX - 5, startY + 2);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();
        }

        static void AddHall()
        {
            Console.Clear();

            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║               Salon Ekleme Modülü              ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║    Salon Adı    :                              ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("║                                                ║");
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine("║    Salon Ücreti :                              ║");
            Console.SetCursorPosition(startX, startY + 6);
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 25, startY + 3);
            string hallName = Console.ReadLine();

            Console.SetCursorPosition(startX + 25, startY + 5);
            decimal price = decimal.Parse(Console.ReadLine());

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string AddHalls =
                    $"insert into Halls(HallName,Price) Values('{hallName}','{price}')";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(AddHalls, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX + 15, startY + 9);

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Başarıyla Salon Eklendi.");
                Thread.Sleep(1000);
                Console.SetCursorPosition(startX + 15, startY + 11);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");
                context.db.Close();
            }
            catch (Exception es)
            {
                Console.SetCursorPosition(startX + 15, startY + 7);

                Console.WriteLine("Hata: " + es.Message);
            }

            ////////////////////////
            ///


            Console.SetCursorPosition(startX - 5, startY + 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.Clear();
            Console.SetCursorPosition(startX + 5, startY + 7);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();
        }

        static void UpdateHall()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(startX, startY);

            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║             Salon Güncelleme Modülü            ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║   Salon ID :                                   ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("║                                                ║");
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine("║   Salon Adı :                                  ║");
            Console.SetCursorPosition(startX, startY + 6);
            Console.WriteLine("║                                                ║");
            Console.SetCursorPosition(startX, startY + 7);
            Console.WriteLine("║   Salon Ücreti :                               ║");
            Console.SetCursorPosition(startX, startY + 8);
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 20, startY + 3);
            int hallID = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(startX + 20, startY + 5);
            string hallName = Console.ReadLine();

            Console.SetCursorPosition(startX + 20, startY + 7);
            decimal price = decimal.Parse(Console.ReadLine());

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string UpdateHalls =
                    $"update Halls set HallName='{hallName}',Price='{price}' where HallID={hallID}";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(UpdateHalls, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX + 13, startY + 11);

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Başarıyla Salon Güncellendi.");
                Thread.Sleep(1000);
                Console.SetCursorPosition(startX + 13, startY + 13);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");
                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }

            ////////////////////////
            ///
            Console.SetCursorPosition(startX - 5, startY + 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetCursorPosition(startX + 5, startY + 7);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();
        }

        static void DeleteHall()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║              Salon Silme Modülü                ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║   Salon ID :                                   ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 20, startY + 3);
            int hallID = int.Parse(Console.ReadLine());

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string DeleteHall = $"update Halls set IsDelete=1 where HallID={hallID}";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(DeleteHall, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX + 15, startY + 8);

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Başarıyla Salon Silindi.");

                Thread.Sleep(1000);
                Console.SetCursorPosition(startX + 10, startY + 10);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");
                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }

            Console.SetCursorPosition(startX - 5, startY + 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.Clear();
            Console.SetCursorPosition(startX + 5, startY + 7);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();
        }

        static void AddTicket()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(startX, startY);

            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║               Bilet Alma Modülü                ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║   Ziyaretçi ID :                               ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("║   Salon ID     :                               ║");
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 20, startY + 3);

            int visitorID = int.Parse(Console.ReadLine());
            Console.SetCursorPosition(startX + 20, startY + 4);
            int hallID = int.Parse(Console.ReadLine());

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string AddTicketSql =
                    $"insert into Tickets(VisitorID,HallID,PurchaseTime) Values('{visitorID}','{hallID}',GETDATE())";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(AddTicketSql, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX, startY + 7);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Başarıyla Bilet Eklendi.");

                Thread.Sleep(1000);
                Console.SetCursorPosition(startX, startY + 10);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");

                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }

            Console.SetCursorPosition(startX - 5, startY + 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.Clear();
            Console.SetCursorPosition(startX + 5, startY + 7);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();

            ////////////////////////
        }

        static void UpdateTicket()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 80) / 2;
            int startY = (windowHeight - 20) / 2;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(startX, startY);
            Console.WriteLine(
                "╔═══════════════════════════════════════════════════════════════════════════╗"
            );
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine(
                "║                      Bilet Güncelleme Modülü                              ║"
            );
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine(
                "╠═══════════════════════════════════════════════════════════════════════════╣"
            );
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine(
                "║   Bilet ID                                  :                             ║"
            );
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine(
                "║   Ziyaretçi ID                              :                             ║"
            );
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine(
                "║   Salon ID                                  :                             ║"
            );
            Console.SetCursorPosition(startX, startY + 6);
            Console.WriteLine(
                "║   Biletin Yeni Tarihi (yyyy-MM-dd HH:mm:ss) :                             ║"
            );
            Console.SetCursorPosition(startX, startY + 7);
            Console.WriteLine(
                "╚═══════════════════════════════════════════════════════════════════════════╝"
            );

            Console.SetCursorPosition(startX + 50, startY + 3);
            int ticketID = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(startX + 50, startY + 4);
            int visitorID = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(startX + 50, startY + 5);
            int hallID = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(startX + 50, startY + 6);
            DateTime purchaseTime = DateTime.Parse(Console.ReadLine());
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string UpdateTicketSQL =
                    $"update tickets set PurchaseTime = '{purchaseTime}' where TicketID = '{ticketID}' and VisitorID = '{visitorID}' and HallID = '{hallID}'";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(UpdateTicketSQL, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX + 10, startY + 9);

                Console.SetCursorPosition(startX, startY + 9);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Başarıyla Bilet Güncellendi");

                Thread.Sleep(1000);
                Console.SetCursorPosition(startX, startY + 11);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");

                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }
            Console.SetCursorPosition(startX - 5, startY + 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.Clear();
            Console.SetCursorPosition(startX + 5, startY + 7);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();
            ////////////////////////
        }

        static void DeleteTicket()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 40) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║        Bilet Silme Modülü          ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║   Bilet ID       :                 ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("╚════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 22, startY + 3);
            int ticketID = int.Parse(Console.ReadLine());

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;

            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string UpdateTicketSQL =
                    $"update tickets set IsDelete = 1 where TicketID = '{ticketID}'";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(UpdateTicketSQL, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX, startY + 9);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Başarıyla Bilet Silindi.");

                Thread.Sleep(1000);
                Console.SetCursorPosition(startX, startY + 11);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");
                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }

            Console.SetCursorPosition(startX - 5, startY + 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.Clear();
            Console.SetCursorPosition(startX + 5, startY + 7);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();

            ////////////////////////
        }

        static void AddExhibit()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 90) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(startX, startY);
            Console.WriteLine(
                "╔════════════════════════════════════════════════════════════════════════════════════╗"
            );
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine(
                "║                        Yeni Eser Tanımlama Modülü                                  ║"
            );
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine(
                "╠════════════════════════════════════════════════════════════════════════════════════╣"
            );
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine(
                "║   Salon ID         :                                                               ║"
            );
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine(
                "║   Eser Adı         :                                                               ║"
            );
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine(
                "║   Eser Açıklaması  :                                                               ║"
            );
            Console.SetCursorPosition(startX, startY + 6);
            Console.WriteLine(
                "╚════════════════════════════════════════════════════════════════════════════════════╝"
            );

            Console.SetCursorPosition(startX + 25, startY + 3);
            int hallID = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(startX + 25, startY + 4);
            string exhibitName = Console.ReadLine();

            Console.SetCursorPosition(startX + 25, startY + 5);
            string description = Console.ReadLine();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ////////////////////////
            ///
            DataContex context = new DataContex();
            try
            {
                string AddExhibit =
                    $"insert into Exhibits(HallID,ExhibitName,Description) values('{hallID}','{exhibitName}','{description}')";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(AddExhibit, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX, startY + 9);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Başarıyla Eser Eklendi.");

                Thread.Sleep(1000);
                Console.SetCursorPosition(startX, startY + 11);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");
                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }

            ////////////////////////
            ///

            Console.SetCursorPosition(startX - 5, startY + 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.Clear();
            Console.SetCursorPosition(startX + 5, startY + 7);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();
        }

        static void UpdateExhibit()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 90) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine(
                "╔════════════════════════════════════════════════════════════════════════════════════╗"
            );
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine(
                "║                             Eser Güncelleme Modülü                                 ║"
            );
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine(
                "╠════════════════════════════════════════════════════════════════════════════════════╣"
            );
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine(
                "║   Eser ID                 :                                                        ║"
            );
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine(
                "║   Eserin Salon ID         :                                                        ║"
            );
            Console.SetCursorPosition(startX, startY + 5);
            Console.WriteLine(
                "║   Eserin Yeni Adı         :                                                        ║"
            );
            Console.SetCursorPosition(startX, startY + 6);
            Console.WriteLine(
                "║   Eserin Yeni Açıklaması  :                                                        ║"
            );
            Console.SetCursorPosition(startX, startY + 7);
            Console.WriteLine(
                "╚════════════════════════════════════════════════════════════════════════════════════╝"
            );

            Console.SetCursorPosition(startX + 30, startY + 3);
            int exhibitID = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(startX + 30, startY + 4);
            int hallID = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(startX + 30, startY + 5);
            string exhibitName = Console.ReadLine();

            Console.SetCursorPosition(startX + 30, startY + 6);
            string description = Console.ReadLine();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string UpdateExhibit =
                    $"update Exhibits set ExhibitName = '{exhibitName}', Description = '{description}' where ExhibitID = '{exhibitID}' and HallID='{hallID}' ";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(UpdateExhibit, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX, startY + 9);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Başarıyla Eser Güncellendi.");

                Thread.Sleep(1000);
                Console.SetCursorPosition(startX, startY + 11);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");
                context.db.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine("Hata: " + es.Message);
            }

            ////////////////////////
            ///

            Console.SetCursorPosition(startX - 5, startY + 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.Clear();
            Console.SetCursorPosition(startX + 5, startY + 7);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();
        }

        static void DeleteExhibit()
        {
            Console.Clear();
            // Ekran Boyutu Değişkenleri

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 50) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(startX, startY);
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startX, startY + 1);
            Console.WriteLine("║               Eser Silme Modülü                ║");
            Console.SetCursorPosition(startX, startY + 2);
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startX, startY + 3);
            Console.WriteLine("║    Eser ID :                                   ║");
            Console.SetCursorPosition(startX, startY + 4);
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            Console.SetCursorPosition(startX + 15, startY + 3);
            int exhibitID = int.Parse(Console.ReadLine());

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ////////////////////////
            DataContex context = new DataContex();
            try
            {
                string DeleteExhibits =
                    $"update Exhibits set IsDelete=1 where ExhibitID={exhibitID}";

                context.db.Open();
                SqlCommand insertSql = new SqlCommand(DeleteExhibits, context.db);
                insertSql.ExecuteNonQuery();
                Console.SetCursorPosition(startX + 15, startY + 7);

                Console.SetCursorPosition(startX, startY + 9);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Başarıyla Eser Silindi.");

                Thread.Sleep(1000);
                Console.SetCursorPosition(startX, startY + 11);
                Console.WriteLine("Ana Menüye Dönmek İçin Enter basınız.");

                context.db.Close();
            }
            catch (Exception es)
            {
                Console.SetCursorPosition(startX + 15, startY + 7);

                Console.WriteLine("Hata: " + es.Message);
            }

            Console.ReadLine();

            Console.SetCursorPosition(startX - 5, startY + 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.Clear();
            Console.SetCursorPosition(startX + 5, startY + 7);
            Console.WriteLine("Ana Menüye Yönlendiriliyorsunuz...");
            Thread.Sleep(1000);
            YetkiliAnaMenu();
        }
    }
}
