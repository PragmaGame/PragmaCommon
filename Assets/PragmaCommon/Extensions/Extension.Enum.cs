using System;
using System.Collections.Generic;
using System.Linq;

namespace Pragma.Common
{
    public static partial class Extension
    {
        public static List<T> GetEnumList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}