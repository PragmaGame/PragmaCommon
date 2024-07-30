using System;
using UnityEngine;

namespace Pragma.Common
{
    [Serializable]
    public struct Vector3Bool
    {
        public bool x;
        public bool y;
        public bool z;

        public Vector3Bool(bool x, bool y, bool z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3Bool(int x, int y, int z)
        {
            this.x = x == 1;
            this.y = y == 1;
            this.z = z == 1;
        }

        public Vector3 GetVector3()
        {
            return new Vector3(x ? 1 : 0, y ? 1 : 0, z ? 1 : 0);
        }
    }
}