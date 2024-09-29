using System;
using System.Collections.Generic;

namespace Pragma.Common
{
    public class RxCondition<TParam> : IReadOnlyRx<bool>
    {
        private RxField<bool> _isConditionMet;
        private List<ConditionResult> _conditionResults;
        private Func<TParam, bool> _condition;
        private RxConditionType _conditionType;

        public bool Value => _isConditionMet.Value;

        public RxCondition(IEnumerable<IReadOnlyRx<TParam>> fields, Func<TParam, bool> condition, RxConditionType conditionType)
        {
            _conditionResults = new List<ConditionResult>();
            _condition = condition;
            _conditionType = conditionType;

            foreach (var field in fields)
            {
                var conditionResult = new ConditionResult
                {
                    wrapper = new RxFieldSelfArgumentWrapper<TParam>(field),
                    value = _condition.Invoke(field.Value),
                };
                
                conditionResult.wrapper.Subscribe(OnUpdateValue);
                _conditionResults.Add(conditionResult);
            }
        }

        public void ForceInvoke() => _isConditionMet.ForceInvoke();
        public void Subscribe(Action<bool> listener) => _isConditionMet.Subscribe(listener);
        public void Unsubscribe(Action<bool> listener) => _isConditionMet.Unsubscribe(listener);
        
        private void OnUpdateValue(IReadOnlyRx<TParam> field)
        {
            var conditionResult = _conditionResults.Find(result => result.wrapper.Field == field);
            var isConditionMetCurrent = _condition.Invoke(field.Value);

            if (conditionResult.value == isConditionMetCurrent)
            {
                return;
            }
            
            conditionResult.value = isConditionMetCurrent;
            _isConditionMet.Value = IsConditionMet();
        }

        private bool IsConditionMet()
        {
            return _conditionType switch
            {
                RxConditionType.All => IsConditionAll(),
                RxConditionType.Any => IsConditionAny(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool IsConditionAll()
        {
            foreach (var field in _conditionResults)
            {
                if (!field.value)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsConditionAny()
        {
            foreach (var field in _conditionResults)
            {
                if (field.value)
                {
                    return true;
                }
            }

            return false;
        }
        
        private class ConditionResult
        {
            public RxFieldSelfArgumentWrapper<TParam> wrapper;
            public bool value;
        }
    }

    public enum RxConditionType
    {
        All = 1,
        Any = 2,
    }
}