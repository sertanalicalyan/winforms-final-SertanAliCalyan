namespace CokDepoluStokOtomasyonu
{
    partial class FrmRaporlar
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
            dgvRapor = new DataGridView();
            btnPdfAl = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRapor).BeginInit();
            SuspendLayout();
            // 
            // dgvRapor
            // 
            dgvRapor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRapor.Location = new Point(26, 12);
            dgvRapor.Name = "dgvRapor";
            dgvRapor.Size = new Size(1062, 384);
            dgvRapor.TabIndex = 0;
            // 
            // btnPdfAl
            // 
            btnPdfAl.ForeColor = Color.DarkGreen;
            btnPdfAl.Location = new Point(805, 402);
            btnPdfAl.Name = "btnPdfAl";
            btnPdfAl.Size = new Size(283, 23);
            btnPdfAl.TabIndex = 1;
            btnPdfAl.Text = "📄 Raporu PDF Olarak İndir";
            btnPdfAl.UseVisualStyleBackColor = true;
            btnPdfAl.Click += btnPdfAl_Click;
            // 
            // FrmRaporlar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 661);
            Controls.Add(btnPdfAl);
            Controls.Add(dgvRapor);
            Name = "FrmRaporlar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmRaporlar";
            Load += FrmRaporlar_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvRapor).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvRapor;
        private Button btnPdfAl;
    }
}