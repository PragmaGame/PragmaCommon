using System;

namespace Pragma.Common
{
    public static partial class PragmaExtension
    {
        public static bool IsHasFlag(int hash, int value)
        {
            return (hash & value) == value;
        }

        public static void AddFlag(ref int hash, int value)
        {
            hash |= value;
        }

        public static void RemoveFlag(ref int hash, int value)
        {
            hash ^= value;
        }
    }
}