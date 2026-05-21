using System;
using System.Windows.Forms;
using CokDepoluStokOtomasyonu.Models; // AktifKullanici sınıfına erişmek için

namespace CokDepoluStokOtomasyonu
{
    public partial class FrmAnaMenu : Form
    {
        public FrmAnaMenu()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(FrmAnaMenu_FormClosed);
        }

        private void FrmAnaMenu_Load(object sender, EventArgs e)
        {
            // 1. Sağ üst köşeye kimin giriş yaptığını yazdırıyoruz
            lblKullaniciBilgi.Text = $"Giriş Yapan: {AktifKullanici.Bilgi.KullaniciAdi} | Rol: {AktifKullanici.Bilgi.Rol}";

            // 2. Hocanın istediği Rol Ayrımı (Sadece Admin rapor görebilir)
            if (AktifKullanici.Bilgi.Rol != "Admin")
            {
                btnRaporlar.Visible = false;
            }

            // Giriş yapan kullanıcının rolü "Admin" değilse bu butonu gizle!
            if (AktifKullanici.Bilgi.Rol != "Admin")
            {
                btnKullaniciYonetimi.Visible = false;
            }
        }

        // Form kapatıldığında programın arka planda asılı kalmaması için
        private void FrmAnaMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnCikisYap_Click(object sender, EventArgs e)
        {
            // Kullanıcıya çıkmak isteyip istemediğini soruyoruz (Yanlışlıkla tıklamalara karşı güvenlik)
            DialogResult cevap = MessageBox.Show("Hesabınızdan çıkış yapmak istediğinize emin misiniz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                // Hafızadaki kullanıcı bilgisini temizle
                AktifKullanici.Bilgi = null;

                // Application.Restart() programı tamamen kapatıp sıfırdan (Giriş ekranından) tekrar başlatır.
                // Bu yöntem, arkada gizli kalan formların bellekte yer kaplamasını önleyen en temiz yoldur.
                Application.Restart();
            }
        }

        private void btnStokYonetimi_Click(object sender, EventArgs e)
        {
            FrmStokYonetimi frmStok = new FrmStokYonetimi();
            frmStok.ShowDialog(); // ShowDialog, bu form kapanmadan arkadaki forma tıklanmasını engeller
        }

        private void btnSatinAlma_Click(object sender, EventArgs e)
        {
            FrmSatinAlma frmSatinAlma = new FrmSatinAlma();
            frmSatinAlma.ShowDialog();
        }

        private void btnRaporlar_Click(object sender, EventArgs e)
        {
            FrmRaporlar frmRapor = new FrmRaporlar();
            frmRapor.ShowDialog();
        }

        private void btnKullaniciYonetimi_Click(object sender, EventArgs e)
        {
            FrmKullanicilar frm = new FrmKullanicilar();
            frm.ShowDialog();
        }
    }
}