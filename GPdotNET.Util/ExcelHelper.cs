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
namespace GPdotNET.Util
{
    public class AlphaCharEnum
    {
        char[] alphabet = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                          'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                          'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// Enumeration of Ecels columns
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string AlphabetFromIndex(int index)
        {
            if (index == 0)
                return "";
            int firstLetter = index / 26;
            int secondLetter = index % 26;
            if (firstLetter == 0)
            {
                return alphabet[secondLetter - 1].ToString();
            }
            else
            {
                if (firstLetter > 26)
                    return "";//Not support number
                else if (secondLetter == 0 && firstLetter == 1)
                    return alphabet[25].ToString();
                else if (secondLetter == 0 && firstLetter < 1)
                    return alphabet[firstLetter - 1].ToString();
                else if (secondLetter == 0 && firstLetter > 1)
                    return alphabet[firstLetter - 2].ToString() + "Z";
                else
                    return alphabet[firstLetter - 1].ToString() + alphabet[secondLetter - 1].ToString();
            }

        }
    }
}
