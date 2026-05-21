namespace CokDepoluStokOtomasyonu
{
    partial class FrmKullanicilar
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
            dgvKullanicilar = new DataGridView();
            btnPasifKullanicilar = new Button();
            btnYeniKullanici = new Button();
            btnLoglariGoster = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvKullanicilar).BeginInit();
            SuspendLayout();
            // 
            // dgvKullanicilar
            // 
            dgvKullanicilar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKullanicilar.Location = new Point(12, 12);
            dgvKullanicilar.Name = "dgvKullanicilar";
            dgvKullanicilar.Size = new Size(776, 329);
            dgvKullanicilar.TabIndex = 0;
            // 
            // btnPasifKullanicilar
            // 
            btnPasifKullanicilar.Location = new Point(12, 376);
            btnPasifKullanicilar.Name = "btnPasifKullanicilar";
            btnPasifKullanicilar.Size = new Size(193, 23);
            btnPasifKullanicilar.TabIndex = 1;
            btnPasifKullanicilar.Text = "Pasif Kullanıcıları Göster";
            btnPasifKullanicilar.UseVisualStyleBackColor = true;
            btnPasifKullanicilar.Click += btnPasifKullanicilar_Click_1;
            // 
            // btnYeniKullanici
            // 
            btnYeniKullanici.Location = new Point(12, 347);
            btnYeniKullanici.Name = "btnYeniKullanici";
            btnYeniKullanici.Size = new Size(193, 23);
            btnYeniKullanici.TabIndex = 2;
            btnYeniKullanici.Text = "Yeni Kullanıcı Ekle";
            btnYeniKullanici.UseVisualStyleBackColor = true;
            btnYeniKullanici.Click += btnYeniKullanici_Click;
            // 
            // btnLoglariGoster
            // 
            btnLoglariGoster.Location = new Point(650, 347);
            btnLoglariGoster.Name = "btnLoglariGoster";
            btnLoglariGoster.Size = new Size(138, 23);
            btnLoglariGoster.TabIndex = 3;
            btnLoglariGoster.Text = "İşlem Loglarını İncele";
            btnLoglariGoster.UseVisualStyleBackColor = true;
            btnLoglariGoster.Click += btnLoglariGoster_Click;
            // 
            // FrmKullanicilar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLoglariGoster);
            Controls.Add(btnYeniKullanici);
            Controls.Add(btnPasifKullanicilar);
            Controls.Add(dgvKullanicilar);
            Name = "FrmKullanicilar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmKullanicilar";
            Load += FrmKullanicilar_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvKullanicilar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvKullanicilar;
        private Button btnPasifKullanicilar;
        private Button btnYeniKullanici;
        private Button btnLoglariGoster;
    }
}