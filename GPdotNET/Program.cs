//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2010 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See licence section of  http://gpdotnet.codeplex.com/license                         //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Threading;
using System.Windows.Forms;

namespace GPdotNET
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
            if (Properties.Settings.Default.Lang==1)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("bs");
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("bs");
            }
            //Properties.Settings.Default.Lang is INT type for further localization
            //For some other language you can set Lang=2,3,....
            Application.Run(new GPdotNetApp());
        }
    }
}
