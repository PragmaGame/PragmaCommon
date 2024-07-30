using System;
using UnityEngine;

namespace Pragma.Common
{
    [Serializable]
    public class ProcedureAnimParam<T> where T : struct
    {
        public float duration;
        public AnimationCurve curve;
        public T startValue;
        public T endValue;
    }
}