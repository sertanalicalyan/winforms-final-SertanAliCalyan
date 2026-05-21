namespace CokDepoluStokOtomasyonu
{
    partial class FrmGiris
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            txtKullaniciAdi = new TextBox();
            txtSifre = new TextBox();
            btnGiris = new Button();
            btnGoster = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(371, 109);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 0;
            label1.Text = "Kullanıcı Adı";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(391, 173);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 1;
            label2.Text = "Şifre";
            // 
            // txtKullaniciAdi
            // 
            txtKullaniciAdi.Location = new Point(357, 127);
            txtKullaniciAdi.Name = "txtKullaniciAdi";
            txtKullaniciAdi.Size = new Size(100, 23);
            txtKullaniciAdi.TabIndex = 2;
            txtKullaniciAdi.KeyDown += txtKullaniciAdi_KeyDown;
            // 
            // txtSifre
            // 
            txtSifre.Location = new Point(357, 191);
            txtSifre.Name = "txtSifre";
            txtSifre.PasswordChar = '*';
            txtSifre.Size = new Size(100, 23);
            txtSifre.TabIndex = 3;
            // 
            // btnGiris
            // 
            btnGiris.Location = new Point(371, 233);
            btnGiris.Name = "btnGiris";
            btnGiris.Size = new Size(75, 23);
            btnGiris.TabIndex = 4;
            btnGiris.Text = "Giriş Yap";
            btnGiris.UseVisualStyleBackColor = true;
            btnGiris.Click += btnGiris_Click;
            // 
            // btnGoster
            // 
            btnGoster.FlatStyle = FlatStyle.Flat;
            btnGoster.Location = new Point(463, 191);
            btnGoster.Name = "btnGoster";
            btnGoster.Size = new Size(20, 23);
            btnGoster.TabIndex = 5;
            btnGoster.Text = "👁";
            btnGoster.UseVisualStyleBackColor = true;
            // 
            // FrmGiris
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnGoster);
            Controls.Add(btnGiris);
            Controls.Add(txtSifre);
            Controls.Add(txtKullaniciAdi);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmGiris";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += FrmGiris_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtKullaniciAdi;
        private TextBox txtSifre;
        private Button btnGiris;
        private Button btnGoster;
    }
}
