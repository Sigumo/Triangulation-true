using System;
using System.Windows.Forms;

namespace Triangulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var logic = new DomainLogic();
            Application.Run(new Form1(logic));
        }
    }
}
