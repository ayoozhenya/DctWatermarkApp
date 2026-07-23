namespace DctWatermarkApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxOriginal;
        private System.Windows.Forms.PictureBox pictureBoxResult;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnEmbed;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtWatermark;
        private System.Windows.Forms.NumericUpDown numAlpha;
        private System.Windows.Forms.Label lblMetrics;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBoxOriginal = new System.Windows.Forms.PictureBox();
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnEmbed = new System.Windows.Forms.Button();
            this.btnExtract = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtWatermark = new System.Windows.Forms.TextBox();
            this.numAlpha = new System.Windows.Forms.NumericUpDown();
            this.lblMetrics = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAlpha)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxOriginal
            // 
            this.pictureBoxOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxOriginal.Location = new System.Drawing.Point(12, 80);
            this.pictureBoxOriginal.Name = "pictureBoxOriginal";
            this.pictureBoxOriginal.Size = new System.Drawing.Size(500, 500);
            this.pictureBoxOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginal.TabIndex = 0;
            this.pictureBoxOriginal.TabStop = false;
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxResult.Location = new System.Drawing.Point(530, 80);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(500, 500);
            this.pictureBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult.TabIndex = 1;
            this.pictureBoxResult.TabStop = false;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(90, 28);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Загрузить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnEmbed
            // 
            this.btnEmbed.Location = new System.Drawing.Point(110, 12);
            this.btnEmbed.Name = "btnEmbed";
            this.btnEmbed.Size = new System.Drawing.Size(90, 28);
            this.btnEmbed.TabIndex = 3;
            this.btnEmbed.Text = "Встроить";
            this.btnEmbed.UseVisualStyleBackColor = true;
            this.btnEmbed.Click += new System.EventHandler(this.btnEmbed_Click);
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(208, 12);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(90, 28);
            this.btnExtract.TabIndex = 4;
            this.btnExtract.Text = "Извлечь";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(306, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 28);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtWatermark
            // 
            this.txtWatermark.Location = new System.Drawing.Point(12, 50);
            this.txtWatermark.Name = "txtWatermark";
            this.txtWatermark.Size = new System.Drawing.Size(300, 20);
            this.txtWatermark.TabIndex = 6;
            this.txtWatermark.Text = "Пример";
            // 
            // numAlpha
            // 
            this.numAlpha.Location = new System.Drawing.Point(320, 50);
            this.numAlpha.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAlpha.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numAlpha.Name = "numAlpha";
            this.numAlpha.Size = new System.Drawing.Size(60, 20);
            this.numAlpha.TabIndex = 7;
            this.numAlpha.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblMetrics
            // 
            this.lblMetrics.Location = new System.Drawing.Point(12, 590);
            this.lblMetrics.Name = "lblMetrics";
            this.lblMetrics.Size = new System.Drawing.Size(900, 40);
            this.lblMetrics.TabIndex = 8;
            this.lblMetrics.Text = "Метрики:";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1044, 641);
            this.Controls.Add(this.lblMetrics);
            this.Controls.Add(this.numAlpha);
            this.Controls.Add(this.txtWatermark);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.btnEmbed);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.pictureBoxResult);
            this.Controls.Add(this.pictureBoxOriginal);
            this.Name = "MainForm";
            this.Text = "DCT Watermark App";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAlpha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
