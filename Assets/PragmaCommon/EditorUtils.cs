using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Pragma.Common
{
    public static class EditorUtils
    {
        public static IEnumerable<T> GetAssets<T>() where T : Object
        {
            var assets = AssetDatabase.FindAssets(" t: " + typeof(T).Name);

            if (assets.Length < 1 || EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                return Array.Empty<T>();
            }

            return assets.Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<T>);
        }
    }
}