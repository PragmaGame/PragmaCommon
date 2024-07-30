using UnityEngine;

namespace Pragma.Common.Hexagon
{
    public struct Layout
    {
        public readonly Orientation orientation;
        public readonly Vector2 size;
        public readonly Vector2 origin;

        public Layout(Orientation orientation, Vector2 size, Vector2 origin)
        {
            this.orientation = orientation;
            this.size = size;
            this.origin = origin;
        }
    };
}