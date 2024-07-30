using System;
using UnityEngine;

namespace Pragma.Common.Hexagon
{
    public readonly struct Hex
    {
        public readonly int q;
        public readonly int r;
        public readonly int s;

        public int ID => Mathf.Abs(q) + Mathf.Abs(r) + Mathf.Abs(s);

        public Hex(int q, int r, int s)
        {
            if (q + r + s != 0)
                Debug.LogError("Invalid hexagon format");

            this.q = q;
            this.r = r;
            this.s = s;
        }

        #region Equality

        public bool Equals(Hex other) => q == other.q && r == other.r && s == other.s;
        public override bool Equals(object obj) => obj is Hex other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(q, r, s);

        #endregion
    };
}