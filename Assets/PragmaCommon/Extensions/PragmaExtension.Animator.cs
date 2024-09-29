using UnityEngine;

namespace Pragma.Common
{
    public static partial class PragmaExtension
    {
        public static bool IsEndCurrentAnimation(this Animator animator, int layer = 0)
        {
            return animator.GetCurrentAnimatorStateInfo(layer).normalizedTime + Time.deltaTime >= 1 && !animator.IsInTransition(layer);
        }
    }
}