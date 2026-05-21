namespace CokDepoluStokOtomasyonu
{
    partial class FrmKullaniciDuzenle
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
            btnGoster = new Button();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnGuncelle = new Button();
            cmbRol = new ComboBox();
            txtSifre = new TextBox();
            txtKullaniciAdi = new TextBox();
            SuspendLayout();
            // 
            // btnGoster
            // 
            btnGoster.FlatStyle = FlatStyle.Flat;
            btnGoster.Location = new Point(502, 141);
            btnGoster.Name = "btnGoster";
            btnGoster.Size = new Size(19, 23);
            btnGoster.TabIndex = 15;
            btnGoster.Text = "👁";
            btnGoster.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(289, 172);
            label3.Name = "label3";
            label3.Size = new Size(0, 15);
            label3.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(280, 145);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 13;
            label2.Text = "Şifre:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(261, 116);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 12;
            label1.Text = "Kullanıcı Adı:";
            // 
            // btnGuncelle
            // 
            btnGuncelle.Location = new Point(343, 200);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(153, 23);
            btnGuncelle.TabIndex = 11;
            btnGuncelle.Text = "Güncelle";
            btnGuncelle.UseVisualStyleBackColor = true;
            // 
            // cmbRol
            // 
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(343, 171);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(153, 23);
            cmbRol.TabIndex = 10;
            // 
            // txtSifre
            // 
            txtSifre.Location = new Point(343, 142);
            txtSifre.Name = "txtSifre";
            txtSifre.Size = new Size(153, 23);
            txtSifre.TabIndex = 9;
            // 
            // txtKullaniciAdi
            // 
            txtKullaniciAdi.Location = new Point(343, 113);
            txtKullaniciAdi.Name = "txtKullaniciAdi";
            txtKullaniciAdi.Size = new Size(153, 23);
            txtKullaniciAdi.TabIndex = 8;
            // 
            // FrmKullaniciDuzenle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnGoster);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnGuncelle);
            Controls.Add(cmbRol);
            Controls.Add(txtSifre);
            Controls.Add(txtKullaniciAdi);
            Name = "FrmKullaniciDuzenle";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmKullaniciDuzenle";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGoster;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnGuncelle;
        private ComboBox cmbRol;
        private TextBox txtSifre;
        private TextBox txtKullaniciAdi;
    }
}