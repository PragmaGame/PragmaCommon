using UnityEngine;

namespace Pragma.Common
{
    public static partial class PragmaExtension
    {
        public static float LerpByCurve(this AnimationCurve curve, float evaluate, float a, float b)
        {
            var t = curve.Evaluate(evaluate);

            return Mathf.Lerp(a, b, t);
        }
        
        public static float LerpByCurve(this AnimationCurve curve, float evaluate, Vector2 ab)
        {
            return LerpByCurve(curve, evaluate, ab.x, ab.y);
        }
    }
}