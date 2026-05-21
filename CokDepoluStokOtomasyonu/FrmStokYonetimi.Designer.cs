namespace CokDepoluStokOtomasyonu
{
    partial class FrmStokYonetimi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvStoklar = new DataGridView();
            btnYeniIslem = new Button();
            btnTeklifYonetimi = new Button();
            btnMalKabul = new Button();
            btnDepolar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvStoklar).BeginInit();
            SuspendLayout();
            // 
            // dgvStoklar
            // 
            dgvStoklar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvStoklar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStoklar.Location = new Point(12, 60);
            dgvStoklar.Name = "dgvStoklar";
            dgvStoklar.RowHeadersWidth = 51;
            dgvStoklar.Size = new Size(776, 378);
            dgvStoklar.TabIndex = 0;
            // 
            // btnYeniIslem
            // 
            btnYeniIslem.Location = new Point(202, 14);
            btnYeniIslem.Name = "btnYeniIslem";
            btnYeniIslem.Size = new Size(184, 36);
            btnYeniIslem.TabIndex = 1;
            btnYeniIslem.Text = "➕ Yeni Stok İşlemi";
            btnYeniIslem.UseVisualStyleBackColor = true;
            btnYeniIslem.Click += btnYeniIslem_Click;
            // 
            // btnTeklifYonetimi
            // 
            btnTeklifYonetimi.Location = new Point(392, 16);
            btnTeklifYonetimi.Name = "btnTeklifYonetimi";
            btnTeklifYonetimi.Size = new Size(184, 34);
            btnTeklifYonetimi.TabIndex = 2;
            btnTeklifYonetimi.Text = "Teklif Yönetimi";
            btnTeklifYonetimi.UseVisualStyleBackColor = true;
            btnTeklifYonetimi.Click += btnTeklifYonetimi_Click;
            // 
            // btnMalKabul
            // 
            btnMalKabul.Location = new Point(582, 16);
            btnMalKabul.Name = "btnMalKabul";
            btnMalKabul.Size = new Size(184, 34);
            btnMalKabul.TabIndex = 3;
            btnMalKabul.Text = "Mal Kabul İşlemleri";
            btnMalKabul.UseVisualStyleBackColor = true;
            btnMalKabul.Click += btnMalKabul_Click;
            // 
            // btnDepolar
            // 
            btnDepolar.Location = new Point(12, 14);
            btnDepolar.Name = "btnDepolar";
            btnDepolar.Size = new Size(184, 36);
            btnDepolar.TabIndex = 4;
            btnDepolar.Text = "Depo Yönetimi";
            btnDepolar.UseVisualStyleBackColor = true;
            // 
            // FrmStokYonetimi
            // 
            ClientSize = new Size(800, 450);
            Controls.Add(btnDepolar);
            Controls.Add(btnMalKabul);
            Controls.Add(btnTeklifYonetimi);
            Controls.Add(btnYeniIslem);
            Controls.Add(dgvStoklar);
            Name = "FrmStokYonetimi";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Stok ve Depo Yönetimi";
            Load += FrmStokYonetimi_Load;
            ((System.ComponentModel.ISupportInitialize)dgvStoklar).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStoklar;
        private System.Windows.Forms.Button btnYeniIslem;
        private Button btnTeklifYonetimi;
        private Button btnMalKabul;
        private Button btnDepolar;
    }
}