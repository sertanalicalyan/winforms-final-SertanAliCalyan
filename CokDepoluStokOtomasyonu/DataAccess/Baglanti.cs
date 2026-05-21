using System;
using Microsoft.Data.SqlClient;

namespace CokDepoluStokOtomasyonu.DataAccess
{
    public static class Baglanti
    {
        // SQL Server bağlantı adresimiz (Connection String)
        // Eğer SSMS'te farklı bir sunucu adı kullandıysan ".\SQLEXPRESS" kısmını ona göre değiştirmelisin.
        private static string connectionString = @"Server=.\SQLEXPRESS;Database=StokOtomasyonDB;Trusted_Connection=True;Encrypt=False;";

        // Veritabanı bağlantısını açıp bize teslim eden metod
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}