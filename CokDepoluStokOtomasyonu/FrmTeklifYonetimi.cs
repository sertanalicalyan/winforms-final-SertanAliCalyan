using CokDepoluStokOtomasyonu.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmTeklifYonetimi : Form
    {
        public FrmTeklifYonetimi()
        {
            InitializeComponent();

            // Olayları (Eventleri) kod üzerinden bağlıyoruz
            this.Load += FrmTeklifYonetimi_Load;
            dgvAcikTalepler.SelectionChanged += DgvAcikTalepler_SelectionChanged;
            btnTeklifKaydet.Click += BtnTeklifKaydet_Click;
            btnTalebiOnayla.Click += BtnTalebiOnayla_Click;
        }

        private void FrmTeklifYonetimi_Load(object sender, EventArgs e)
        {
            AcikTalepleriListele();
        }

        // 1. AÇIK TALEPLERİ LİSTELE
        private void AcikTalepleriListele()
        {
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            t.TalepID, 
                            u.UrunAdi AS [Talep Edilen Ürün], 
                            t.Miktar, 
                            t.TalepTarihi 
                        FROM SatinAlmaTalepleri t
                        INNER JOIN Urunler u ON t.UrunID = u.UrunID
                        WHERE t.Durum = 'Bekliyor'";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvAcikTalepler.DataSource = dt;
                    if (dgvAcikTalepler.Columns.Contains("TalepID"))
                        dgvAcikTalepler.Columns["TalepID"].Visible = false; // ID'yi gizle
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Talepler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        // 2. TABLODA SEÇİM DEĞİŞTİKÇE O TALEBİN TEKLİFLERİNİ GETİR
        private void DgvAcikTalepler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAcikTalepler.SelectedRows.Count > 0)
            {
                int secilenTalepId = Convert.ToInt32(dgvAcikTalepler.SelectedRows[0].Cells["TalepID"].Value);
                TeklifleriGetir(secilenTalepId);
            }
            else
            {
                dgvTeklifler.DataSource = null; // Seçim yoksa teklif tablosunu boşalt
            }
        }

        private void TeklifleriGetir(int talepId)
        {
            using (SqlConnection conn = Baglanti.GetConnection())
            {
                string query = "SELECT TedarikciAdi AS [Firma Adı], BirimFiyat AS Fiyat, TeklifTarihi AS [Tarih] FROM TedarikciTeklifleri WHERE TalepID = @tId";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@tId", talepId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTeklifler.DataSource = dt;
            }
        }

        // 3. YENİ TEKLİF KAYDET
        private void BtnTeklifKaydet_Click(object sender, EventArgs e)
        {
            if (dgvAcikTalepler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen önce sol taraftan teklif eklenecek talebi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTedarikci.Text) || string.IsNullOrWhiteSpace(txtFiyat.Text))
            {
                MessageBox.Show("Lütfen Tedarikçi Adı ve Fiyat alanlarını doldurun!");
                return;
            }

            int talepId = Convert.ToInt32(dgvAcikTalepler.SelectedRows[0].Cells["TalepID"].Value);

            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO TedarikciTeklifleri (TalepID, TedarikciAdi, BirimFiyat, Fiyat, TeslimSuresiGun) VALUES (@tId, @firma, @fiyat, @fiyat, 1)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    
                        // ... (parametreler)
                        {
                            cmd.Parameters.AddWithValue("@tId", talepId);
                        cmd.Parameters.AddWithValue("@firma", txtTedarikci.Text);
                        // Fiyatı virgüllü sayı olarak güvenle dönüştürüyoruz
                        cmd.Parameters.AddWithValue("@fiyat", Convert.ToDecimal(txtFiyat.Text.Replace('.', ',')));
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Teklif başarıyla sisteme eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Formu temizle ve teklif listesini güncelle
                txtTedarikci.Clear();
                txtFiyat.Clear();
                TeklifleriGetir(talepId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: Fiyat alanına sadece rakam giriniz. (Detay: " + ex.Message + ")");
            }
        }

        // 4. İŞ KURALI KONTROLÜ VE TALEBİ ONAYLAMA
        private void BtnTalebiOnayla_Click(object sender, EventArgs e)
        {
            if (dgvAcikTalepler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen onaylanacak talebi seçin!");
                return;
            }

            int talepId = Convert.ToInt32(dgvAcikTalepler.SelectedRows[0].Cells["TalepID"].Value);

            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();

                    // İŞ KURALI: Bu talep için kaç teklif girilmiş kontrol et!
                    string kuralQuery = "SELECT COUNT(*) FROM TedarikciTeklifleri WHERE TalepID = @tId";
                    using (SqlCommand cmdKural = new SqlCommand(kuralQuery, conn))
                    {
                        cmdKural.Parameters.AddWithValue("@tId", talepId);
                        int teklifSayisi = Convert.ToInt32(cmdKural.ExecuteScalar());

                        if (teklifSayisi < 2)
                        {
                            MessageBox.Show($"İşlem Durduruldu! Şirket kuralları gereği bir satın almanın onaylanabilmesi için en az 2 farklı tedarikçiden teklif girilmiş olması zorunludur. (Mevcut Teklif Sayısı: {teklifSayisi})", "Rekabet Kuralı İhlali", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return; // Kodu burada kes, onaylama!
                        }
                    }

                    // Kural geçildiyse Talebi Onaylandı statüsüne al
                    string updateQuery = "UPDATE SatinAlmaTalepleri SET Durum = 'Onaylandi' WHERE TalepID = @tId";
                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@tId", talepId);
                        cmdUpdate.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Harika! En az 2 teklif kuralı sağlandı ve Satın Alma Talebi başarıyla 'ONAYLANDI' olarak işaretlendi. Artık bu ürünleri Mal Kabul ekranından depoya alabilirsiniz.", "Onay Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tabloyu yenile (Onaylanan talep listeden düşecektir)
                AcikTalepleriListele();
                dgvTeklifler.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }
    }
}