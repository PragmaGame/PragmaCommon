using UnityEngine;

namespace Pragma.Common
{
    public static partial class Extension
    {
        /// <summary>
        /// Returns the value corresponding to the key in the preference file if it exists.
        /// If it doesn't exist, it will return defaultValue.
        /// (Internally, the value is stored as an int with either 0 or 1.)
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value if none is given.</param>
        /// <returns>The value corresponding to key in the preference file if it exists, else the default value.</returns>
        public static bool PlayerPrefsGetBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }

        /// <summary>
        /// Sets the value of the preference entry identified by the key.
        /// (Internally, the value is stored as an int with either 0 or 1.)
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value to set the preference entry to.</param>
        public static void PlayerPrefsSetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
    }
}