using System;
using System.Windows.Forms;

namespace DctWatermarkApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());   // Загружаем нашу форму
        }
    }
}
