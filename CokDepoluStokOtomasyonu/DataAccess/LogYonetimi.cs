using Microsoft.Data.SqlClient;
using System;
using CokDepoluStokOtomasyonu.Models; // AktifKullanici class'ının olduğu yer

namespace CokDepoluStokOtomasyonu.DataAccess
{
    public static class LogYonetimi
    {
        // İstediğimiz formdan LogYonetimi.LogEkle(...) diyerek bu metodu çağıracağız
        public static void LogEkle(string islemTipi, string tabloAdi, string aciklama)
        {
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO IslemLoglari (KullaniciID, IslemTipi, TabloAdi, Aciklama) VALUES (@kID, @islem, @tablo, @detay)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Sistemi o an kullanan kişinin ID'sini otomatik alıyoruz
                        cmd.Parameters.AddWithValue("@kID", AktifKullanici.Bilgi.KullaniciID);
                        cmd.Parameters.AddWithValue("@islem", islemTipi);
                        cmd.Parameters.AddWithValue("@tablo", tabloAdi);
                        cmd.Parameters.AddWithValue("@detay", aciklama);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                // Bilerek boş bıraktık: Eğer log yazılırken veritabanı anlık yavaşlarsa ana program çökmesin, işlemi yapmaya devam etsin.
            }
        }
    }
}