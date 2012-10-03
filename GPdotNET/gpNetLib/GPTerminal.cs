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
namespace GPNETLib
{
    //terminali u GP sa sastoje od nezavisno promjenjivih i slucajno generiranih konstanti
    //ova klasa sadrzi naziv terminala koji je pohranjen pod tim nazivom u experimentalnim podacima
    // i slucalno generiranim konstantama
    // npr x1, x2, ... ili R1, R2 ...
    [Serializable]
    public class GPTerminal
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsConstant { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
