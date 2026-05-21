using CokDepoluStokOtomasyonu.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmIslemLoglari : Form
    {
        public FrmIslemLoglari()
        {
            InitializeComponent();
            this.Load += FrmIslemLoglari_Load;
        }

        private void FrmIslemLoglari_Load(object sender, EventArgs e)
        {
            LoglariListele();
        }

        private void LoglariListele()
        {
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();

                    // INNER JOIN kullanarak sadece KullaniciID değil, işlemi yapan kişinin gerçek adını da ekrana yazdırıyoruz.
                    // En yeni yapılan işlem en üstte görünsün diye "ORDER BY IslemTarihi DESC" ekledik.
                    string query = @"SELECT 
                                        L.LogID, 
                                        K.KullaniciAdi AS [İşlemi Yapan], 
                                        L.IslemTipi AS [İşlem Tipi], 
                                        L.TabloAdi AS [Modül / Tablo], 
                                        L.Aciklama AS [Açıklama], 
                                        L.IslemTarihi AS [İşlem Tarihi] 
                                     FROM IslemLoglari L
                                     INNER JOIN Kullanicilar K ON L.KullaniciID = K.KullaniciID
                                     ORDER BY L.IslemTarihi DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvLoglar.DataSource = dt;
                }

                // LogID sütununu kullanıcının görmesine gerek yok, gizliyoruz.
                if (dgvLoglar.Columns["LogID"] != null)
                    dgvLoglar.Columns["LogID"].Visible = false;

                // Tablonun sütun genişliklerini içeriğe göre otomatik ayarla (Şık görünmesi için)
                dgvLoglar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loglar listelenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}