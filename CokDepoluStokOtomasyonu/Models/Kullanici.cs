using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CokDepoluStokOtomasyonu.Models
{
    public class Kullanici
{
    public int KullaniciID { get; set; }
    public string KullaniciAdi { get; set; }
    public string Rol { get; set; }
}

// Sisteme giriş yapan kişiyi uygulama boyunca burada tutacağız
public static class AktifKullanici
{
    public static Kullanici Bilgi { get; set; }
}
}