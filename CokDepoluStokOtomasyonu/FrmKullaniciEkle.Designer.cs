namespace CokDepoluStokOtomasyonu
{
    partial class FrmKullaniciEkle
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
            txtKullaniciAdi = new TextBox();
            txtSifre = new TextBox();
            cmbRol = new ComboBox();
            btnKaydet = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnGoster = new Button();
            SuspendLayout();
            // 
            // txtKullaniciAdi
            // 
            txtKullaniciAdi.Location = new Point(334, 87);
            txtKullaniciAdi.Name = "txtKullaniciAdi";
            txtKullaniciAdi.Size = new Size(153, 23);
            txtKullaniciAdi.TabIndex = 0;
            // 
            // txtSifre
            // 
            txtSifre.Location = new Point(334, 116);
            txtSifre.Name = "txtSifre";
            txtSifre.Size = new Size(153, 23);
            txtSifre.TabIndex = 1;
            // 
            // cmbRol
            // 
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(334, 145);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(153, 23);
            cmbRol.TabIndex = 2;
            // 
            // btnKaydet
            // 
            btnKaydet.Location = new Point(334, 174);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(153, 23);
            btnKaydet.TabIndex = 3;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = true;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(252, 90);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 4;
            label1.Text = "Kullanıcı Adı:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(271, 119);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 5;
            label2.Text = "Şifre:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(280, 146);
            label3.Name = "label3";
            label3.Size = new Size(0, 15);
            label3.TabIndex = 6;
            // 
            // btnGoster
            // 
            btnGoster.FlatStyle = FlatStyle.Flat;
            btnGoster.Location = new Point(493, 115);
            btnGoster.Name = "btnGoster";
            btnGoster.Size = new Size(19, 23);
            btnGoster.TabIndex = 7;
            btnGoster.Text = "👁";
            btnGoster.UseVisualStyleBackColor = true;
            // 
            // FrmKullaniciEkle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnGoster);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnKaydet);
            Controls.Add(cmbRol);
            Controls.Add(txtSifre);
            Controls.Add(txtKullaniciAdi);
            Name = "FrmKullaniciEkle";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmKullaniciEkle";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtKullaniciAdi;
        private TextBox txtSifre;
        private ComboBox cmbRol;
        private Button btnKaydet;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnGoster;
    }
}