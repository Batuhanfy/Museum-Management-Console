using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MuzeProjesi
{
    internal class MessageBox
    {

         public static void Show(string message)
        {
            Console.Clear();

            // Ekran Boyutu Değişkenleri
            Console.BackgroundColor = ConsoleColor.DarkGreen;

             int windowWidth = Console.WindowWidth;
             int windowHeight = Console.WindowHeight;

            // Ekranın Ortasını Belirten Kordinat Tanımları

            int startX = (windowWidth - 60) / 2;
            int startY = (windowHeight - 20) / 2;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine(message);

            
             Thread.Sleep(1800);
        }
    }
  
    
}
