using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pragma.Common
{
    [Serializable]
    public abstract class SerializedHashSet<T> : ISerializationCallbackReceiver, ISet<T>,
                                                 IReadOnlyCollection<T>
    {
        [SerializeField, HideInInspector] private List<T> values  = new List<T>();
        
        protected HashSet<T> hashSet = new HashSet<T>();

        void ISerializationCallbackReceiver.OnAfterDeserialize() => Deserialize();
        void ISerializationCallbackReceiver.OnBeforeSerialize() => Serialize();

        protected SerializedHashSet() {}
        
        protected SerializedHashSet(HashSet<T> hashSet)
        {
            this.hashSet = hashSet;
        }

        private void Deserialize()
        {
            Clear();
            
            foreach (var val in values)
            {
                Add(val);
            }
        }

        private void Serialize()
        {
            values.Clear();
            
            foreach (var val in this)
            {
                values.Add(val);
            }
        }
        
        
        /// <summary>
        /// Implementations IReadOnlyCollection and ISet
        /// </summary>
        public int Count => hashSet.Count;
        
        public bool IsReadOnly => false;
        
        bool ISet<T>.Add(T item) => hashSet.Add(item);
        
        bool ICollection<T>.Remove(T item) => hashSet.Remove(item);
        
        public void ExceptWith(IEnumerable<T> other) => hashSet.ExceptWith(other);
        
        public void IntersectWith(IEnumerable<T> other) => hashSet.IntersectWith(other);
        
        public bool IsProperSubsetOf(IEnumerable<T> other) => hashSet.IsProperSubsetOf(other);
        
        public bool IsProperSupersetOf(IEnumerable<T> other) => hashSet.IsProperSupersetOf(other);
        
        public bool IsSubsetOf(IEnumerable<T> other) => hashSet.IsSubsetOf(other);
        
        public bool IsSupersetOf(IEnumerable<T> other) => hashSet.IsSupersetOf(other);
        
        public bool Overlaps(IEnumerable<T> other) => hashSet.Overlaps(other);
        
        public bool SetEquals(IEnumerable<T> other) => hashSet.SetEquals(other);
        
        public void SymmetricExceptWith(IEnumerable<T> other) => hashSet.SymmetricExceptWith(other);
        
        public void UnionWith(IEnumerable<T> other) => hashSet.UnionWith(other);
        
        public void Add(T item) => hashSet.Add(item);
        
        public void Clear() => hashSet.Clear();
        
        public bool Contains(T item) => hashSet.Contains(item);
        
        public void CopyTo(T[] array, int arrayIndex) => hashSet.CopyTo(array, arrayIndex);
        
        public IEnumerator<T> GetEnumerator() => hashSet.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}