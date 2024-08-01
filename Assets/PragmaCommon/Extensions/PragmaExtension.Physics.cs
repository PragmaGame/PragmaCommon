using UnityEngine;

namespace Pragma.Common
{
    public static partial class PragmaExtension
    {
        /// <summary>
        /// Creates a Bounds encapsulating all given colliders bounds.
        /// </summary>
        /// <param name="colliders">The colliders.</param>
        /// <returns>A Bounds encapsulating all given colliders bounds.</returns>
        public static Bounds CombineColliderBounds(Collider[] colliders)
        {
            var bounds = colliders[0].bounds;

            foreach (var colliderComponent in colliders)
            {
                bounds.Encapsulate(colliderComponent.bounds);
            }

            return bounds;
        }

        /// <summary>
        /// Given a CharacterController and a point of origin (the lower point of the capsule), this returns the
        /// point1, point2 and radius needed to fill a CapsuleCast().
        /// </summary>
        /// <param name="characterController">The CharacterController to use as the capsule, providing scale, radius, height and center offset.</param>
        /// <param name="origin">The capsule cast starting point at the lower end of the capsule.</param>
        /// <param name="point1">Outputs the point1 parameter to be used in the CapsuleCast()</param>
        /// <param name="point2">Outputs the point2 parameter to be used in the CapsuleCast()</param>
        /// <param name="radius">Outputs the radius parameter to be used in the CapsuleCast()</param>
        public static void GetCapsuleCastData(CharacterController characterController, Vector3 origin,
            out Vector3 point1, out Vector3 point2, out float radius)
        {
            var scale = characterController.transform.lossyScale;
            radius = characterController.radius * scale.x;
            var height = characterController.height * scale.y - (radius * 2);
            var center = characterController.center;
            center.Scale(scale);
            point1 = origin + center + Vector3.down * (height / 2f);
            point2 = point1 + Vector3.up * height;
        }

    }
}