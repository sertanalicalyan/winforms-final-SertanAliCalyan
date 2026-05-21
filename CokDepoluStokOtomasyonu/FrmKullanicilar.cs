using CokDepoluStokOtomasyonu.DataAccess; // Senin bağlantı sınıfının olduğu yer
using Microsoft.Data.SqlClient; // veya System.Data.SqlClient
using System;
using System.Data;
using System.Windows.Forms;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmKullanicilar : Form
    {
        bool pasifleriGoster = false; // Aktif/Pasif listesi arasında geçiş için

        public FrmKullanicilar()
        {
            InitializeComponent();
            this.Load += FrmKullanicilar_Load;
            btnPasifKullanicilar.Click += BtnPasifKullanicilar_Click;
            dgvKullanicilar.CellContentClick += DgvKullanicilar_CellContentClick;
        }

        private void FrmKullanicilar_Load(object sender, EventArgs e)
        {
            ListeyiGetir();
        }

        private void ListeyiGetir()
        {
            dgvKullanicilar.Columns.Clear(); // Önceki sütunları temizle

            using (SqlConnection conn = Baglanti.GetConnection())
            {
                conn.Open();
                // DÜZELTME 1: IsDeleted yerine veritabanındaki gerçek ad olan "AktifMi" kullanıldı.
                string query = "SELECT KullaniciID, KullaniciAdi AS [Kullanıcı Adı], Rol AS [Yetki Rolü], SonGirisTarihi AS [Son Giriş] FROM Kullanicilar WHERE AktifMi = @aktifMi";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                // DÜZELTME 2: Eğer pasifleri gösteriyorsak (true) AktifMi=0 olanları, aktifleri gösteriyorsak AktifMi=1 olanları çekiyoruz.
                da.SelectCommand.Parameters.AddWithValue("@aktifMi", pasifleriGoster ? 0 : 1);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvKullanicilar.DataSource = dt;
            }

            // ID Sütununu gizle
            dgvKullanicilar.Columns["KullaniciID"].Visible = false;

            // DÜZENLE Butonu Sütunu
            DataGridViewButtonColumn btnDuzenle = new DataGridViewButtonColumn();
            btnDuzenle.Name = "btnDuzenle";
            btnDuzenle.HeaderText = "İşlem";
            btnDuzenle.Text = "Düzenle";
            btnDuzenle.UseColumnTextForButtonValue = true;
            dgvKullanicilar.Columns.Add(btnDuzenle);

            // SİL (veya AKTİF ET) Butonu Sütunu
            DataGridViewButtonColumn btnSil = new DataGridViewButtonColumn();
            btnSil.Name = "btnSil";
            btnSil.HeaderText = "Durum Değiştir";
            btnSil.Text = pasifleriGoster ? "Aktif Et" : "Sil"; // DÜZELTME 3: Buton metni duruma göre değişecek
            btnSil.UseColumnTextForButtonValue = true;
            dgvKullanicilar.Columns.Add(btnSil);
        }

        private void BtnPasifKullanicilar_Click(object sender, EventArgs e)
        {
            pasifleriGoster = !pasifleriGoster; // Durumu tersine çevir

            if (pasifleriGoster)
                btnPasifKullanicilar.Text = "Aktif Kullanıcıları Göster";
            else
                btnPasifKullanicilar.Text = "Pasif Kullanıcıları Göster";

            ListeyiGetir(); // Listeyi yeni duruma göre yenile
        }

        // Eski metot (Designer patlamasın diye silinmedi, içi boşaltıldı ki çift tıklama hatası yapmasın)
        private void dgvKullanicilar_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Kullanılmıyor, asıl işlemler DgvKullanicilar_CellContentClick içinde.
        }

        // Eski metot (Designer patlamasın diye tutuluyor)
        private void KullaniciDurumDegistir(int id, int isDeleted)
        {
            // Kullanılmıyor
        }

        private void FrmKullanicilar_Load_1(object sender, EventArgs e)
        {
            if (AktifKullanici.Bilgi.Rol != "Admin")
            {
                btnYeniKullanici.Visible = false;
            }
        }

        private void btnYeniKullanici_Click(object sender, EventArgs e)
        {
            FrmKullaniciEkle yeniForm = new FrmKullaniciEkle();
            yeniForm.ShowDialog();
        }

        // ASIL ÇALIŞAN TIKLAMA METODUMUZ
        private void DgvKullanicilar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Eğer tıklanan yer tablonun başlığıysa işlem yapma
            if (e.RowIndex < 0) return;

            string tiklananSutunIsmi = dgvKullanicilar.Columns[e.ColumnIndex].Name;

            // --- 1. DÜZENLE BUTONU İŞLEMLERİ ---
            if (tiklananSutunIsmi == "btnDuzenle")
            {
                DataGridViewRow row = dgvKullanicilar.Rows[e.RowIndex];
                FrmKullaniciDuzenle frmDuzenle = new FrmKullaniciDuzenle();

                frmDuzenle.SeciliID = row.Cells["KullaniciID"].Value.ToString();
                frmDuzenle.SeciliAd = row.Cells["Kullanıcı Adı"].Value.ToString();
                frmDuzenle.SeciliRol = row.Cells["Yetki Rolü"].Value.ToString();
                frmDuzenle.SeciliSifre = "";

                frmDuzenle.ShowDialog();
            }
            // --- 2. DURUM DEĞİŞTİR (SİL / AKTİF ET) BUTONU İŞLEMLERİ ---
            else if (tiklananSutunIsmi == "btnSil")
            {
                DataGridViewRow row = dgvKullanicilar.Rows[e.RowIndex];
                string seciliID = row.Cells["KullaniciID"].Value.ToString();
                string kAd = row.Cells["Kullanıcı Adı"].Value.ToString();

                // DÜZELTME 4: Hangi tabloda olduğumuza göre mesajı ayarlıyoruz
                string islemMesaji = pasifleriGoster ? $"{kAd} adlı kullanıcıyı tekrar AKTİF ETMEK istediğinize emin misiniz?" : $"{kAd} adlı kullanıcıyı PASİFE ALMAK (silmek) istediğinize emin misiniz?";

                DialogResult cevap = MessageBox.Show(islemMesaji, "Durum Değiştirme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (cevap == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = Baglanti.GetConnection())
                        {
                            conn.Open();
                            // Pasif tablosundaysak durumu 1 (Aktif) yap, Aktif tablosundaysak durumu 0 (Pasif) yap
                            int yeniDurum = pasifleriGoster ? 1 : 0;

                            string updateQuery = "UPDATE Kullanicilar SET AktifMi = @durum WHERE KullaniciID = @id";

                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@durum", yeniDurum);
                                cmd.Parameters.AddWithValue("@id", seciliID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        string sonucMesaji = pasifleriGoster ? "Kullanıcı başarıyla aktifleştirildi." : "Kullanıcı başarıyla pasife alındı.";
                        MessageBox.Show(sonucMesaji, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // İşlem başarılı olduktan hemen sonra logluyoruz:
                        string yapilanIslem = pasifleriGoster ? "Aktifleştirme" : "Pasife Alma";
                        LogYonetimi.LogEkle(yapilanIslem, "Kullanicilar", $"{kAd} adlı kullanıcının durumu {AktifKullanici.Bilgi.KullaniciAdi} tarafından değiştirildi.");

                        // DÜZELTME 5: RemoveAt yerine ListeyiGetir() çağırıyoruz. 
                        // Böylece anında o anki (aktif veya pasif) listeyi güncelleyip yanlış kişilerin ekranda kalmasını engelliyoruz.
                        ListeyiGetir();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("İşlem sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Eski metot (Silinmedi)
        private void btnPasifKullanicilar_Click_1(object sender, EventArgs e)
        {
        }

        private void btnLoglariGoster_Click(object sender, EventArgs e)
        {
            // Yeni oluşturduğumuz log ekranını çağırıyoruz
            FrmIslemLoglari logFormu = new FrmIslemLoglari();
            logFormu.ShowDialog(); // Formu üst pencere olarak açar
        }
    }
}