using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DctWatermarkApp
{
    public partial class MainForm : Form
    {
        Bitmap original;
        Bitmap watermarked;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.bmp;*.png;*.jpg;*.jpeg;*.tif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                original = (Bitmap)Image.FromFile(ofd.FileName);
                pictureBoxOriginal.Image = original;
                watermarked = null;
                pictureBoxResult.Image = null;
            }
        }

        private void btnEmbed_Click(object sender, EventArgs e)
        {
            if (original == null)
            {
                MessageBox.Show("Сначала загрузите изображение.");
                return;
            }

            string text = txtWatermark.Text;
            int alpha = (int)numAlpha.Value;

            try
            {
                watermarked = WatermarkLogic.EmbedDct(original, text, alpha);
                pictureBoxResult.Image = watermarked;

                double mse = ImageMetrics.MSE(original, watermarked);
                double psnr = ImageMetrics.PSNR(mse);
                double ssim = ImageMetrics.SSIM(original, watermarked);

                lblMetrics.Text = string.Format("MSE={0:F2},  PSNR={1:F2} dB,  SSIM={2:F4}", mse, psnr, ssim);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (watermarked == null)
            {
                MessageBox.Show("Нет изображения с ВЗ.");
                return;
            }

            string extracted = WatermarkLogic.ExtractDct(watermarked);

            MessageBox.Show("Извлечённый текст:\n" + extracted);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (watermarked == null)
            {
                MessageBox.Show("Нет результата для сохранения.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG|*.png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                watermarked.Save(sfd.FileName, ImageFormat.Png);
            }
        }
    }
}
