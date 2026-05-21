namespace CokDepoluStokOtomasyonu
{
    partial class FrmSatinAlma
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
            dgvEksikStoklar = new DataGridView();
            label1 = new Label();
            btnSiparisVer = new Button();
            cmbUrunler = new ComboBox();
            txtMiktar = new TextBox();
            btnTalepOlustur = new Button();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvEksikStoklar).BeginInit();
            SuspendLayout();
            // 
            // dgvEksikStoklar
            // 
            dgvEksikStoklar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEksikStoklar.Location = new Point(12, 28);
            dgvEksikStoklar.Name = "dgvEksikStoklar";
            dgvEksikStoklar.Size = new Size(516, 303);
            dgvEksikStoklar.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(322, 15);
            label1.TabIndex = 1;
            label1.Text = "🚨 Kritik Eşiğin Altına Düşen Ürünler (Acil Sipariş Gerekenler)";
            // 
            // btnSiparisVer
            // 
            btnSiparisVer.Location = new Point(302, 337);
            btnSiparisVer.Name = "btnSiparisVer";
            btnSiparisVer.Size = new Size(226, 23);
            btnSiparisVer.TabIndex = 2;
            btnSiparisVer.Text = "\U0001f6d2 Seçili Üründen Sipariş Ver";
            btnSiparisVer.UseVisualStyleBackColor = true;
            btnSiparisVer.Click += btnSiparisVer_Click;
            // 
            // cmbUrunler
            // 
            cmbUrunler.FormattingEnabled = true;
            cmbUrunler.Location = new Point(614, 28);
            cmbUrunler.Name = "cmbUrunler";
            cmbUrunler.Size = new Size(162, 23);
            cmbUrunler.TabIndex = 3;
            // 
            // txtMiktar
            // 
            txtMiktar.Location = new Point(614, 57);
            txtMiktar.Name = "txtMiktar";
            txtMiktar.Size = new Size(162, 23);
            txtMiktar.TabIndex = 4;
            // 
            // btnTalepOlustur
            // 
            btnTalepOlustur.Location = new Point(614, 86);
            btnTalepOlustur.Name = "btnTalepOlustur";
            btnTalepOlustur.Size = new Size(162, 23);
            btnTalepOlustur.TabIndex = 5;
            btnTalepOlustur.Text = "Manuel Talep Oluştur";
            btnTalepOlustur.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(534, 31);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 6;
            label2.Text = "Ürün Seçiniz:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(534, 60);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 7;
            label3.Text = "Miktar:";
            // 
            // FrmSatinAlma
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnTalepOlustur);
            Controls.Add(txtMiktar);
            Controls.Add(cmbUrunler);
            Controls.Add(btnSiparisVer);
            Controls.Add(label1);
            Controls.Add(dgvEksikStoklar);
            Name = "FrmSatinAlma";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmSatinAlma";
            Load += FrmSatinAlma_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEksikStoklar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvEksikStoklar;
        private Label label1;
        private Button btnSiparisVer;
        private ComboBox cmbUrunler;
        private TextBox txtMiktar;
        private Button btnTalepOlustur;
        private Label label2;
        private Label label3;
    }
}