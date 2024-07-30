using System.Collections.Generic;
using UnityEngine;

namespace Pragma.Common.Hexagon
{
    public static class HexagonGrid
    {
        public static List<Hex> GenerateHexagonGrid(int radius)
        {
            var hexagonsMap = new List<Hex>();

            for (var q = -radius; q <= radius; q++)
            {
                var r1 = Mathf.Max(-radius, -q - radius);
                var r2 = Mathf.Min(radius, -q + radius);

                for (var r = r1; r <= r2; r++)
                {
                    hexagonsMap.Add(new Hex(q, r, -q - r));
                }
            }

            return hexagonsMap;

            // var firstEl = hexagonsMap.FindIndex(hex => hex.q == 0 && hex.r == 0 && hex.s == 0);
            // hexagonsMap.RemoveAt(firstEl);
            //
            // var result = (new List<Hex> {new Hex(0, 0, 0)});
            // result.AddRange(hexagonsMap.OrderBy(e => Random.value).ToList());
            //
            // return result;
        }

        public static int GetRadius(int count)
        {
            var radius = 0;
            count -= 1;

            while (count > 0)
            {
                radius += 1;
                var e = radius * 6;
                count -= e;
            }

            return radius;
        }
    }
}