namespace CokDepoluStokOtomasyonu
{
    partial class FrmDepolar
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
            txtDepoAdi = new TextBox();
            btnDepoEkle = new Button();
            dgvDepolar = new DataGridView();
            txtSehir = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDepolar).BeginInit();
            SuspendLayout();
            // 
            // txtDepoAdi
            // 
            txtDepoAdi.Location = new Point(87, 27);
            txtDepoAdi.Name = "txtDepoAdi";
            txtDepoAdi.Size = new Size(100, 23);
            txtDepoAdi.TabIndex = 0;
            // 
            // btnDepoEkle
            // 
            btnDepoEkle.Location = new Point(83, 220);
            btnDepoEkle.Name = "btnDepoEkle";
            btnDepoEkle.Size = new Size(75, 23);
            btnDepoEkle.TabIndex = 4;
            btnDepoEkle.Text = "Yeni Depo Ekle";
            btnDepoEkle.UseVisualStyleBackColor = true;
            // 
            // dgvDepolar
            // 
            dgvDepolar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDepolar.Location = new Point(193, 12);
            dgvDepolar.Name = "dgvDepolar";
            dgvDepolar.Size = new Size(595, 410);
            dgvDepolar.TabIndex = 5;
            // 
            // txtSehir
            // 
            txtSehir.Location = new Point(87, 59);
            txtSehir.Name = "txtSehir";
            txtSehir.Size = new Size(100, 23);
            txtSehir.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 30);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 7;
            label1.Text = "Depo Adı:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 62);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 8;
            label2.Text = "Şehir:";
            // 
            // FrmDepolar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtSehir);
            Controls.Add(dgvDepolar);
            Controls.Add(btnDepoEkle);
            Controls.Add(txtDepoAdi);
            Name = "FrmDepolar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmDepolar";
            ((System.ComponentModel.ISupportInitialize)dgvDepolar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDepoAdi;
        private Button btnDepoEkle;
        private DataGridView dgvDepolar;
        private TextBox txtSehir;
        private Label label1;
        private Label label2;
    }
}