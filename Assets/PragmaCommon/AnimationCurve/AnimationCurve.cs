using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public abstract class AnimationCurve<T>
    {
        [SerializeField] private AnimationCurve _curve;
        
        [SerializeField] protected T min;
        [SerializeField] protected T max;

        public T Evaluate(float value)
        {
            return Lerp(_curve.Evaluate(value));
        }

        protected abstract T Lerp(float value);
    }
}