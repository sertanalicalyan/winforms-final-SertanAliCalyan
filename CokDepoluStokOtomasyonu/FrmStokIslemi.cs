using CokDepoluStokOtomasyonu.DataAccess;
using CokDepoluStokOtomasyonu.Models;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmStokIslemi : Form
    {
        public FrmStokIslemi()
        {
            InitializeComponent();
        }

        private bool formYuklendi = false;

        private void FrmStokIslemi_Load(object sender, EventArgs e)
        {
            // 1. SİHİRLİ SATIR: Tasarım ekranı bozulsa bile, kutu değiştiğinde o metodu zorla çalıştırır!
            cmbIslemTipi.SelectedIndexChanged += cmbIslemTipi_SelectedIndexChanged;

            // 2. İşlem Tiplerini dolduruyoruz (Önce temizleyip ekliyoruz ki çiftleme olmasın)
            cmbIslemTipi.Items.Clear();
            cmbIslemTipi.Items.Add("Giriş");
            cmbIslemTipi.Items.Add("Çıkış");
            cmbIslemTipi.Items.Add("Transfer");

            // 3. Başlangıçta hedef depo araçlarını gizliyoruz
            cmbHedefDepo.Visible = false;
            lblHedefStok.Visible = false;
            lblHedefDepoBaslik.Visible = false;

            // 4. Veritabanından ürün ve depoları çek
            ComboboxlariDoldur();

            // 5. Yükleme bitti bayrağı
            formYuklendi = true;

            // 6. Kutudaki seçimi ŞİMDİ yapıyoruz ki, üstteki sihirli satır anında tetiklensin!
            cmbIslemTipi.SelectedIndex = 0;
            CanliStokGuncelle();
        }

        // --- CANLI STOK KONTROL METODLARI ---

        private int AnlikStokGetir(int depoId, int urunId)
        {
            int stok = 0;
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Miktar FROM Stoklar WHERE DepoID = @dId AND UrunID = @uId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@dId", depoId);
                        cmd.Parameters.AddWithValue("@uId", urunId);
                        object result = cmd.ExecuteScalar();
                        if (result != null) stok = Convert.ToInt32(result);
                    }
                }
            }
            catch { }
            return stok;
        }

        private void CanliStokGuncelle()
        {
            if (!formYuklendi || cmbUrun.SelectedValue == null || cmbKaynakDepo.SelectedValue == null) return;

            int urunId = Convert.ToInt32(cmbUrun.SelectedValue);
            int kaynakDepoId = Convert.ToInt32(cmbKaynakDepo.SelectedValue);

            lblKaynakStok.Text = $"Seçilen Depodaki Stok: {AnlikStokGetir(kaynakDepoId, urunId)} Adet";

            if (cmbIslemTipi.SelectedItem.ToString() == "Transfer" && cmbHedefDepo.SelectedValue != null)
            {
                int hedefDepoId = Convert.ToInt32(cmbHedefDepo.SelectedValue);
                lblHedefStok.Text = $"Hedef Depodaki Stok: {AnlikStokGetir(hedefDepoId, urunId)} Adet";
            }
        }

        // --- SEÇİMLER DEĞİŞTİKÇE GÖRÜNÜRLÜK AYARLARI ---

        private void cmbIslemTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIslemTipi.SelectedItem != null && cmbIslemTipi.SelectedItem.ToString() == "Transfer")
            {
                // Transfer seçildiyse hedef depoyla ilgili HER ŞEYİ göster
                cmbHedefDepo.Visible = true;
                lblHedefStok.Visible = true;
                lblHedefDepoBaslik.Visible = true;
            }
            else
            {
                // Giriş veya Çıkış seçildiyse hedef depoyla ilgili HER ŞEYİ gizle
                cmbHedefDepo.Visible = false;
                lblHedefStok.Visible = false;
                lblHedefDepoBaslik.Visible = false;
            }
            CanliStokGuncelle();
        }

        private void ComboboxlariDoldur()
        {
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();

                    // Ürünler
                    SqlDataAdapter daUrun = new SqlDataAdapter("SELECT UrunID, UrunAdi FROM Urunler", conn);
                    DataTable dtUrun = new DataTable();
                    daUrun.Fill(dtUrun);
                    cmbUrun.DataSource = dtUrun;
                    cmbUrun.DisplayMember = "UrunAdi";
                    cmbUrun.ValueMember = "UrunID";

                    // Kaynak Depo
                    SqlDataAdapter daDepo = new SqlDataAdapter("SELECT DepoID, DepoAdi FROM Depolar", conn);
                    DataTable dtDepo = new DataTable();
                    daDepo.Fill(dtDepo);
                    cmbKaynakDepo.DataSource = dtDepo;
                    cmbKaynakDepo.DisplayMember = "DepoAdi";
                    cmbKaynakDepo.ValueMember = "DepoID";

                    // Hedef Depo
                    DataTable dtHedefDepo = dtDepo.Copy();
                    cmbHedefDepo.DataSource = dtHedefDepo;
                    cmbHedefDepo.DisplayMember = "DepoAdi";
                    cmbHedefDepo.ValueMember = "DepoID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void cmbUrun_SelectedIndexChanged(object sender, EventArgs e)
        {
            CanliStokGuncelle();
        }

        private void cmbKaynakDepo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CanliStokGuncelle();
        }

        private void cmbHedefDepo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CanliStokGuncelle();
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            if (nudMiktar.Value <= 0)
            {
                MessageBox.Show("Miktar 0'dan büyük olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtGerekce.Text))
            {
                MessageBox.Show("Lütfen bir işlem gerekçesi giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string islemTipi = cmbIslemTipi.SelectedItem.ToString();
            int urunId = Convert.ToInt32(cmbUrun.SelectedValue);
            int kaynakDepoId = Convert.ToInt32(cmbKaynakDepo.SelectedValue);
            int miktar = Convert.ToInt32(nudMiktar.Value);
            string gerekce = txtGerekce.Text;
            int hedefDepoId = islemTipi == "Transfer" ? Convert.ToInt32(cmbHedefDepo.SelectedValue) : 0;

            if (islemTipi == "Transfer" && kaynakDepoId == hedefDepoId)
            {
                MessageBox.Show("Kaynak ve Hedef depo aynı olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = Baglanti.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                // Eşik kontrol değişkenleri
                bool esikUyarisiVer = false;
                int kalanStok = 0;
                int uyariEsigi = 0;

                try
                {
                    if (islemTipi == "Çıkış" || islemTipi == "Transfer")
                    {
                        string stokKontrolQuery = "SELECT Miktar FROM Stoklar WITH (UPDLOCK) WHERE DepoID = @depoId AND UrunID = @urunId";
                        using (SqlCommand cmdKontrol = new SqlCommand(stokKontrolQuery, conn, transaction))
                        {
                            cmdKontrol.Parameters.AddWithValue("@depoId", kaynakDepoId);
                            cmdKontrol.Parameters.AddWithValue("@urunId", urunId);

                            object sonuc = cmdKontrol.ExecuteScalar();
                            int mevcutStok = sonuc != null ? Convert.ToInt32(sonuc) : 0;

                            if (mevcutStok < miktar)
                            {
                                throw new Exception($"Yetersiz Stok! Bu depoda sadece {mevcutStok} adet var.");
                            }

                            string stokDusQuery = "UPDATE Stoklar SET Miktar = Miktar - @miktar WHERE DepoID = @depoId AND UrunID = @urunId";
                            using (SqlCommand cmdDus = new SqlCommand(stokDusQuery, conn, transaction))
                            {
                                cmdDus.Parameters.AddWithValue("@miktar", miktar);
                                cmdDus.Parameters.AddWithValue("@depoId", kaynakDepoId);
                                cmdDus.Parameters.AddWithValue("@urunId", urunId);
                                cmdDus.ExecuteNonQuery();
                            }
                        }
                    }

                    if (islemTipi == "Giriş" || islemTipi == "Transfer")
                    {
                        int eklenecekDepo = (islemTipi == "Giriş") ? kaynakDepoId : hedefDepoId;

                        string kayitVarMiQuery = "SELECT COUNT(*) FROM Stoklar WITH (UPDLOCK) WHERE DepoID = @depoId AND UrunID = @urunId";
                        using (SqlCommand cmdVarMi = new SqlCommand(kayitVarMiQuery, conn, transaction))
                        {
                            cmdVarMi.Parameters.AddWithValue("@depoId", eklenecekDepo);
                            cmdVarMi.Parameters.AddWithValue("@urunId", urunId);
                            int varMi = Convert.ToInt32(cmdVarMi.ExecuteScalar());

                            if (varMi > 0)
                            {
                                string stokEkleQuery = "UPDATE Stoklar SET Miktar = Miktar + @miktar WHERE DepoID = @depoId AND UrunID = @urunId";
                                using (SqlCommand cmdEkle = new SqlCommand(stokEkleQuery, conn, transaction))
                                {
                                    cmdEkle.Parameters.AddWithValue("@miktar", miktar);
                                    cmdEkle.Parameters.AddWithValue("@depoId", eklenecekDepo);
                                    cmdEkle.Parameters.AddWithValue("@urunId", urunId);
                                    cmdEkle.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string stokInsertQuery = "INSERT INTO Stoklar (DepoID, UrunID, Miktar) VALUES (@depoId, @urunId, @miktar)";
                                using (SqlCommand cmdInsert = new SqlCommand(stokInsertQuery, conn, transaction))
                                {
                                    cmdInsert.Parameters.AddWithValue("@depoId", eklenecekDepo);
                                    cmdInsert.Parameters.AddWithValue("@urunId", urunId);
                                    cmdInsert.Parameters.AddWithValue("@miktar", miktar);
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    // --- MİNMUM EŞİK HESAPLAMASI KONTROLÜ ---
                    if (islemTipi == "Çıkış" || islemTipi == "Transfer")
                    {
                        string esikQuery = "SELECT s.Miktar, u.MinEsik FROM Stoklar s INNER JOIN Urunler u ON s.UrunID = u.UrunID WHERE s.DepoID = @kaynakDepo AND s.UrunID = @urunId";
                        using (SqlCommand cmdEsik = new SqlCommand(esikQuery, conn, transaction))
                        {
                            cmdEsik.Parameters.AddWithValue("@kaynakDepo", kaynakDepoId);
                            cmdEsik.Parameters.AddWithValue("@urunId", urunId);
                            using (SqlDataReader dr = cmdEsik.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    kalanStok = Convert.ToInt32(dr["Miktar"]);
                                    uyariEsigi = Convert.ToInt32(dr["MinEsik"]);

                                    if (uyariEsigi > 0 && kalanStok < uyariEsigi)
                                    {
                                        esikUyarisiVer = true;
                                    }
                                }
                            }
                        }
                    }

                    string logQuery = @"INSERT INTO StokHareketleri (KullaniciID, UrunID, HareketTipi, KaynakDepoID, HedefDepoID, Miktar, Gerekce) VALUES (@kullanici, @urun, @tip, @kaynak, @hedef, @miktar, @gerekce)";

                    using (SqlCommand cmdLog = new SqlCommand(logQuery, conn, transaction))
                    {
                        cmdLog.Parameters.AddWithValue("@kullanici", AktifKullanici.Bilgi.KullaniciID);
                        cmdLog.Parameters.AddWithValue("@urun", urunId);

                        string ingilizceIslemTipi = islemTipi == "Giriş" ? "Giris" : (islemTipi == "Çıkış" ? "Cikis" : "Transfer");
                        cmdLog.Parameters.AddWithValue("@tip", ingilizceIslemTipi);

                        // HATAYI ÖNLEYEN KISIM: Her iki tarafı da (object) türüne cast ettik
                        cmdLog.Parameters.AddWithValue("@kaynak", islemTipi == "Giriş" ? (object)DBNull.Value : (object)kaynakDepoId);
                        cmdLog.Parameters.AddWithValue("@hedef", islemTipi == "Transfer" ? (object)hedefDepoId : (object)DBNull.Value);

                        cmdLog.Parameters.AddWithValue("@miktar", miktar);
                        cmdLog.Parameters.AddWithValue("@gerekce", gerekce);

                        cmdLog.ExecuteNonQuery();
                    }

                    // Transaction'ı tek seferde bitirip işlemi güvene alıyoruz
                    transaction.Commit();

                    MessageBox.Show("İşlem başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // EŞİK UYARISI KODU (Form kapanmadan hemen önce gösteriyoruz)
                    if (esikUyarisiVer)
                    {
                        MessageBox.Show($"Kritik Stok Uyarısı: Bu işlem sonrası üründen depoda {kalanStok} adet kaldı. (Belirlenen Minimum Eşik: {uyariEsigi})", "Stok Uyarı Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message, "İşlem Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }// USING BLOĞUNUN KAPANIŞ PARANTEZİ (Hataların asıl sebebi genelde bunun unutulmasıdır)

        private void cmbIslemTipi_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}