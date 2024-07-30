using System;

namespace Pragma.Common
{
    public static partial class Extension
    {
        public static string DateTimeToString(this DateTime dateTime)
        {
            return dateTime.ToBinary().ToString();
        }

        public static DateTime StringToDateTime(this string timeString)
        {
            var timeLong = Convert.ToInt64(timeString);
            return DateTime.FromBinary(timeLong);
        }
    }
}