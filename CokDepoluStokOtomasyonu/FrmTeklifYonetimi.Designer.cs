namespace CokDepoluStokOtomasyonu
{
    partial class FrmTeklifYonetimi
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
            dgvAcikTalepler = new DataGridView();
            dgvTeklifler = new DataGridView();
            txtTedarikci = new TextBox();
            txtFiyat = new TextBox();
            btnTeklifKaydet = new Button();
            btnTalebiOnayla = new Button();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAcikTalepler).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTeklifler).BeginInit();
            SuspendLayout();
            // 
            // dgvAcikTalepler
            // 
            dgvAcikTalepler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAcikTalepler.Location = new Point(12, 12);
            dgvAcikTalepler.Name = "dgvAcikTalepler";
            dgvAcikTalepler.Size = new Size(236, 426);
            dgvAcikTalepler.TabIndex = 0;
            // 
            // dgvTeklifler
            // 
            dgvTeklifler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTeklifler.Location = new Point(552, 12);
            dgvTeklifler.Name = "dgvTeklifler";
            dgvTeklifler.Size = new Size(236, 426);
            dgvTeklifler.TabIndex = 1;
            // 
            // txtTedarikci
            // 
            txtTedarikci.Location = new Point(347, 12);
            txtTedarikci.Name = "txtTedarikci";
            txtTedarikci.Size = new Size(172, 23);
            txtTedarikci.TabIndex = 2;
            // 
            // txtFiyat
            // 
            txtFiyat.Location = new Point(347, 41);
            txtFiyat.Name = "txtFiyat";
            txtFiyat.Size = new Size(172, 23);
            txtFiyat.TabIndex = 3;
            // 
            // btnTeklifKaydet
            // 
            btnTeklifKaydet.Location = new Point(347, 70);
            btnTeklifKaydet.Name = "btnTeklifKaydet";
            btnTeklifKaydet.Size = new Size(172, 23);
            btnTeklifKaydet.TabIndex = 4;
            btnTeklifKaydet.Text = "Teklifi Ekle";
            btnTeklifKaydet.UseVisualStyleBackColor = true;
            // 
            // btnTalebiOnayla
            // 
            btnTalebiOnayla.Location = new Point(347, 137);
            btnTalebiOnayla.Name = "btnTalebiOnayla";
            btnTalebiOnayla.Size = new Size(172, 23);
            btnTalebiOnayla.TabIndex = 5;
            btnTalebiOnayla.Text = "Satın Almayı Onayla";
            btnTalebiOnayla.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(263, 15);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 6;
            label1.Text = "Tedarikçi Adı:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(282, 44);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 7;
            label2.Text = "Fiyat:";
            // 
            // FrmTeklifYonetimi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnTalebiOnayla);
            Controls.Add(btnTeklifKaydet);
            Controls.Add(txtFiyat);
            Controls.Add(txtTedarikci);
            Controls.Add(dgvTeklifler);
            Controls.Add(dgvAcikTalepler);
            Name = "FrmTeklifYonetimi";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmTeklifYonetimi";
            ((System.ComponentModel.ISupportInitialize)dgvAcikTalepler).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTeklifler).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvAcikTalepler;
        private DataGridView dgvTeklifler;
        private TextBox txtTedarikci;
        private TextBox txtFiyat;
        private Button btnTeklifKaydet;
        private Button btnTalebiOnayla;
        private Label label1;
        private Label label2;
    }
}