using System;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

namespace DisplayAutoRotation
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool onlyone;
            Mutex mtx = new Mutex(true, Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString(), out onlyone);
            if (onlyone)// запуск единственного экземпляра приложения
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else MessageBox.Show("Один экземпляр приложения уже запущен");
        }
    }
}
