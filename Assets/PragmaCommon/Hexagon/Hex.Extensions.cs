using UnityEngine;

namespace Pragma.Common.Hexagon
{
    public static class HexExtensions
    {
        public static readonly Hex[] Directions = new Hex[]
        {
            new Hex(1, 0, -1),
            new Hex(1, -1, 0),
            new Hex(0, -1, 1),
            new Hex(-1, 0, 1),
            new Hex(-1, 1, 0),
            new Hex(0, 1, -1)
        };

        public static Hex GetDirection(int direction)
        {
            if (direction is < 0 or >= 6)
                Debug.LogError("Invalid direction");

            return Directions[direction];
        }

        public static Hex Add(this Hex a, Hex b)
        {
            return new Hex(a.q + b.q, a.r + b.r, a.s + b.s);
        }

        public static Hex Subtract(this Hex a, Hex b)
        {
            return new Hex(a.q - b.q, a.r - b.r, a.s - b.s);
        }

        public static Hex Multiply(this Hex a, int k)
        {
            return new Hex(a.q * k, a.r * k, a.s * k);
        }

        public static int Length(this Hex hex)
        {
            return ((Mathf.Abs(hex.q) + Mathf.Abs(hex.r) + Mathf.Abs(hex.s)) / 2);
        }

        public static int Distance(this Hex a, Hex b)
        {
            return Length(Subtract(a, b));
        }

        public static Hex GetNeighbor(this Hex hex, int direction)
        {
            return Add(hex, GetDirection(direction));
        }

        public static Vector2 HexToPixel(this Hex h, Layout layout)
        {
            var orientation = layout.orientation;
            var x = (orientation.f0 * h.q + orientation.f1 * h.r) * layout.size.x;
            var y = (orientation.f2 * h.q + orientation.f3 * h.r) * layout.size.y;

            return new Vector2(x + layout.origin.x, y + layout.origin.y);
        }
    }
}