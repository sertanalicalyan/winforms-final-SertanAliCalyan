namespace CokDepoluStokOtomasyonu
{
    partial class FrmIslemLoglari
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
            dgvLoglar = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvLoglar).BeginInit();
            SuspendLayout();
            // 
            // dgvLoglar
            // 
            dgvLoglar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLoglar.Location = new Point(12, 12);
            dgvLoglar.Name = "dgvLoglar";
            dgvLoglar.Size = new Size(776, 426);
            dgvLoglar.TabIndex = 0;
            // 
            // FrmIslemLoglari
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvLoglar);
            Name = "FrmIslemLoglari";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmIslemLoglari";
            Load += FrmIslemLoglari_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLoglar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvLoglar;
    }
}