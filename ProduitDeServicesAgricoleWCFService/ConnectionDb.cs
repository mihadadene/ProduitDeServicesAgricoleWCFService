using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduitDeServicesAgricoleWCFService
{
   public class Connection
    {
        public static MySqlConnection cnx = null;

        public static MySqlConnection GetInstance()
        {
            String cs = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = demodb; Integrated Security = True";
            MySqlConnection cnx = new MySqlConnection(cs);
            try
            {
                cnx.Open();
                Console.WriteLine("Connected...");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return cnx;
        }
    }
}
