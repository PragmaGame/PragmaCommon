using UnityEngine;

namespace Pragma.Common
{
    public static partial class PragmaExtension
    {
        /// <summary>
        /// Is a specific layer actived in the given LayerMask?
        /// </summary>
        /// <param name="mask">The LayerMask.</param>
        /// <param name="layer">The layer to check for.</param>
        /// <returns>True if the layer is activated.</returns>
        public static bool ContainsLayer(this LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) != 0;
        }
    }
}