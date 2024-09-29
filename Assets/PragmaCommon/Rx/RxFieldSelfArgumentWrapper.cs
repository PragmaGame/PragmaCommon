using System;

namespace Pragma.Common
{
    public class RxFieldSelfArgumentWrapper<T>
    {
        private readonly IReadOnlyRx<T> _field;
        private Action<IReadOnlyRx<T>> _listeners;

        public IReadOnlyRx<T> Field => _field;
        
        public T Value => _field.Value;

        public RxFieldSelfArgumentWrapper(IReadOnlyRx<T> field = null)
        {
            _field = field ?? new RxField<T>();
            _field.Subscribe(OnValueUpdate);
        }

        public void Subscribe(Action<IReadOnlyRx<T>> listener) => _listeners += listener;
        public void Unsubscribe(Action<IReadOnlyRx<T>> listener) => _listeners -= listener;

        private void OnValueUpdate(T _) => _listeners?.Invoke(_field);
    }
}