using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Pragma.Common
{
    public class AsyncAction<T>
    {
        private List<Func<T, CancellationToken, UniTask>> _listeners;

        public AsyncAction()
        {
            _listeners = new List<Func<T, CancellationToken, UniTask>>();
        }

        public bool TrySubscribe(Func<T, CancellationToken, UniTask> action)
        {
            if (_listeners.Contains(action))
            {
                return false;
            }
            
            _listeners.Add(action);

            return true;
        }

        public void Subscribe(Func<T, CancellationToken, UniTask> action)
        {
            _listeners.Add(action);
        }

        public void Unsubscribe(Func<T, CancellationToken, UniTask> action)
        {
            _listeners.Remove(action);
        }

        public IEnumerable<UniTask> GetInvocationList(T arg, CancellationToken token = default)
        {
            return _listeners.Count == 0 ? Enumerable.Empty<UniTask>() : _listeners.Select(action => action(arg, token));
        }

        public async UniTask InvokeSimultaneously(T arg, CancellationToken token = default)
        {
            if (_listeners.Count == 0)
            {
                return;
            }
            
            await UniTask.WhenAll(GetInvocationList(arg, token));
        }

        public async UniTask InvokeSequence(T arg, CancellationToken token = default)
        {
            if (_listeners.Count == 0)
            {
                return;
            }
            
            foreach (var listener in _listeners)
            {
                await listener.Invoke(arg, token);
            }
        }
    }
}