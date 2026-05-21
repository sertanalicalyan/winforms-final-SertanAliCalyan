using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using CokDepoluStokOtomasyonu.DataAccess;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmStokYonetimi : Form
    {
        public FrmStokYonetimi()
        {
            InitializeComponent();
        }

        private void FrmStokYonetimi_Load(object sender, EventArgs e)
        { // <-- İŞTE BURADAKİ SÜSLÜ PARANTEZİ UNUTMUŞTUM :)

            // Form açıldığı anda çalışacak kod
            StoklariListele();
        }

        // Kodlar kalabalık olmasın diye veri çekme işlemini ayrı bir metoda yazıyoruz
        private void StoklariListele()
        {
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();
                    // Ürünleri ve bulundukları depoları isimleriyle getiren SQL sorgusu
                    string query = @"
                        SELECT 
                            d.DepoAdi AS 'Depo',
                            u.UrunAdi AS 'Ürün',
                            s.Miktar AS 'Mevcut Stok',
                            u.Birim AS 'Birim',
                            u.KritikEsik AS 'Kritik Eşik'
                        FROM Stoklar s
                        INNER JOIN Depolar d ON s.DepoID = d.DepoID
                        INNER JOIN Urunler u ON s.UrunID = u.UrunID
                        ORDER BY d.DepoAdi, u.UrunAdi";

                    // SqlDataAdapter verileri çeker ve DataTable'a (sanal tabloya) doldurur
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Sanal tablomuzu ekrandaki DataGridView'a bağlıyoruz
                    dgvStoklar.DataSource = dt;
                    dgvStoklar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sütunları ekrana tam sığdırır
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Stok verileri çekilirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnYeniIslem_Click(object sender, EventArgs e)
        {
            FrmStokIslemi islemFormu = new FrmStokIslemi();
            islemFormu.ShowDialog();

            // İşlem bitip form kapandığında listeyi otomatik güncelle
            StoklariListele();
        }

        private void btnTeklifYonetimi_Click(object sender, EventArgs e)
        {
            FrmTeklifYonetimi frmTeklif = new FrmTeklifYonetimi();
            frmTeklif.ShowDialog();
        }

        private void btnMalKabul_Click(object sender, EventArgs e)
        {
            // Depo Yönetimi formunu çağırıyoruz
            FrmDepolar depoFormu = new FrmDepolar();

            // ShowDialog kullanmak burada daha mantıklıdır, 
            // kullanıcı depo ekleme işini bitirip pencereyi kapatana kadar arka ekrana tıklayamaz.
            depoFormu.ShowDialog();
        }
    }
}