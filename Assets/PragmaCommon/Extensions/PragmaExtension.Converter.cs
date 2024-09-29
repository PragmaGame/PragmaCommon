using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pragma.Common
{
    public static partial class PragmaExtension
    {
        public static bool FloatToBool(this float value)
        {
            return Mathf.Approximately(value,1f);
        }
        
        public static float BoolToFloat(this bool value)
        {
            return value ? 1f : 0f;
        }
        
        public static int BoolToInt(this bool value)
        {
            return value ? 1 : 0;
        }
        
        public static string DateTimeToString(this DateTime dateTime)
        {
            return dateTime.ToBinary().ToString();
        }

        public static DateTime StringToDateTime(this string timeString)
        {
            var timeLong = Convert.ToInt64(timeString);
            return DateTime.FromBinary(timeLong);
        }
        
        public static List<T> GetEnumList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}