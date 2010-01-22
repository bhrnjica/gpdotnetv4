using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNETLib
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
