using System;
using UnityEngine;

namespace Pragma.Common
{
    [Serializable]
    public class SerializedTuple<T1, T2>
    {
        [SerializeField] private T1 _value1;

        [SerializeField] private T2 _value2;
            
        public SerializedTuple(T1 value1, T2 value2)
        {
            _value1 = value1;
            _value2 = value2;
        }

        public T1 Value1 { get => _value1; set => _value1 = value; }
        public T2 Value2 { get => _value2; set => _value2 = value; }
    }
}