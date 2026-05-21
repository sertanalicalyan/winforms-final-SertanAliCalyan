using CokDepoluStokOtomasyonu.DataAccess;
using CokDepoluStokOtomasyonu.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmMalKabul : Form
    {
        public FrmMalKabul()
        {
            InitializeComponent();

            this.Load += FrmMalKabul_Load;
            btnKabulEt.Click += BtnKabulEt_Click;
        }

        private void FrmMalKabul_Load(object sender, EventArgs e)
        {
            OnayliTalepleriGetir();
            DepolariGetir();
        }

        private void OnayliTalepleriGetir()
        {
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    // Sadece onaylanmış, bekleyen siparişleri çekiyoruz
                    string query = @"
                        SELECT 
                            t.TalepID, 
                            u.UrunID,
                            u.UrunAdi AS [Ürün Adı], 
                            t.Miktar AS [Beklenen Miktar], 
                            t.TalepTarihi AS [Sipariş Tarihi]
                        FROM SatinAlmaTalepleri t
                        INNER JOIN Urunler u ON t.UrunID = u.UrunID
                        WHERE t.Durum = 'Onaylandi'";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvOnayliTalepler.DataSource = dt;

                    // ID'leri arka planda tut ama kullanıcıdan gizle
                    if (dgvOnayliTalepler.Columns.Contains("TalepID")) dgvOnayliTalepler.Columns["TalepID"].Visible = false;
                    if (dgvOnayliTalepler.Columns.Contains("UrunID")) dgvOnayliTalepler.Columns["UrunID"].Visible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("Siparişler yüklenemedi: " + ex.Message); }
        }

        private void DepolariGetir()
        {
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT DepoID, DepoAdi FROM Depolar", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbDepo.DataSource = dt;
                    cmbDepo.DisplayMember = "DepoAdi";
                    cmbDepo.ValueMember = "DepoID";
                }
            }
            catch { }
        }

        private void BtnKabulEt_Click(object sender, EventArgs e)
        {
            if (dgvOnayliTalepler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen mal kabulü yapılacak onaylı siparişi listeden seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtGelenMiktar.Text) || cmbDepo.SelectedValue == null)
            {
                MessageBox.Show("Lütfen depoya giren miktarı yazın ve hedef depoyu seçin!");
                return;
            }

            int talepId = Convert.ToInt32(dgvOnayliTalepler.SelectedRows[0].Cells["TalepID"].Value);
            int urunId = Convert.ToInt32(dgvOnayliTalepler.SelectedRows[0].Cells["UrunID"].Value);
            int beklenenMiktar = Convert.ToInt32(dgvOnayliTalepler.SelectedRows[0].Cells["Beklenen Miktar"].Value);
            int gelenMiktar = Convert.ToInt32(txtGelenMiktar.Text);
            int secilenDepo = Convert.ToInt32(cmbDepo.SelectedValue);

            if (gelenMiktar <= 0 || gelenMiktar > beklenenMiktar)
            {
                MessageBox.Show($"Hatalı Giriş: Gelen miktar 0 olamaz ve beklenen sipariş miktarından ({beklenenMiktar}) fazla olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();

                    try
                    {
                        // --- 1. STOKLARA EKLEME (Race Condition Korumalı) ---
                        string checkStok = "SELECT COUNT(*) FROM Stoklar WITH (UPDLOCK) WHERE DepoID = @dId AND UrunID = @uId";
                        using (SqlCommand cmdCheck = new SqlCommand(checkStok, conn, trans))
                        {
                            cmdCheck.Parameters.AddWithValue("@dId", secilenDepo);
                            cmdCheck.Parameters.AddWithValue("@uId", urunId);
                            int varMi = Convert.ToInt32(cmdCheck.ExecuteScalar());

                            if (varMi > 0)
                            {
                                string updStok = "UPDATE Stoklar SET Miktar = Miktar + @miktar WHERE DepoID = @dId AND UrunID = @uId";
                                using (SqlCommand cmdUpd = new SqlCommand(updStok, conn, trans))
                                {
                                    cmdUpd.Parameters.AddWithValue("@miktar", gelenMiktar);
                                    cmdUpd.Parameters.AddWithValue("@dId", secilenDepo);
                                    cmdUpd.Parameters.AddWithValue("@uId", urunId);
                                    cmdUpd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insStok = "INSERT INTO Stoklar (DepoID, UrunID, Miktar) VALUES (@dId, @uId, @miktar)";
                                using (SqlCommand cmdIns = new SqlCommand(insStok, conn, trans))
                                {
                                    cmdIns.Parameters.AddWithValue("@miktar", gelenMiktar);
                                    cmdIns.Parameters.AddWithValue("@dId", secilenDepo);
                                    cmdIns.Parameters.AddWithValue("@uId", urunId);
                                    cmdIns.ExecuteNonQuery();
                                }
                            }
                        }

                        // --- 2. HAREKET GEÇMİŞİNE (LOG) YAZMA ---
                        string logQuery = "INSERT INTO StokHareketleri (KullaniciID, UrunID, HareketTipi, HedefDepoID, Miktar, Gerekce) VALUES (@kull, @uId, 'Giris', @hedef, @miktar, 'Satın Alma - Mal Kabul')";
                        using (SqlCommand cmdLog = new SqlCommand(logQuery, conn, trans))
                        {
                            cmdLog.Parameters.AddWithValue("@kull", AktifKullanici.Bilgi.KullaniciID);
                            cmdLog.Parameters.AddWithValue("@uId", urunId);
                            cmdLog.Parameters.AddWithValue("@hedef", secilenDepo);
                            cmdLog.Parameters.AddWithValue("@miktar", gelenMiktar);
                            cmdLog.ExecuteNonQuery();
                        }

                        // --- 3. İŞ KURALI: KISMİ KABUL MANTIĞI ---
                        if (gelenMiktar == beklenenMiktar)
                        {
                            // Ürünlerin tamamı geldiyse talebi tamamen KAPAT
                            string updTalep = "UPDATE SatinAlmaTalepleri SET Durum = 'Kapandi' WHERE TalepID = @tId";
                            using (SqlCommand cmdTalep = new SqlCommand(updTalep, conn, trans))
                            {
                                cmdTalep.Parameters.AddWithValue("@tId", talepId);
                                cmdTalep.ExecuteNonQuery();
                            }
                            MessageBox.Show("Ürünlerin tamamı teslim alındı. Sipariş kapatıldı ve stoklara eklendi!", "Tam Kabul", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // KISMI KABUL: Gelen miktar kadarını kapat, eksik miktar için YENİ talep aç!
                            string updTalep = "UPDATE SatinAlmaTalepleri SET Durum = 'Kapandi', Miktar = @gelen WHERE TalepID = @tId";
                            using (SqlCommand cmdTalep = new SqlCommand(updTalep, conn, trans))
                            {
                                cmdTalep.Parameters.AddWithValue("@gelen", gelenMiktar);
                                cmdTalep.Parameters.AddWithValue("@tId", talepId);
                                cmdTalep.ExecuteNonQuery();
                            }

                            int kalanMiktar = beklenenMiktar - gelenMiktar;
                            string insYeniTalep = "INSERT INTO SatinAlmaTalepleri (UrunID, Miktar, Durum) VALUES (@uId, @kalan, 'Acik')";
                            using (SqlCommand cmdYeni = new SqlCommand(insYeniTalep, conn, trans))
                            {
                                cmdYeni.Parameters.AddWithValue("@uId", urunId);
                                cmdYeni.Parameters.AddWithValue("@kalan", kalanMiktar);
                                cmdYeni.ExecuteNonQuery();
                            }
                            MessageBox.Show($"Kısmi kabul yapıldı! Gelen {gelenMiktar} adet stoka eklendi. Eksik kalan {kalanMiktar} adet için sistem otomatik olarak yeni bir açık talep oluşturdu ve süreci başa aldı.", "Kısmi Kabul Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        trans.Commit();
                        OnayliTalepleriGetir(); // Tabloyu yenile, kabul edilen sipariş ekrandan düşer.
                        txtGelenMiktar.Clear();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Kayıt sırasında hata oluştu: " + ex.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message);
            }
        }
    }
}