using System;

namespace Pragma.Common
{
    public interface IReadOnlyRx<out T>
    {
        public void Subscribe(Action<T> listener);
        public void Unsubscribe(Action<T> listener);
        
        public T Value { get; }
    }
}