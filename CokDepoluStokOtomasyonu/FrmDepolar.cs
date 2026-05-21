using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using CokDepoluStokOtomasyonu.DataAccess;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmDepolar : Form
    {
        public FrmDepolar()
        {
            InitializeComponent();
            this.Load += FrmDepolar_Load;
            btnDepoEkle.Click += BtnDepoEkle_Click;
            dgvDepolar.CellContentClick += DgvDepolar_CellContentClick;
        }

        private void FrmDepolar_Load(object sender, EventArgs e)
        {
            DepolariGetir();
        }

        private void DepolariGetir()
        {
            try
            {
                dgvDepolar.Columns.Clear();

                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();
                    // Tablondaki gerçek sütunları çekiyoruz (AktifMi olmadığı için herkesi getiriyoruz)
                    string query = "SELECT DepoID, DepoAdi AS [Depo Adı], Sehir AS [Şehir] FROM Depolar";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDepolar.DataSource = dt;
                }

                if (dgvDepolar.Columns["DepoID"] != null)
                    dgvDepolar.Columns["DepoID"].Visible = false;

                // Tabloda "AktifMi" sütunu olmadığı için işlemi mecburen "Kalıcı Silme" (Hard Delete) olarak yapıyoruz.
                DataGridViewButtonColumn btnSil = new DataGridViewButtonColumn();
                btnSil.Name = "btnSil";
                btnSil.HeaderText = "İşlem";
                btnSil.Text = "Kalıcı Olarak Sil";
                btnSil.UseColumnTextForButtonValue = true;
                dgvDepolar.Columns.Add(btnSil);

                dgvDepolar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }

        private void BtnDepoEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDepoAdi.Text))
            {
                MessageBox.Show("Depo Adı boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();
                    // Sadece DepoAdi ve Sehir ekliyoruz
                    string query = "INSERT INTO Depolar (DepoAdi, Sehir) VALUES (@ad, @sehir)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ad", txtDepoAdi.Text);
                        // Eğer şehir kutusunu boş bırakırsa null gitmemesi için kontrol ekliyoruz
                        cmd.Parameters.AddWithValue("@sehir", string.IsNullOrWhiteSpace(txtSehir.Text) ? "" : txtSehir.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                LogYonetimi.LogEkle("Ekleme", "Depolar", $"{txtDepoAdi.Text} adlı yeni depo sisteme eklendi.");
                MessageBox.Show("Depo başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtDepoAdi.Clear();
                txtSehir.Clear();
                DepolariGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDepolar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvDepolar.Columns[e.ColumnIndex].Name == "btnSil")
            {
                string seciliID = dgvDepolar.Rows[e.RowIndex].Cells["DepoID"].Value.ToString();
                string depoAdi = dgvDepolar.Rows[e.RowIndex].Cells["Depo Adı"].Value.ToString();

                DialogResult cevap = MessageBox.Show($"{depoAdi} adlı depoyu KALICI OLARAK silmek istediğinize emin misiniz?\n(Bu işlem geri alınamaz!)", "Kritik Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (cevap == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = Baglanti.GetConnection())
                        {
                            conn.Open();
                            // AktifMi olmadığı için satırı veritabanından tamamen yok ediyoruz
                            string query = "DELETE FROM Depolar WHERE DepoID = @id";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", seciliID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        LogYonetimi.LogEkle("Silme (Kalıcı)", "Depolar", $"{depoAdi} adlı depo tamamen silindi.");
                        DepolariGetir();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Silme işlemi sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}