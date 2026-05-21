using CokDepoluStokOtomasyonu.DataAccess;
using CokDepoluStokOtomasyonu.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmSatinAlma : Form
    {
        public FrmSatinAlma()
        {
            InitializeComponent();

            // Eğer butona çift tıklayıp oluşturmadıysan, tıklanma olayını buradan bağlıyoruz
            btnTalepOlustur.Click += btnTalepOlustur_Click;
        }

        private void FrmSatinAlma_Load(object sender, EventArgs e)
        {
            EksikStoklariListele(); // Senin mevcut metodun
            UrunleriGetir();        // YENİ: ComboBox'ı dolduran metodumuz
        }

        // ======================================================================
        // SENİN MEVCUT, HİÇ EKSİLTİLMEMİŞ KODLARIN (Eksik Stok & Hızlı Sipariş)
        // ======================================================================

        public void EksikStoklariListele()
        {
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            u.UrunID AS 'Ürün Kodu',
                            u.UrunAdi AS 'Ürün Adı', 
                            ISNULL(SUM(s.Miktar), 0) AS 'Mevcut Toplam Stok', 
                            u.KritikEsik AS 'Kritik Eşik',
                            (u.KritikEsik - ISNULL(SUM(s.Miktar), 0) + 10) AS 'Önerilen Sipariş Miktarı'
                        FROM Urunler u
                        LEFT JOIN Stoklar s ON u.UrunID = s.UrunID
                        GROUP BY u.UrunID, u.UrunAdi, u.KritikEsik
                        HAVING ISNULL(SUM(s.Miktar), 0) <= u.KritikEsik";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvEksikStoklar.DataSource = dt;
                    dgvEksikStoklar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eksik stoklar çekilirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSiparisVer_Click(object sender, EventArgs e)
        {
            if (dgvEksikStoklar.SelectedRows.Count > 0)
            {
                int urunId = Convert.ToInt32(dgvEksikStoklar.SelectedRows[0].Cells["Ürün Kodu"].Value);
                string urunAdi = dgvEksikStoklar.SelectedRows[0].Cells["Ürün Adı"].Value.ToString();
                int onerilenMiktar = Convert.ToInt32(dgvEksikStoklar.SelectedRows[0].Cells["Önerilen Sipariş Miktarı"].Value);

                DataTable dtDepolar = new DataTable();
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT DepoID, DepoAdi FROM Depolar", conn);
                    da.Fill(dtDepolar);
                }

                Form prompt = new Form()
                {
                    Width = 420,
                    Height = 280,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "Sipariş ve Depo Belirleme",
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false
                };

                Label textLabel = new Label() { Left = 20, Top = 20, Width = 360, Height = 40, Text = $"{urunAdi} için sistemin önerisi: {onerilenMiktar} Adet.\nLütfen miktar ve eklenecek depoyu seçiniz:" };
                Label lblMiktar = new Label() { Left = 20, Top = 70, Width = 100, Text = "Sipariş Miktarı:" };
                NumericUpDown inputMiktar = new NumericUpDown() { Left = 130, Top = 68, Width = 230, Maximum = 99999, Minimum = 1, Value = onerilenMiktar };
                Label lblDepo = new Label() { Left = 20, Top = 110, Width = 100, Text = "Hedef Depo:" };
                ComboBox cmbDepo = new ComboBox() { Left = 130, Top = 108, Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };
                cmbDepo.DataSource = dtDepolar;
                cmbDepo.DisplayMember = "DepoAdi";
                cmbDepo.ValueMember = "DepoID";

                Label lblAnlikStok = new Label() { Left = 130, Top = 140, Width = 230, Text = "Mevcut Stok: Hesaplanıyor...", ForeColor = System.Drawing.Color.Blue };
                Button btnOnay = new Button() { Text = "Siparişi Onayla", Left = 130, Top = 180, Width = 150, DialogResult = DialogResult.OK };

                cmbDepo.SelectedIndexChanged += (s, ev) =>
                {
                    if (cmbDepo.SelectedValue != null && int.TryParse(cmbDepo.SelectedValue.ToString(), out int secilenId))
                    {
                        int mevcutStok = 0;
                        try
                        {
                            using (SqlConnection c = Baglanti.GetConnection())
                            {
                                c.Open();
                                string q = "SELECT Miktar FROM Stoklar WHERE DepoID = @dId AND UrunID = @uId";
                                using (SqlCommand cmd = new SqlCommand(q, c))
                                {
                                    cmd.Parameters.AddWithValue("@dId", secilenId);
                                    cmd.Parameters.AddWithValue("@uId", urunId);
                                    object res = cmd.ExecuteScalar();
                                    if (res != null) mevcutStok = Convert.ToInt32(res);
                                }
                            }
                        }
                        catch { }
                        lblAnlikStok.Text = $"Bu depodaki mevcut stok: {mevcutStok} Adet";
                    }
                };

                if (cmbDepo.Items.Count > 0) cmbDepo.SelectedIndex = 0;

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(lblMiktar);
                prompt.Controls.Add(inputMiktar);
                prompt.Controls.Add(lblDepo);
                prompt.Controls.Add(cmbDepo);
                prompt.Controls.Add(lblAnlikStok);
                prompt.Controls.Add(btnOnay);
                prompt.AcceptButton = btnOnay;

                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    int kesinSiparisMiktari = (int)inputMiktar.Value;
                    int secilenDepoId = (int)cmbDepo.SelectedValue;

                    using (SqlConnection conn = Baglanti.GetConnection())
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            string checkQuery = "SELECT COUNT(*) FROM Stoklar WITH (UPDLOCK) WHERE DepoID = @depoId AND UrunID = @uId";
                            using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn, transaction))
                            {
                                cmdCheck.Parameters.AddWithValue("@depoId", secilenDepoId);
                                cmdCheck.Parameters.AddWithValue("@uId", urunId);
                                int varMi = Convert.ToInt32(cmdCheck.ExecuteScalar());

                                if (varMi > 0)
                                {
                                    string updateQuery = "UPDATE Stoklar SET Miktar = Miktar + @miktar WHERE DepoID = @depoId AND UrunID = @uId";
                                    using (SqlCommand cmdUpd = new SqlCommand(updateQuery, conn, transaction))
                                    {
                                        cmdUpd.Parameters.AddWithValue("@miktar", kesinSiparisMiktari);
                                        cmdUpd.Parameters.AddWithValue("@depoId", secilenDepoId);
                                        cmdUpd.Parameters.AddWithValue("@uId", urunId);
                                        cmdUpd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    string insertQuery = "INSERT INTO Stoklar (DepoID, UrunID, Miktar) VALUES (@depoId, @uId, @miktar)";
                                    using (SqlCommand cmdIns = new SqlCommand(insertQuery, conn, transaction))
                                    {
                                        cmdIns.Parameters.AddWithValue("@miktar", kesinSiparisMiktari);
                                        cmdIns.Parameters.AddWithValue("@depoId", secilenDepoId);
                                        cmdIns.Parameters.AddWithValue("@uId", urunId);
                                        cmdIns.ExecuteNonQuery();
                                    }
                                }
                            }

                            string logQuery = @"INSERT INTO StokHareketleri 
                                                (KullaniciID, UrunID, HareketTipi, KaynakDepoID, Miktar, Gerekce) 
                                                VALUES (@kull, @uId, 'Giris', @depoId, @miktar, 'Manuel Onaylı Satın Alma (Kritik Eşik)')";

                            using (SqlCommand cmdLog = new SqlCommand(logQuery, conn, transaction))
                            {
                                cmdLog.Parameters.AddWithValue("@kull", AktifKullanici.Bilgi.KullaniciID);
                                cmdLog.Parameters.AddWithValue("@uId", urunId);
                                cmdLog.Parameters.AddWithValue("@depoId", secilenDepoId);
                                cmdLog.Parameters.AddWithValue("@miktar", kesinSiparisMiktari);
                                cmdLog.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show($"Başarılı! Seçtiğiniz depoya {kesinSiparisMiktari} adet {urunAdi} eklendi.", "Sipariş Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            EksikStoklariListele();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Sipariş sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen sipariş vermek için listeden bir ürün seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ======================================================================
        // SİSTEME YENİ EKLENEN MANUEL TALEP OLUŞTURMA İŞ KURALLARI (MODÜL 2)
        // ======================================================================

        private void UrunleriGetir()
        {
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT UrunID, UrunAdi FROM Urunler", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (cmbUrunler != null)
                    {
                        cmbUrunler.DisplayMember = "UrunAdi";
                        cmbUrunler.ValueMember = "UrunID";
                        cmbUrunler.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürünler yüklenirken hata: " + ex.Message);
            }
        }

        private void btnTalepOlustur_Click(object sender, EventArgs e)
        {
            if (cmbUrunler.SelectedValue == null || string.IsNullOrWhiteSpace(txtMiktar.Text))
            {
                MessageBox.Show("Lütfen ürün seçin ve miktar girin!");
                return;
            }

            int urunId = Convert.ToInt32(cmbUrunler.SelectedValue);
            int miktar = Convert.ToInt32(txtMiktar.Text);

            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();

                    // İŞ KURALI: Bu ürün için 'Acik' statüsünde bir talep var mı?
                    string kontrolQuery = "SELECT COUNT(*) FROM SatinAlmaTalepleri WHERE UrunID = @urunId AND Durum = 'Bekliyor'";
                    using (SqlCommand cmdKontrol = new SqlCommand(kontrolQuery, conn))
                    {
                        cmdKontrol.Parameters.AddWithValue("@urunId", urunId);
                        int acikTalepSayisi = Convert.ToInt32(cmdKontrol.ExecuteScalar());

                        if (acikTalepSayisi > 0)
                        {
                            // VARSA: Yenisini açma, eskisinin miktarını güncelle!
                            string updateQuery = "UPDATE SatinAlmaTalepleri SET Miktar = Miktar + @ekMiktar WHERE UrunID = @urunId AND Durum = 'Bekliyor'";
                            using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@ekMiktar", miktar);
                                cmdUpdate.Parameters.AddWithValue("@urunId", urunId);
                                cmdUpdate.ExecuteNonQuery();
                            }
                            MessageBox.Show($"Bu ürün için zaten açık bir talep vardı. Yeni miktar ({miktar} adet) mevcut talebe eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // YOKSA: Sıfırdan yeni talep oluştur
                            string insertQuery = "INSERT INTO SatinAlmaTalepleri (UrunID, Miktar, Durum) VALUES (@urunId, @miktar, 'Bekliyor')";
                            using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                            {
                                cmdInsert.Parameters.AddWithValue("@urunId", urunId);
                                cmdInsert.Parameters.AddWithValue("@miktar", miktar);
                                cmdInsert.ExecuteNonQuery();
                            }
                            MessageBox.Show("Yeni satın alma talebi başarıyla oluşturuldu!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }
    }
}