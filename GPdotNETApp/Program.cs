using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace GPdotNETApp
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

            //Bosnian language settings, otherwise use English
            if (Properties.Settings.Default.Lang)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("bs-Latn");
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("bs-Latn");
            }


            Application.Run(new GPdotNetApp());
        }
    }
}
