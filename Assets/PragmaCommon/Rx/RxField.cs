using System;
using System.Collections.Generic;

namespace Pragma.Common
{
    public class RxField<T> : IReadOnlyRx<T>
    {
        private T _value;
        private Action<T> _listeners;

        public RxField()
        {
            _value = default;
        }
        
        public RxField(T defaultValue)
        {
            _value = defaultValue;
        }
        
        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                {
                    return;
                }
                
                _value = value;
                _listeners?.Invoke(_value);
            }
        }

        public void ForceInvoke() => _listeners?.Invoke(_value);

        public void Subscribe(Action<T> listener) => _listeners += listener;
        public void Unsubscribe(Action<T> listener) => _listeners -= listener;
    }
}