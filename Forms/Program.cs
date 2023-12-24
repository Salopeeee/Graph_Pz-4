using System;
using System.Windows.Forms;
using Forms;

namespace Forms
{
    static class Program
    {
        /// <summary>
        /// Головна точка входу для додатку.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Створення головної форми
            MainForm mainForm = new MainForm();
            // Передбачаючи, що ви вже визначили GraphController та підключили відповідний простір імен
            // GraphController graphController = new GraphController(mainForm);

            // Запуск головної форми
            Application.Run(mainForm);
        }
    }
}
