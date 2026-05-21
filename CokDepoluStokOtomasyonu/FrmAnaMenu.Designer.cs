namespace CokDepoluStokOtomasyonu
{
    partial class FrmAnaMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblKullaniciBilgi = new Label();
            btnStokYonetimi = new Button();
            btnSatinAlma = new Button();
            btnRaporlar = new Button();
            btnCikisYap = new Button();
            btnKullaniciYonetimi = new Button();
            SuspendLayout();
            // 
            // lblKullaniciBilgi
            // 
            lblKullaniciBilgi.AutoSize = true;
            lblKullaniciBilgi.Location = new Point(12, 9);
            lblKullaniciBilgi.Name = "lblKullaniciBilgi";
            lblKullaniciBilgi.Size = new Size(113, 15);
            lblKullaniciBilgi.TabIndex = 0;
            lblKullaniciBilgi.Text = "Giriş Yapan Kullanıcı";
            // 
            // btnStokYonetimi
            // 
            btnStokYonetimi.Location = new Point(314, 70);
            btnStokYonetimi.Name = "btnStokYonetimi";
            btnStokYonetimi.Size = new Size(196, 69);
            btnStokYonetimi.TabIndex = 1;
            btnStokYonetimi.Text = "📦 Stok ve Depo Yönetimi";
            btnStokYonetimi.UseVisualStyleBackColor = true;
            btnStokYonetimi.Click += btnStokYonetimi_Click;
            // 
            // btnSatinAlma
            // 
            btnSatinAlma.Location = new Point(314, 145);
            btnSatinAlma.Name = "btnSatinAlma";
            btnSatinAlma.Size = new Size(196, 69);
            btnSatinAlma.TabIndex = 2;
            btnSatinAlma.Text = "\U0001f6d2 Satın Alma Sistemi";
            btnSatinAlma.UseVisualStyleBackColor = true;
            btnSatinAlma.Click += btnSatinAlma_Click;
            // 
            // btnRaporlar
            // 
            btnRaporlar.Location = new Point(314, 220);
            btnRaporlar.Name = "btnRaporlar";
            btnRaporlar.Size = new Size(196, 69);
            btnRaporlar.TabIndex = 3;
            btnRaporlar.Text = "📊 Raporlar";
            btnRaporlar.UseVisualStyleBackColor = true;
            btnRaporlar.Click += btnRaporlar_Click;
            // 
            // btnCikisYap
            // 
            btnCikisYap.Location = new Point(713, 5);
            btnCikisYap.Name = "btnCikisYap";
            btnCikisYap.Size = new Size(75, 23);
            btnCikisYap.TabIndex = 4;
            btnCikisYap.Text = "Çıkış Yap";
            btnCikisYap.UseVisualStyleBackColor = true;
            btnCikisYap.Click += btnCikisYap_Click;
            // 
            // btnKullaniciYonetimi
            // 
            btnKullaniciYonetimi.Location = new Point(314, 295);
            btnKullaniciYonetimi.Name = "btnKullaniciYonetimi";
            btnKullaniciYonetimi.Size = new Size(196, 69);
            btnKullaniciYonetimi.TabIndex = 5;
            btnKullaniciYonetimi.Text = "👤Kullanıcı Yönetimi";
            btnKullaniciYonetimi.UseVisualStyleBackColor = true;
            btnKullaniciYonetimi.Click += btnKullaniciYonetimi_Click;
            // 
            // FrmAnaMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnKullaniciYonetimi);
            Controls.Add(btnCikisYap);
            Controls.Add(btnRaporlar);
            Controls.Add(btnSatinAlma);
            Controls.Add(btnStokYonetimi);
            Controls.Add(lblKullaniciBilgi);
            Name = "FrmAnaMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAnaMenu";
            Load += FrmAnaMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblKullaniciBilgi;
        private Button btnStokYonetimi;
        private Button btnSatinAlma;
        private Button btnRaporlar;
        private Button btnCikisYap;
        private Button btnKullaniciYonetimi;
    }
}