using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class AnimationCurveVector3 : AnimationCurve<Vector3>
    {
        protected override Vector3 Lerp(float value)
        {
            return Vector3.Lerp(min, max, value);
        }
    }
}