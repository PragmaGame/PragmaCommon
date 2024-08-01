using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pragma.Common
{
    // GameObject and Transform
    public static partial class PragmaExtension
    {
        public static void GetComponentIfNull<T>(ref T component, GameObject gameObject) where T : Component
        {
            if (component != null)
            {
                return;
            }

            component = gameObject.GetComponent<T>();
        }
        
        public static T GetComponentIfNull<T>(this T component, GameObject gameObject) where T : Component
        {
            return component != null ? component : gameObject.GetComponent<T>();
        }
        
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();

            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
        
        public static void SetActive(bool isActive, params GameObject[] gameObjects)
        {
            for (var index = 0; index < gameObjects.Length; index++)
            {
                var gameObject = gameObjects[index];

                if (null != gameObject)
                {
                    gameObject.SetActive(isActive);
                }
            }
        }

        public static void SetActive(this IEnumerable<GameObject> gameObjects, bool isActive)
        {
            foreach (var gameObject in gameObjects)
            {
                if (null != gameObject)
                {
                    gameObject.SetActive(isActive);
                }
            }
        }

        public static void SetEnable(bool isEnable, params Behaviour[] monoBehaviours)
        {
            for (var index = 0; index < monoBehaviours.Length; index++)
            {
                var monoBehaviour = monoBehaviours[index];

                if (null != monoBehaviour)
                {
                    monoBehaviour.enabled = isEnable;
                }
            }
        }
        
        public static void SetEnable(this IEnumerable<Behaviour> monoBehaviours, bool isEnable)
        {
            foreach (var monoBehaviour in monoBehaviours)
            {
                if (null != monoBehaviour)
                {
                    monoBehaviour.enabled = isEnable;
                }
            }
        }

        public static void SetActiveNotNull(bool isActive, params GameObject[] gameObjects)
        {
            for (var index = 0; index < gameObjects.Length; index++)
            {
                gameObjects[index].SetActive(isActive);
            }
        }
        
        public static void SetActiveNotNull(this IEnumerable<GameObject> gameObjects, bool isActive)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(isActive);
            }
        }

        public static void SetEnableNotNull(bool isEnable, params Behaviour[] monoBehaviours)
        {
            for (var index = 0; index < monoBehaviours.Length; index++)
            {
                monoBehaviours[index].enabled = isEnable;
            }
        }
		
        public static void SetEnableNotNull(this IEnumerable<Behaviour> monoBehaviours, bool isEnable)
        {
            foreach (var monoBehaviour in monoBehaviours)
            {
                monoBehaviour.enabled = isEnable;
            }
        }

        public static void SetLayerRecursively(this Transform transform, int layer)
        {
            transform.gameObject.layer = layer;

            foreach (Transform child in transform)
            {
                child.SetLayerRecursively(layer);
            }
        }

        public static void SetLayerRecursively(this GameObject obj, int layer)
        {
            SetLayerRecursively(obj.transform, layer);
        }
        
        public static void DestroyChildrenImmediate(this GameObject obj)
        {
            DestroyChildrenImmediate(obj.transform);
        }

        public static void DestroyChildrenImmediate(this Transform transform)
        {
            var children = transform.childCount;

            for (var i = 0; i < children; i++)
            {
                Object.DestroyImmediate(transform.GetChild(0).gameObject);   
            }
        }
        
        public static List<Transform> GetAllChildren(this Transform transform)
        {
            var children = new List<Transform>(transform.childCount);
            
            var i = 0;
            while (i < transform.childCount)
            {
                children.Add(transform.GetChild(i));
                ++i;
            }

            return children;
        }
        
        public static void CopyPositionAndRotationFrom(this Transform transform, Transform source)
        {
            transform.position = source.position;
            transform.rotation = source.rotation;
        }
        
        public static void ResetLocals(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        public static void StripCloneFromName(this GameObject gameObject)
        {
            gameObject.name = gameObject.GetNameWithoutClone();
        }
        
        public static string GetNameWithoutClone(this GameObject gameObject)
        {
            var gameObjectName = gameObject.name;

            var clonePartIndex = gameObjectName.IndexOf("(Clone)", StringComparison.Ordinal);

            if (clonePartIndex == -1)
                return gameObjectName;

            return gameObjectName.Substring(0, clonePartIndex);
        }
        
        public static void SetX(this Transform transform, float x)
        {
            var newPosition = transform.position;
            transform.position = new Vector3(x, newPosition.y, newPosition.z);
        }

        public static void SetY(this Transform transform, float y)
        {
            var newPosition = transform.position;
            transform.position = new Vector3(newPosition.x, y, newPosition.z);
        }

        public static void SetZ(this Transform transform, float z)
        {
            var newPosition = transform.position;
            transform.position = new Vector3(newPosition.x, newPosition.y, z);
        }

        public static void SetXY(this Transform transform, float x, float y)
        {
            var newPosition = new Vector3(x, y, transform.position.z);
            transform.position = newPosition;
        }

        public static void SetXZ(this Transform transform, float x, float z)
        {
            var newPosition = new Vector3(x, transform.position.y, z);
            transform.position = newPosition;
        }

        public static void SetXZ(this Transform transform, Vector3 vector)
        {
            SetXZ(transform, vector.x, vector.z);
        }

        public static void SetYZ(this Transform transform, float y, float z)
        {
            var newPosition = new Vector3(transform.position.x, y, z);
            transform.position = newPosition;
        }

        public static void SetXYZ(this Transform transform, float x, float y, float z)
        {
            var newPosition = new Vector3(x, y, z);
            transform.position = newPosition;
        }

        public static void TranslateX(this Transform transform, float x)
        {
            var offset = new Vector3(x, 0, 0);
            transform.position += offset;
        }

        public static void TranslateY(this Transform transform, float y)
        {
            var offset = new Vector3(0, y, 0);
            transform.position += offset;
        }

        public static void TranslateZ(this Transform transform, float z)
        {
            var offset = new Vector3(0, 0, z);
            transform.position += offset;
        }

        public static void TranslateXY(this Transform transform, float x, float y)
        {
            var offset = new Vector3(x, y, 0);
            transform.position += offset;
        }

        public static void TranslateXZ(this Transform transform, float x, float z)
        {
            var offset = new Vector3(x, 0, z);
            transform.position += offset;
        }

        public static void TranslateYZ(this Transform transform, float y, float z)
        {
            var offset = new Vector3(0, y, z);
            transform.position += offset;
        }

        public static void TranslateXYZ(this Transform transform, float x, float y, float z)
        {
            var offset = new Vector3(x, y, z);
            transform.position += offset;
        }

        public static void SetLocalX(this Transform transform, float x)
        {
            var newLocalPosition = transform.localPosition;
            transform.localPosition = new Vector3(x, newLocalPosition.y, newLocalPosition.z);
        }

        public static void SetLocalY(this Transform transform, float y)
        {
            var newLocalPosition = transform.localPosition;
            transform.localPosition = new Vector3(newLocalPosition.x, y, newLocalPosition.z);
        }

        public static void SetLocalZ(this Transform transform, float z)
        {
            var newLocalPosition = transform.localPosition;
            transform.localPosition = new Vector3(newLocalPosition.x, newLocalPosition.y, z);
        }

        public static void SetLocalXY(this Transform transform, float x, float y)
        {
            var newLocalPosition = new Vector3(x, y, transform.localPosition.z);
            transform.localPosition = newLocalPosition;
        }

        public static void SetLocalXZ(this Transform transform, float x, float z)
        {
            var newLocalPosition = new Vector3(x, transform.localPosition.z, z);
            transform.localPosition = newLocalPosition;
        }

        public static void SetLocalYZ(this Transform transform, float y, float z)
        {
            var newLocalPosition = new Vector3(transform.localPosition.x, y, z);
            transform.localPosition = newLocalPosition;
        }

        public static void SetLocalXYZ(this Transform transform, float x, float y, float z)
        {
            var newLocalPosition = new Vector3(x, y, z);
            transform.localPosition = newLocalPosition;
        }

        public static void SetPosition(this Transform transform, Vector3 position)
        {
            transform.position = position;
        }

        public static void ResetPosition(this Transform transform)
        {
            transform.position = Vector3.zero;
        }

        public static void SetLocalPosition(this Transform transform, Vector3 vector)
        {
            transform.localPosition = vector;
        }
        
        public static void ResetLocalPosition(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }

        public static void SetScaleX(this Transform transform, float x)
        {
            var newLocalScale = transform.localScale;
            transform.localScale = new Vector3(x, newLocalScale.y, newLocalScale.z);
        }

        public static void SetScaleY(this Transform transform, float y)
        {
            var newLocalScale = transform.localScale;
            transform.localScale = new Vector3(newLocalScale.x, y, newLocalScale.z);
        }

        public static void SetScaleZ(this Transform transform, float z)
        {
            var newLocalScale = transform.localScale;
            transform.localScale = new Vector3(newLocalScale.x, newLocalScale.y, z);
        }

        public static void SetScaleXY(this Transform transform, float x, float y)
        {
            var newLocalScale = new Vector3(x, y, transform.localScale.z);
            transform.localScale = newLocalScale;
        }

        public static void SetScaleXZ(this Transform transform, float x, float z)
        {
            var newLocalScale = new Vector3(x, transform.localScale.y, z);
            transform.localScale = newLocalScale;
        }

        public static void SetScaleYZ(this Transform transform, float y, float z)
        {
            var newLocalScale = new Vector3(transform.localScale.x, y, z);
            transform.localScale = newLocalScale;
        }

        public static void SetScaleXYZ(this Transform transform, float x, float y, float z)
        {
            var newLocalScale = new Vector3(x, y, z);
            transform.localScale = newLocalScale;
        }

        public static void ResetScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }

        public static void FlipX(this Transform transform)
        {
            transform.SetScaleX(-transform.localScale.x);
        }

        public static void FlipY(this Transform transform)
        {
            transform.SetScaleY(-transform.localScale.y);
        }

        public static void FlipZ(this Transform transform)
        {
            transform.SetScaleZ(-transform.localScale.z);
        }

        public static void FlipXY(this Transform transform)
        {
            var localScale = transform.localScale;
            transform.SetScaleXY(-localScale.x, -localScale.y);
        }

        public static void FlipXZ(this Transform transform)
        {
            var localScale = transform.localScale;
            transform.SetScaleXZ(-localScale.x, -localScale.z);
        }

        public static void FlipYZ(this Transform transform)
        {
            var localScale = transform.localScale;
            transform.SetScaleYZ(-localScale.y, -localScale.z);
        }

        public static void FlipXYZ(this Transform transform)
        {
            var localScale = transform.localScale;
            transform.SetScaleXYZ(-localScale.z, -localScale.y, -localScale.z);
        }

        public static void FlipPositive(this Transform transform)
        {
            var localScale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(localScale.x), Mathf.Abs(localScale.y), Mathf.Abs(localScale.z));
        }

        public static void RotateAroundX(this Transform transform, float angle)
        {
            var rotation = new Vector3(angle, 0, 0);
            transform.Rotate(rotation);
        }

        public static void RotateAroundY(this Transform transform, float angle)
        {
            var rotation = new Vector3(0, angle, 0);
            transform.Rotate(rotation);
        }

        public static void RotateAroundZ(this Transform transform, float angle)
        {
            var rotation = new Vector3(0, 0, angle);
            transform.Rotate(rotation);
        }

        public static void SetRotationX(this Transform transform, float angle)
        {
            transform.eulerAngles = new Vector3(angle, 0, 0);
        }

        public static void SetRotationY(this Transform transform, float angle)
        {
            transform.eulerAngles = new Vector3(0, angle, 0);
        }

        public static void SetRotationZ(this Transform transform, float angle)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public static void SetLocalRotationX(this Transform transform, float angle)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
        }

        public static void SetLocalRotationY(this Transform transform, float angle)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }

        public static void SetLocalRotationZ(this Transform transform, float angle)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        public static void ResetRotation(this Transform transform)
        {
            transform.rotation = Quaternion.identity;
        }

        public static void ResetLocalRotation(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
        }

        public static void ResetLocal(this Transform transform)
        {
            transform.ResetLocalRotation();
            transform.ResetLocalPosition();
            transform.ResetScale();
        }

        public static void Reset(this Transform transform)
        {
            transform.ResetRotation();
            transform.ResetPosition();
            transform.ResetScale();
        }
        
        public static void OffsetX(this Transform transform, float x)
        {
            transform.position = transform.position.OffsetX(x);
        }
        
        public static void OffsetY(this Transform transform, float y)
        {
            transform.position = transform.position.OffsetY(y);
        }
        
        public static void OffsetZ(this Transform transform, float z)
        {
            transform.position = transform.position.OffsetZ(z);
        }
        
        public static void OffsetXY(this Transform transform, float x, float y)
        {
            transform.position = transform.position.OffsetXY(x, y);
        }
        
        public static void OffsetXZ(this Transform transform, float x, float z)
        {
            transform.position = transform.position.OffsetXZ(x, z);
        }
        
        public static void OffsetYZ(this Transform transform, float y, float z)
        {
            transform.position = transform.position.OffsetYZ(y, z);
        }
        
        public static void ClampX(this Transform transform, float min, float max)
        {
            transform.SetX(Mathf.Clamp(transform.position.x, min, max));
        }
        
        public static void ClampY(this Transform transform, float min, float max)
        {
            transform.SetY(Mathf.Clamp(transform.position.x, min, max));
        }
        
        /// <summary>
        /// Snap position to grid of snapValue
        /// </summary>
        public static void SnapPosition(this Transform transform, float snapValue)
        {
            transform.position = transform.position.SnapValue(snapValue);
        }
        
        // RectTransform 

        public static void SetPositionX(this RectTransform transform, float x)
        {
            transform.anchoredPosition = transform.anchoredPosition.SetX(x);
        }

        public static void SetPositionY(this RectTransform transform, float y)
        {
            transform.anchoredPosition = transform.anchoredPosition.SetY(y);
        }

        public static void OffsetPositionX(this RectTransform transform, float x)
        {
            transform.anchoredPosition = transform.anchoredPosition.OffsetX(x);
        }

        public static void OffsetPositionY(this RectTransform transform, float y)
        {
            transform.anchoredPosition = transform.anchoredPosition.OffsetY(y);
        }
    }
}