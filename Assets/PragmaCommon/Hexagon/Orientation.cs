using UnityEngine;

namespace Pragma.Common.Hexagon
{
    public struct Orientation
    {
        public static readonly Orientation LayoutPointy
            = new Orientation(Mathf.Sqrt(3),
                              Mathf.Sqrt(3) / 2.0f,
                              0.0f,
                              3.0f / 2.0f,
                              Mathf.Sqrt(3) / 3.0f,
                              -1.0f / 3.0f,
                              0.0f,
                              2.0f / 3.0f,
                              0.5f);

        public static Orientation layoutFlat
            = new Orientation(3.0f / 2.0f,
                              0.0f,
                              Mathf.Sqrt(3) / 2.0f,
                              Mathf.Sqrt(3),
                              2.0f / 3.0f,
                              0.0f,
                              -1.0f / 3.0f,
                              Mathf.Sqrt(3) / 3.0f,
                              0.0f);

        public readonly float f0;
        public readonly float f1;
        public readonly float f2;
        public readonly float f3;
        public readonly float b0;
        public readonly float b1;
        public readonly float b2;
        public readonly float b3;
        public readonly float start_angle; // in multiples of 60°

        public Orientation(float f0_, float f1_, float f2_, float f3_,
                           float b0_, float b1_, float b2_, float b3_,
                           float start_angle_)
        {
            f0 = f0_;
            f1 = f1_;
            f2 = f2_;
            f3 = f3_;
            b0 = b0_;
            b1 = b1_;
            b2 = b2_;
            b3 = b3_;
            start_angle = start_angle_;
        }
    };
}