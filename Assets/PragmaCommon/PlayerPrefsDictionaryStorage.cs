using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pragma.Common
{
    public class PlayerPrefsDictionaryStorage<T>
    {
        private readonly string _keysPrefix;
		private readonly string _storageKey;

		private readonly List<string> _existingKeys;
		private readonly Dictionary<string, RxField<T>> _tempDictionary;

		public PlayerPrefsDictionaryStorage(string keysPrefix, string storageKey)
		{
			_keysPrefix = keysPrefix;
			_storageKey = storageKey;

			var keysStorageValue = PlayerPrefs.GetString(GetKeyWithPrefix(_storageKey));

			_existingKeys = new List<string>();
			_tempDictionary = new Dictionary<string, RxField<T>>();

			if (string.IsNullOrEmpty(keysStorageValue)) return;

			_existingKeys = keysStorageValue.Split(',').ToList();
			_tempDictionary = _existingKeys.ToDictionary(GetKeyWithPrefix, o => new RxField<T> { Value = Get(o) });
		}

		public RxField<T> GetRx(string key) => _tempDictionary[GetKeyWithPrefix(key)];

		public T Get(string key, T defaultValue = default)
		{
			var withPrefix = GetKeyWithPrefix(key);

			if (_tempDictionary.ContainsKey(withPrefix))
				return _tempDictionary[withPrefix].Value;

			var stringValue = PlayerPrefs.GetString(withPrefix, string.Empty);

			try
			{
				if (!string.IsNullOrEmpty(stringValue))
					return JsonUtility.FromJson<T>(stringValue);

				return defaultValue;
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			return default;
		}

		public void Save(string key, T data)
		{
			var withPrefix = GetKeyWithPrefix(key);

			if (!_tempDictionary.ContainsKey(withPrefix))
				_tempDictionary.Add(withPrefix, new RxField<T>());

			_tempDictionary[withPrefix].Value = data;

			PlayerPrefs.SetString(withPrefix, JsonUtility.ToJson(data));

			TryAddKey(key);
		}

		public void Delete(string key)
		{
			var withPrefix = GetKeyWithPrefix(key);
			_tempDictionary.Remove(withPrefix);
			PlayerPrefs.DeleteKey(withPrefix);
			TryRemoveKey(key);
		}

		public bool Has(string key)
		{
			var withPrefix = GetKeyWithPrefix(key);

			return PlayerPrefs.HasKey(withPrefix);
		}

		public Dictionary<string, T> ToDictionary() => _tempDictionary.ToDictionary(o => GetKeyWithoutPrefix(o.Key), o => o.Value.Value);

		public List<T> ToList() => _tempDictionary.Select(kpv => kpv.Value.Value).ToList();

		private string GetKeyWithPrefix(string key) => _keysPrefix + "/" + key;
        private string GetKeyWithoutPrefix(string key) => key.Replace(_keysPrefix + "/", string.Empty);

		private void TryAddKey(string key)
		{
			if (_existingKeys.Contains(key)) return;

			_existingKeys.Add(key);
			UpdateKeys();
		}

		private void TryRemoveKey(string key)
		{
			if (!_existingKeys.Contains(key)) return;

			_existingKeys.Remove(key);
			UpdateKeys();
		}

		private void UpdateKeys()
		{
			PlayerPrefs.SetString(GetKeyWithPrefix(_storageKey), string.Join(",", _existingKeys));
		}
    }
}