using CokDepoluStokOtomasyonu.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmKullaniciEkle : Form
    {
        public FrmKullaniciEkle()
        {
            InitializeComponent();

            // 1. ROLLERİ EKLİYORUZ (ComboBox boş kalmasın)
            cmbRol.Items.Add("Admin");
            cmbRol.Items.Add("Personel");
            cmbRol.SelectedIndex = 0; // Form açıldığında varsayılan olarak "Admin" seçili gelsin

            // 2. ŞİFREYİ GİZLİYORUZ
            txtSifre.PasswordChar = '*';

            // 3. GÖZ BUTONUNUN BASILI TUTMA OLAYLARINI (EVENT) BAĞLIYORUZ
            btnGoster.MouseDown += BtnGoster_MouseDown;
            btnGoster.MouseUp += BtnGoster_MouseUp;
        }

        // Fareyle butona BASILI TUTULDUĞUNDA çalışacak kod
        private void BtnGoster_MouseDown(object sender, MouseEventArgs e)
        {
            // '\0' (Sıfır) karakteri, maskelemeyi iptal et ve gerçek yazıyı göster demektir
            txtSifre.PasswordChar = '\0';
        }

        // Fare butondan BIRAKILDIĞINDA çalışacak kod
        private void BtnGoster_MouseUp(object sender, MouseEventArgs e)
        {
            // Maskeyi tekrar yıldıza çevir
            txtSifre.PasswordChar = '*';
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // 1. Boş Alan Kontrolü
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) ||
                string.IsNullOrWhiteSpace(txtSifre.Text) ||
                cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Lütfen Kullanıcı Adı, Şifre ve Rol alanlarını eksiksiz doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Eksik varsa işlemi burada kes
            }

            try
            {
                using (SqlConnection conn = Baglanti.GetConnection())
                {
                    conn.Open();

                    // 2. Bu kullanıcı adı daha önce alınmış mı kontrolü (Aynı isimde 2 kişi olmasın)
                    string checkQuery = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @kAd";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@kAd", txtKullaniciAdi.Text);
                        int varMi = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (varMi > 0)
                        {
                            MessageBox.Show("Bu kullanıcı adı zaten sistemde kayıtlı! Lütfen farklı bir ad seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // İsim kullanılıyorsa işlemi durdur
                        }
                    }

                    // 3. Veritabanına Kaydetme İşlemi
                    string insertQuery = "INSERT INTO Kullanicilar (KullaniciAdi, Sifre, Rol) VALUES (@kAd, @sifre, @rol)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@kAd", txtKullaniciAdi.Text);
                        cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);
                        cmd.Parameters.AddWithValue("@rol", cmbRol.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Yeni kullanıcı sisteme başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // İşlem bitince bu ekleme penceresini otomatik kapat
                LogYonetimi.LogEkle("Ekleme", "Kullanicilar", $"{txtKullaniciAdi.Text} adlı yeni personel sisteme eklendi.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
