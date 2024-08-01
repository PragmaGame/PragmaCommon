#if UNITASK

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Pragma.Common
{
    public static partial class Extension
    {
        public static async UniTask Timer(float time, 
            float countdown, 
            bool isIgnoreTimeScale = false, 
            Action<float> tick = null, 
            Action timeOver = null, 
            CancellationToken token = default)
        {
            tick?.Invoke(time);
            
            while (time > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(countdown), isIgnoreTimeScale, cancellationToken : token);
        
                time -= countdown;
                
                tick?.Invoke(time);
            }
            
            timeOver?.Invoke();
        }
        
        public static UniTask<int> WaitMax(this UniTask task, TimeSpan timeSpan)
        {
            return UniTask.WhenAny(
                task,
                UniTask.Delay(timeSpan)
            );
        }
        
        public static async UniTask Sequence(IEnumerable<UniTask> tasks)
        {
            foreach (var task in tasks)
            {
                await task;
            }
        }
        
        public static async UniTask<bool> Sequence(IEnumerable<UniTask<bool>> tasks)
        {
            foreach (var task in tasks)
            {
                if (!await task)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

#endif