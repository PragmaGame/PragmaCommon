using System;

namespace Pragma.Common
{
    public class Delayer
    {
        private float _delay = 0;
        private float _delayTime = 0;

        public event Action DelayEndEvent;
        public float Value => _delayTime;
        public float Normalized => 1f - (_delay != 0 ? _delayTime / _delay : 1f);

        public void AddDelay(float delay)
        {
            _delay += delay;
            _delayTime += delay;
        }

        public void SetDelay(float delay)
        {
            _delay = delay;
            _delayTime = delay;

            if (delay <= 0)
            {
                DelayEndEvent?.Invoke();
            }
        }

        public void Reset()
        {
            _delay = 0;
            _delayTime = 0;
        }

        public void Update(float deltaTime)
        {
            if (_delayTime <= 0) return;

            _delayTime -= deltaTime;

            if (_delayTime <= 0)
            {
                _delayTime = 0;
                DelayEndEvent?.Invoke();
            }
        }
    }
}