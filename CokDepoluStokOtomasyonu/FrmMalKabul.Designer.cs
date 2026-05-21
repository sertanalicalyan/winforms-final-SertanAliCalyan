namespace CokDepoluStokOtomasyonu
{
    partial class FrmMalKabul
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
            dgvOnayliTalepler = new DataGridView();
            cmbDepo = new ComboBox();
            txtGelenMiktar = new TextBox();
            btnKabulEt = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvOnayliTalepler).BeginInit();
            SuspendLayout();
            // 
            // dgvOnayliTalepler
            // 
            dgvOnayliTalepler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOnayliTalepler.Location = new Point(12, 12);
            dgvOnayliTalepler.Name = "dgvOnayliTalepler";
            dgvOnayliTalepler.Size = new Size(342, 305);
            dgvOnayliTalepler.TabIndex = 0;
            // 
            // cmbDepo
            // 
            cmbDepo.FormattingEnabled = true;
            cmbDepo.Location = new Point(360, 12);
            cmbDepo.Name = "cmbDepo";
            cmbDepo.Size = new Size(213, 23);
            cmbDepo.TabIndex = 1;
            // 
            // txtGelenMiktar
            // 
            txtGelenMiktar.Location = new Point(360, 41);
            txtGelenMiktar.Name = "txtGelenMiktar";
            txtGelenMiktar.Size = new Size(213, 23);
            txtGelenMiktar.TabIndex = 2;
            // 
            // btnKabulEt
            // 
            btnKabulEt.Location = new Point(360, 70);
            btnKabulEt.Name = "btnKabulEt";
            btnKabulEt.Size = new Size(213, 23);
            btnKabulEt.TabIndex = 3;
            btnKabulEt.Text = "Ürünleri Teslim Al ve Stoka Ekle";
            btnKabulEt.UseVisualStyleBackColor = true;
            // 
            // FrmMalKabul
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnKabulEt);
            Controls.Add(txtGelenMiktar);
            Controls.Add(cmbDepo);
            Controls.Add(dgvOnayliTalepler);
            Name = "FrmMalKabul";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmMalKabul";
            ((System.ComponentModel.ISupportInitialize)dgvOnayliTalepler).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvOnayliTalepler;
        private ComboBox cmbDepo;
        private TextBox txtGelenMiktar;
        private Button btnKabulEt;
    }
}