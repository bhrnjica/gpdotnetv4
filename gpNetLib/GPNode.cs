// Copyright 2006-2009 Bahrudin Hrnjica (bhrnjica@hotmail.com)
// gpNETLib 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPNETLib
{
    [Serializable]
    public struct GPGenNode
    {
        public ushort IndexValue;
        public bool IsFunction;

        //CTOR
        public GPGenNode(ushort initValue)
        {
            this.IndexValue = initValue;
            IsFunction = false;
        }
        //Gene cloning
        public GPGenNode Clone()
        {
            GPGenNode clone =this;
            clone.IndexValue = this.IndexValue;
            clone.IsFunction = this.IsFunction;

            return clone;
        }
        //Docode 
        public string Decode()
        {
            return IndexValue.ToString();
        }
    }
}
