using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class AnimationCurveFloat : AnimationCurve<float>
    {
        protected override float Lerp(float value)
        {
            return Mathf.Lerp(min, max, value);
        }
    }
}