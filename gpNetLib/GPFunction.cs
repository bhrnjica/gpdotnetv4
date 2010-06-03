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

    //Skup funkcija koje se mogu naci u GP ova klasa je popunjena iz XML datoteke
    [Serializable]
    public class GPFunction
    {
       
        //Osobine svake funkcije
        //Da li je odabrana uprogramu
        public bool Selected { get; set; }

        //Kolikoima argumenata
        public int Aritry { get; set; }

        //Naziv funkcije
        public string Name { get; set; }

        //Opis funkcije
        public string Description { get; set; }

        //Definicija funkcije po kojoj se izračunava
        public string Definition { get; set; }

        //Definicija funkcije u Excel notaciji
        public string ExcelDefinition { get; set; }

        //Da li je standardna ili definisana od strane korisnika
        public bool IsReadOnly { get; set; }

        //Da li je funkcija neka slozena funkcija koja zahtjeva 
        // dodatne parametre poput mean, deviation, rsquare ili slicno
        public bool IsDistribution { get; set; }

        //Relative proportionas of selecting function
        public int Weight { get; set; }

        //Function ID
        public ushort ID { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
