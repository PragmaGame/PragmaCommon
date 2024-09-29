using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class AnimationCurveVector2 : AnimationCurve<Vector2>
    {
        protected override Vector2 Lerp(float value)
        {
            return Vector2.Lerp(min, max, value);
        }
    }
}