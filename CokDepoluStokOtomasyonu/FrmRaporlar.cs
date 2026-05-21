using System;
using System.Data;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using CokDepoluStokOtomasyonu.DataAccess;

// PDF Kütüphaneleri
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmRaporlar : Form
    {
        public FrmRaporlar()
        {
            InitializeComponent();
        }

        private void FrmRaporlar_Load(object sender, EventArgs e)
        {
            HareketleriListele();
        }

        private void HareketleriListele()
        {
            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT 
                                        SH.HareketID,
                                        K.Rol AS [İşlemi Yapan Rol],
                                        U.UrunAdi AS [Ürün Adı],
                                        SH.HareketTipi AS [Hareket Tipi],
                                        D1.DepoAdi AS [Kaynak Depo],
                                        D2.DepoAdi AS [Hedef Depo],
                                        SH.Miktar,
                                        SH.Gerekce AS [Gerekçe],
                                        SH.Tarih AS [İşlem Tarihi]
                                     FROM StokHareketleri SH
                                     LEFT JOIN Kullanicilar K ON SH.KullaniciID = K.KullaniciID
                                     LEFT JOIN Urunler U ON SH.UrunID = U.UrunID
                                     LEFT JOIN Depolar D1 ON SH.KaynakDepoID = D1.DepoID
                                     LEFT JOIN Depolar D2 ON SH.HedefDepoID = D2.DepoID
                                     ORDER BY SH.Tarih DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dgvRapor.DataSource = dt;

                        // ID Sütununu kullanıcının görmemesi için gizliyoruz.
                        if (dgvRapor.Columns["HareketID"] != null)
                            dgvRapor.Columns["HareketID"].Visible = false;

                        // Tablo sütunlarını sağdaki boşluğa kadar tam sığdırır
                        dgvRapor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        dgvRapor.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Tabloda hiç veri yok. Lütfen bir stok işlemi yapın.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: " + ex.Message);
            }
        }

        private void btnPdfAl_Click(object sender, EventArgs e)
        {
            if (dgvRapor.Rows.Count == 0)
            {
                MessageBox.Show("Dışa aktarılacak veri bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF Dosyası|*.pdf";
            save.FileName = "Stok_Hareket_Raporu_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
            save.Title = "Raporu Kaydet";

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // ÇÖZÜM 1: TÜRKÇE KARAKTER SORUNUNU GİDERMEK
                    // Windows'un standart Arial fontunu sisteme tanıtıyoruz. IDENTITY_H tüm Türkçe karakterleri (ş,ğ,ç,ö,ü,ı) tanır.
                    string fontYolu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont bf = BaseFont.CreateFont(fontYolu, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                    iTextSharp.text.Font textFont = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.NORMAL);
                    iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                    iTextSharp.text.Font baslikFont = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);

                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 10f);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(save.FileName, FileMode.Create));
                    pdfDoc.Open();

                    Paragraph baslik = new Paragraph("ÇOK DEPOLU STOK YÖNETİMİ - HAREKET RAPORU\n\n", baslikFont);
                    baslik.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(baslik);

                    // ÇÖZÜM 2: KAYMA (BİRBİRİNE GİRME) SORUNUNU GİDERMEK
                    // Tablodaki sadece görünür olan sütun sayısını hesaplayıp PDF tablosunu ona göre çiziyoruz
                    int gorunurSutunSayisi = 0;
                    foreach (DataGridViewColumn col in dgvRapor.Columns)
                    {
                        if (col.Visible) gorunurSutunSayisi++;
                    }

                    PdfPTable pdfTable = new PdfPTable(gorunurSutunSayisi);
                    pdfTable.WidthPercentage = 100;

                    // Başlıkları Ekleme
                    foreach (DataGridViewColumn column in dgvRapor.Columns)
                    {
                        if (column.Visible)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, headerFont));
                            cell.BackgroundColor = new BaseColor(51, 102, 153);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Padding = 5;
                            pdfTable.AddCell(cell);
                        }
                    }

                    // Verileri (Satırları) Ekleme
                    foreach (DataGridViewRow row in dgvRapor.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (dgvRapor.Columns[cell.ColumnIndex].Visible)
                                {
                                    string hucreVerisi = cell.Value != null ? cell.Value.ToString() : "";
                                    PdfPCell pdfCell = new PdfPCell(new Phrase(hucreVerisi, textFont));
                                    pdfCell.Padding = 5;
                                    pdfTable.AddCell(pdfCell);
                                }
                            }
                        }
                    }

                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();

                    MessageBox.Show("PDF Raporu başarıyla oluşturuldu!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("HATA DETAYI: " + ex.Message, "PDF Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FrmRaporlar_Load_1(object sender, EventArgs e)
        {
            HareketleriListele();
        }
    }
}