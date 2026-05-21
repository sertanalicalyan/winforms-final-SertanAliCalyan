namespace CokDepoluStokOtomasyonu
{
    partial class FrmStokIslemi
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
            cmbIslemTipi = new ComboBox();
            cmbUrun = new ComboBox();
            cmbKaynakDepo = new ComboBox();
            cmbHedefDepo = new ComboBox();
            nudMiktar = new NumericUpDown();
            txtGerekce = new TextBox();
            btnOnayla = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblHedefDepoBaslik = new Label();
            label5 = new Label();
            label6 = new Label();
            lblKaynakStok = new Label();
            lblHedefStok = new Label();
            ((System.ComponentModel.ISupportInitialize)nudMiktar).BeginInit();
            SuspendLayout();
            // 
            // cmbIslemTipi
            // 
            cmbIslemTipi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIslemTipi.FormattingEnabled = true;
            cmbIslemTipi.Location = new Point(237, 102);
            cmbIslemTipi.Name = "cmbIslemTipi";
            cmbIslemTipi.Size = new Size(342, 23);
            cmbIslemTipi.TabIndex = 0;
            cmbIslemTipi.SelectedIndexChanged += cmbIslemTipi_SelectedIndexChanged_1;
            // 
            // cmbUrun
            // 
            cmbUrun.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUrun.FormattingEnabled = true;
            cmbUrun.Location = new Point(237, 131);
            cmbUrun.Name = "cmbUrun";
            cmbUrun.Size = new Size(342, 23);
            cmbUrun.TabIndex = 1;
            cmbUrun.SelectedIndexChanged += cmbUrun_SelectedIndexChanged;
            // 
            // cmbKaynakDepo
            // 
            cmbKaynakDepo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKaynakDepo.FormattingEnabled = true;
            cmbKaynakDepo.Location = new Point(237, 160);
            cmbKaynakDepo.Name = "cmbKaynakDepo";
            cmbKaynakDepo.Size = new Size(342, 23);
            cmbKaynakDepo.TabIndex = 2;
            cmbKaynakDepo.SelectedIndexChanged += cmbKaynakDepo_SelectedIndexChanged;
            // 
            // cmbHedefDepo
            // 
            cmbHedefDepo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHedefDepo.FormattingEnabled = true;
            cmbHedefDepo.Location = new Point(237, 189);
            cmbHedefDepo.Name = "cmbHedefDepo";
            cmbHedefDepo.Size = new Size(342, 23);
            cmbHedefDepo.TabIndex = 3;
            cmbHedefDepo.SelectedIndexChanged += cmbHedefDepo_SelectedIndexChanged;
            // 
            // nudMiktar
            // 
            nudMiktar.Location = new Point(237, 218);
            nudMiktar.Name = "nudMiktar";
            nudMiktar.Size = new Size(342, 23);
            nudMiktar.TabIndex = 4;
            // 
            // txtGerekce
            // 
            txtGerekce.Location = new Point(237, 247);
            txtGerekce.Multiline = true;
            txtGerekce.Name = "txtGerekce";
            txtGerekce.Size = new Size(342, 93);
            txtGerekce.TabIndex = 5;
            // 
            // btnOnayla
            // 
            btnOnayla.Location = new Point(346, 346);
            btnOnayla.Name = "btnOnayla";
            btnOnayla.Size = new Size(116, 23);
            btnOnayla.TabIndex = 6;
            btnOnayla.Text = "✅ İşlemi Kaydet";
            btnOnayla.UseVisualStyleBackColor = true;
            btnOnayla.Click += btnOnayla_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(155, 105);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 7;
            label1.Text = "İşlem Tipi:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(155, 134);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 8;
            label2.Text = "Ürün Seçiniz:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(155, 163);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 9;
            label3.Text = "Kaynak Depo:";
            // 
            // lblHedefDepoBaslik
            // 
            lblHedefDepoBaslik.AutoSize = true;
            lblHedefDepoBaslik.Location = new Point(155, 192);
            lblHedefDepoBaslik.Name = "lblHedefDepoBaslik";
            lblHedefDepoBaslik.Size = new Size(73, 15);
            lblHedefDepoBaslik.TabIndex = 10;
            lblHedefDepoBaslik.Text = "Hedef Depo:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(155, 220);
            label5.Name = "label5";
            label5.Size = new Size(78, 15);
            label5.TabIndex = 11;
            label5.Text = "İşlem Miktarı:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(155, 250);
            label6.Name = "label6";
            label6.Size = new Size(52, 15);
            label6.TabIndex = 12;
            label6.Text = "Gerekçe:";
            // 
            // lblKaynakStok
            // 
            lblKaynakStok.AutoSize = true;
            lblKaynakStok.ForeColor = SystemColors.Highlight;
            lblKaynakStok.Location = new Point(585, 163);
            lblKaynakStok.Name = "lblKaynakStok";
            lblKaynakStok.Size = new Size(85, 15);
            lblKaynakStok.TabIndex = 13;
            lblKaynakStok.Text = "Mevcut Stok: 0";
            // 
            // lblHedefStok
            // 
            lblHedefStok.AutoSize = true;
            lblHedefStok.ForeColor = SystemColors.Highlight;
            lblHedefStok.Location = new Point(585, 192);
            lblHedefStok.Name = "lblHedefStok";
            lblHedefStok.Size = new Size(96, 15);
            lblHedefStok.TabIndex = 14;
            lblHedefStok.Text = "Hedefteki Stok: 0";
            // 
            // FrmStokIslemi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblHedefStok);
            Controls.Add(lblKaynakStok);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(lblHedefDepoBaslik);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnOnayla);
            Controls.Add(txtGerekce);
            Controls.Add(nudMiktar);
            Controls.Add(cmbHedefDepo);
            Controls.Add(cmbKaynakDepo);
            Controls.Add(cmbUrun);
            Controls.Add(cmbIslemTipi);
            Name = "FrmStokIslemi";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmStokIslemi";
            Load += FrmStokIslemi_Load;
            ((System.ComponentModel.ISupportInitialize)nudMiktar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbIslemTipi;
        private ComboBox cmbUrun;
        private ComboBox cmbKaynakDepo;
        private ComboBox cmbHedefDepo;
        private NumericUpDown nudMiktar;
        private TextBox txtGerekce;
        private Button btnOnayla;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblHedefDepoBaslik;
        private Label label5;
        private Label label6;
        private Label lblKaynakStok;
        private Label lblHedefStok;
    }
}