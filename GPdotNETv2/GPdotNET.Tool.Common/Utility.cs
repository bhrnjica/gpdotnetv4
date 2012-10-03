//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2012 Bahrudin Hrnjica                                                 //
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
using System.Drawing;
using System.Reflection;

namespace GPdotNET.Tool
{
    /// <summary>
    /// General utility calass
    /// </summary>
    public static class Utility
    {
        public static bool IsNumber<T>(this T obj)
        {
            Type objType = obj.GetType();

            if (objType.IsPrimitive)
            {
                if (objType == typeof(object) ||
                    objType == typeof(string) ||
                    objType == typeof(bool))
                    return false;

                return true;
            }

            return false;
        }

        public static Image LoadImageFromName(string name)
        {
            Assembly asm = Assembly.GetEntryAssembly();
           // string appName = Assembly.GetEntryAssembly().GetName().Name;
            var pic = asm.GetManifestResourceStream(name/*"GPdotNET.App.Resources.gpabout.png"*/);
            return Image.FromStream(pic);
        }

        public static Icon LoadIconFromName(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var pic = asm.GetManifestResourceStream(name/*"GPdotNET.App.Resources.gpabout.png"*/);
            return new Icon(pic);
        }
    }
}
