using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class AnimationCurveColor : AnimationCurve<Color>
    {
        protected override Color Lerp(float value)
        {
            return Color.Lerp(min, max, value);
        }
    }
}