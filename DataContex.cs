using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzeProjesi
{
    internal class DataContex
    {

        public SqlConnection db;
        public DataContex()
        {
            try
            {
                db = new SqlConnection(@"data source=DESKTOP-UO2Q3MT\SQLEXPRESS;initial catalog=Muzeler;integrated security=true");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Bağlantı Hatası:");
                Console.WriteLine(ex.Message);
            }


        }
    }
}

