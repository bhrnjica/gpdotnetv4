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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace GPdotNET.Core
{
    /// <summary>
    /// Implementation of concurrent memory pool
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> where T : new()
        {
            private static ConcurrentStack<T> _items = new ConcurrentStack<T>();
            public static T Get()
            {
                T item;
                if (_items.TryPop(out item))
                    return item;
                else
                    return new T();
            }
   
            public static void Free(T item)
            {
               _items.Push(item);
            }
        }
}
