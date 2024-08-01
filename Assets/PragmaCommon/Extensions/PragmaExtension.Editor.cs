#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Pragma.Common
{
    public static partial class PragmaExtension
    {
        public static IEnumerable<T> GetAssets<T>(string t = "ScriptableObject") where T : Object
        {
            var assets = AssetDatabase.FindAssets(typeof(T).Name + " t:" + t);

            if (assets.Length < 1) return default;

            return assets.Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<T>);
        }

        public static List<T> GetAllPublicConstantValues<T>(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }
    }
}

#endif