using CokDepoluStokOtomasyonu.Models; // Senin orijinal modellerini buraya dahil ettik

namespace CokDepoluStokOtomasyonu
{
    public static class AktifKullanici
    {
        // Sistemdeki aktif kullanıcıyı senin kendi 'Kullanici' modelin üzerinden tutuyoruz
        public static Kullanici Bilgi { get; set; } = new Kullanici();
    }
}