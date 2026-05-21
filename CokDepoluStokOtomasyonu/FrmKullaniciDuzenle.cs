using CokDepoluStokOtomasyonu.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmKullaniciDuzenle : Form
    {
        // Ana ekrandan buraya veri taşımak için açtığımız gizli cepler:
        public string SeciliID { get; set; }
        public string SeciliAd { get; set; }
        public string SeciliSifre { get; set; }
        public string SeciliRol { get; set; }

        public FrmKullaniciDuzenle()
        {
            InitializeComponent();
            this.Load += FrmKullaniciDuzenle_Load;
            btnGuncelle.Click += BtnGuncelle_Click;

            // Göz butonu kodları aynen çalışsın:
            txtSifre.PasswordChar = '*';
            btnGoster.MouseDown += (s, e) => { txtSifre.PasswordChar = '\0'; };
            btnGoster.MouseUp += (s, e) => { txtSifre.PasswordChar = '*'; };
        }

        private void FrmKullaniciDuzenle_Load(object sender, EventArgs e)
        {
            // Form açılırken rolleri ekle ve ana ekrandan gelen verileri kutulara yerleştir
            cmbRol.Items.Add("Admin");
            cmbRol.Items.Add("Personel");

            txtKullaniciAdi.Text = SeciliAd;
            txtSifre.Text = SeciliSifre;
            cmbRol.Text = SeciliRol;
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
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
                    // SQL'deki mevcut kaydın üzerine yeni verileri yazıyoruz
                    string updateQuery = "UPDATE Kullanicilar SET KullaniciAdi = @kAd, Sifre = @sifre, Rol = @rol WHERE KullaniciID = @id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@kAd", txtKullaniciAdi.Text);
                        cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);
                        cmd.Parameters.AddWithValue("@rol", cmbRol.Text);
                        cmd.Parameters.AddWithValue("@id", SeciliID); // Hangi satır güncellenecek?

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Kullanıcı bilgileri başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Güncelleme bitince ekranı kapat
                LogYonetimi.LogEkle("Güncelleme", "Kullanicilar", $"{txtKullaniciAdi.Text} adlı kullanıcının bilgileri / şifresi güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}