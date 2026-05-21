using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using CokDepoluStokOtomasyonu.DataAccess;
using CokDepoluStokOtomasyonu.Models;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();

            // 1. ŞİFREYİ VARSAYILAN OLARAK YILDIZ YAPIYORUZ
            txtSifre.PasswordChar = '*';

            // 2. GÖZ BUTONUNUN OLAYLARINI BAĞLIYORUZ
            btnGoster.MouseDown += BtnGoster_MouseDown;
            btnGoster.MouseUp += BtnGoster_MouseUp;
        }

        // Fareyle göz butonuna basılı tutulduğunda şifreyi gösterir
        private void BtnGoster_MouseDown(object sender, MouseEventArgs e)
        {
            txtSifre.PasswordChar = '\0';
        }

        // Fare bırakıldığında şifreyi tekrar gizler
        private void BtnGoster_MouseUp(object sender, MouseEventArgs e)
        {
            txtSifre.PasswordChar = '*';
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            // Boş alan kontrolü (Veri Doğrulama Kuralı)
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();

                    // DÜZENLEME 1: AktifMi bilgisini de çekiyoruz ki kullanıcıya özel uyarı verebilelim. 
                    // (AND AktifMi = 1 kısmını kaldırdık, kontrolü aşağıda C# ile yapacağız)
                    string query = "SELECT KullaniciID, KullaniciAdi, Rol, AktifMi FROM Kullanicilar WHERE KullaniciAdi = @kAdi AND Sifre = @sifre";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kAdi", txtKullaniciAdi.Text);
                        cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Eğer şifre ve kullanıcı adı eşleştiyse
                            {
                                // DÜZENLEME 2: Kullanıcının hesabı aktif mi diye kontrol ediyoruz
                                bool aktifMi = Convert.ToBoolean(reader["AktifMi"]);

                                if (!aktifMi) // Eğer aktifMi false (0) ise
                                {
                                    MessageBox.Show("Hesabınız pasife alınmıştır! Sisteme giriş yapamazsınız. Lütfen yönetici ile iletişime geçin.", "Giriş Engellendi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return; // İşlemi kes ve girişi engelle
                                }

                                // Hesabı aktifse giriş işlemlerine devam et...
                                AktifKullanici.Bilgi = new Kullanici
                                {
                                    KullaniciID = Convert.ToInt32(reader["KullaniciID"]),
                                    KullaniciAdi = reader["KullaniciAdi"].ToString(),
                                    Rol = reader["Rol"].ToString()
                                };

                                MessageBox.Show($"Hoşgeldiniz, {AktifKullanici.Bilgi.KullaniciAdi}! Rolünüz: {AktifKullanici.Bilgi.Rol}", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // --- SON GİRİŞ TARİHİNİ GÜNCELLEME KODU ---
                                try
                                {
                                    using (var connUpdate = Baglanti.GetConnection())
                                    {
                                        connUpdate.Open();
                                        string updateQuery = "UPDATE Kullanicilar SET SonGirisTarihi = GETDATE() WHERE KullaniciID = @girisYapanID";

                                        // DÜZENLEME 3: Gereksiz uzun "Microsoft.Data.SqlClient" ekleri (cast işlemleri) sadeleştirildi
                                        using (var cmdUpdate = new SqlCommand(updateQuery, connUpdate))
                                        {
                                            cmdUpdate.Parameters.AddWithValue("@girisYapanID", AktifKullanici.Bilgi.KullaniciID);
                                            cmdUpdate.ExecuteNonQuery();
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    // Hata olursa program çökmesin, sessizce geçsin.
                                }
                                // --- GÜNCELLEME KODU BİTİŞİ ---

                                // Ana menüyü oluştur ve göster
                                FrmAnaMenu anaMenu = new FrmAnaMenu();
                                anaMenu.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Hatalı kullanıcı adı veya şifre!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hocanın istediği Try-Catch hata yönetimi
                MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtKullaniciAdi_KeyDown(object sender, KeyEventArgs e)
        {
            // Eğer basılan tuş Enter ise
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Windows'un çıkaracağı o rahatsız edici "Dınn" uyarı sesini engeller
                txtSifre.Focus(); // İmleci doğrudan şifre kutusuna taşır
            }
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnGiris;
        }
    }
}