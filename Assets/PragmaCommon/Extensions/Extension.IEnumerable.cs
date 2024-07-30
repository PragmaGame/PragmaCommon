using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RandomSystem = System.Random;
using RandomUnity = UnityEngine.Random;

namespace Pragma.Common
{
    public static partial class Extension
    {
	    private static readonly RandomSystem random = new RandomSystem();
	    
        /// <summary>
        /// Returns new array with inserted empty element at index
        /// </summary>
        public static T[] InsertAt<T>(this T[] array, int index)
        {
            if (index < 0)
            {
                Debug.LogError("Index is less than zero. Array is not modified");

                return array;
            }

            if (index > array.Length)
            {
                Debug.LogError("Index exceeds array length. Array is not modified");

                return array;
            }

            var newArray = new T[array.Length + 1];
            var index1 = 0;

            for (var index2 = 0; index2 < newArray.Length; ++index2)
            {
                if (index2 == index) continue;

                newArray[index2] = array[index1];
                ++index1;
            }

            return newArray;
        }
        
        /// <summary>
        /// Is array null or empty
        /// </summary>
        public static bool IsNullOrEmpty<T>(this T[] collection) => collection == null || collection.Length == 0;

        /// <summary>
        /// Is list null or empty
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IList<T> collection) => collection == null || collection.Count == 0;

        /// <summary>
        /// Is collection null or empty. IEnumerable is relatively slow. Use Array or List implementation if possible
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || !collection.Any();
        
        /// <summary>
        /// Returns random element from collection
        /// </summary>
        public static T GetRandom<T>(this T[] collection)
        {
            return collection[RandomUnity.Range(0, collection.Length)];
        }

        /// <summary>
        /// Returns random element from collection
        /// </summary>
        public static T GetRandom<T>(this IList<T> collection)
        {
            return collection[RandomUnity.Range(0, collection.Count)];
        }

        /// <summary>
        /// Returns random element from collection
        /// </summary>
        public static T GetRandom<T>(this IEnumerable<T> collection)
        {
            var enumerable = collection as T[] ?? collection.ToArray();

            return enumerable.ElementAt(RandomUnity.Range(0, enumerable.Count()));
        }
        
        /// <summary>
		/// Get next index for circular array. <br />
		/// -1 will result with last element index, Length + 1 is 0. <br />
		/// If step is more that 1, you will get correct offset <br />
		/// 
		/// <code>
		/// Example (infinite loop first->last->first):
		/// i = myArray.NextIndex(i++);
		/// var nextItem = myArray[i];
		/// </code>
		/// </summary>
		public static int NextIndexInCircle<T>(this T[] array, int desiredPosition)
		{
			if (array.IsNullOrEmpty())
			{
				Debug.LogError("NextIndexInCircle Caused: source array is null or empty");

				return -1;
			}

			var length = array.Length;

			if (length == 1) return 0;

			return (desiredPosition % length + length) % length;
		}

		/// <summary>
		/// Adds a key/value pair to the IDictionary&lt;TKey,TValue&gt; if the
		/// key does not already exist. Returns the new value, or the existing
		/// value if the key exists.
		/// </summary>
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> source,
		                                            TKey key,
		                                            TValue value)
		{
			if (!source.ContainsKey(key))
			{
				source[key] = value;
			}

			return source[key];
		}

		/// <summary>
		/// Adds a key/value pair to the IDictionary&lt;TKey,TValue&gt; by using
		/// the specified function if the key does not already exist. Returns
		/// the new value, or the existing value if the key exists.
		/// </summary>
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> source,
		                                            TKey key,
		                                            System.Func<TKey, TValue> valueFactory)
		{
			if (!source.ContainsKey(key))
			{
				source[key] = valueFactory(key);
			}

			return source[key];
		}

		/// <summary>
		/// Adds a key/value pair to the IDictionary&lt;TKey,TValue&gt; by using
		/// the specified function and an argument if the key does not already
		/// exist, or returns the existing value if the key exists.
		/// </summary>
		public static TValue GetOrAdd<TKey, TValue, TArg>(this IDictionary<TKey, TValue> source,
		                                                  TKey key,
		                                                  System.Func<TKey, TArg, TValue> valueFactory,
		                                                  TArg factoryArgument)
		{
			if (!source.ContainsKey(key))
			{
				source[key] = valueFactory(key, factoryArgument);
			}

			return source[key];
		}
		
		public static bool TryFind<T>(this List<T> list, Predicate<T> predicate, out T value)
		{
			value = list.Find(predicate);

			return value != null;
		}

		/// <summary>
		/// First index of an item that matches a predicate.
		/// </summary>
		public static int FirstIndex<T>(this IList<T> source, Predicate<T> predicate)
		{
			for (var i = 0; i < source.Count; ++i)
			{
				if (predicate(source[i]))
				{
					return i;
				}
			}

			return -1;
		}

		/// <summary>
		/// First index of an item that matches a predicate.
		/// </summary>
		public static int FirstIndex<T>(this IEnumerable<T> source, Predicate<T> predicate)
		{
			var index = 0;

			foreach (var e in source)
			{
				if (predicate(e))
				{
					return index;
				}

				++index;
			}

			return -1;
		}

		/// <summary>
		/// Last index of an item that matches a predicate.
		/// </summary>
		public static int LastIndex<T>(this IList<T> source, Predicate<T> predicate)
		{
			for (var i = source.Count - 1; i >= 0; --i)
			{
				if (predicate(source[i]))
				{
					return i;
				}
			}

			return -1;
		}

		/// <summary>
		/// Fills a collection with values generated using a factory function that
		/// passes along their index numbers.
		/// </summary>
		public static IList<T> FillBy<T>(this IList<T> source,
		                                 Func<int, T> valueFactory)
		{
			for (var i = 0; i < source.Count; ++i)
			{
				source[i] = valueFactory(i);
			}

			return source;
		}

		/// <summary>
		/// Fills an array with values generated using a factory function that
		/// passes along their index numbers.
		/// </summary>
		public static T[] FillBy<T>(this T[] source,
		                            Func<int, T> valueFactory)
		{
			for (var i = 0; i < source.Length; ++i)
			{
				source[i] = valueFactory(i);
			}

			return source;
		}

		/// <summary>
		/// Performs an action on each element of a collection.
		/// </summary>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (var element in source)
			{
				action(element);
			}
		}

		/// <summary>
		/// Performs an action on each element of a collection with its index
		/// passed along.
		/// </summary>
		public static void ForEach<T, R>(this IEnumerable<T> source, Func<T, int, R> func)
		{
			var index = 0;

			foreach (var element in source)
			{
				func(element, index);
				++index;
			}
		}

		/// <summary>
		/// Performs a function on each element of a collection.
		/// </summary>
		public static void ForEach<T, R>(this IEnumerable<T> source, Func<T, R> func)
		{
			foreach (var element in source)
			{
				func(element);
			}
		}

		/// <summary>
		/// Performs an action on each element of a collection with its index
		/// passed along.
		/// </summary>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			var index = 0;

			foreach (var element in source)
			{
				action(element, index);
				++index;
			}
		}

		public static void For<T>(this T[] array, Action<T> action)
		{
			// ReSharper disable once ForCanBeConvertedToForeach
			for (var i = 0; i < array.Length; i++)
			{
				action(array[i]);
			}
		}
		
		public static void For<T>(this List<T> array, Action<T> action)
		{
			// ReSharper disable once ForCanBeConvertedToForeach
			for (var i = 0; i < array.Count; i++)
			{
				action(array[i]);
			}
		}

		/// <summary>
        /// Randomise list using System.Random
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list) => list.Shuffle(random);

        /// <summary>
        /// Randomise list using System.Random
        /// </summary>
        /// <param name="list">List to shuffle</param>
        /// <param name="seed">Random seed</param>
        /// <typeparam name="T">List type</typeparam>
        /// <returns>Shuffled list</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list, int seed) => list.Shuffle(new RandomSystem(seed));
        
        private static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list, RandomSystem customRandom)
        {
	        var newList = GetList(list);
	        var n = newList.Count;
	        
            while (n > 1)
            {
                n--;
                var k = customRandom.Next(n + 1);
                (newList[k], newList[n]) = (newList[n], newList[k]);
            }

            return newList;
        }

        /// <summary>
        /// Randomise list using System.Random
        /// </summary>
        /// <param name="list"></param>
        /// <param name="randomiseSelector"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list, Func<T, bool> randomiseSelector)
            where T : class
        {
	        var newList = GetList(list);
	        var countShuffleElements = newList.Count(randomiseSelector);

            if (countShuffleElements <= 1)
            {
                return newList;
            }

            var n = newList.Count;
            while (n > 1)
            {
                n--;

                if (!randomiseSelector(newList[n]))
                {
                    continue;
                }

                var randomiseList = newList.Where(randomiseSelector).Where(x => !x.Equals(newList[n])).ToList();

                var randomValue = newList.IndexOf(randomiseList[random.Next(randomiseList.Count)]);
                (newList[randomValue], newList[n]) = (newList[n], newList[randomValue]);
            }

            return newList;
        }
        
        /// <summary>
        /// Shuffle while all elements will not change their position
        /// </summary>
        /// <param name="list"></param>
        /// <param name="randomiseSelector"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ShuffleUnsafe<T>(this IEnumerable<T> list, Func<T, bool> randomiseSelector)
            where T : class
        {
	        var newList = GetList(list);
	        
            List<T> shuffledList;

            if (newList.Count <= 1)
            {
                return newList;
            }

            if (newList.Count == 2)
            {
                (newList[0], newList[1]) = (newList[1], newList[0]);

                return newList;
            }

            var countCycle = 0;
            
            do
            {
                shuffledList = Shuffle(newList, randomiseSelector).ToList();

                countCycle++;

                if (countCycle > newList.Count * newList.Count)
                {
                    return shuffledList;
                }

            } while (!shuffledList.IsShuffled(newList));

            return shuffledList;
        }

        public static bool IsShuffled<T>(this IEnumerable<T> shuffled, IEnumerable<T> original)
            where T : class
        {
            var shuffledList = GetList(shuffled); 
            var originalList = GetList(original);
            
            if (shuffledList.Count != originalList.Count)
            {
                throw new Exception();
            }

            if (originalList.Count == 1 || originalList.Count == 0)
            {
                return true;
            }

            var originalCount = originalList.Count;
            var sameCount = shuffledList.Where((x, i) => x.Equals(originalList[i])).Count();
            
            return originalCount % 2 == 0 ? sameCount == 0 : (sameCount - 1) <= 0;
        }

        private static IList<T> GetList<T>(this IEnumerable<T> list)
        {
	        return list switch
	        {
		        T[] array => array,
		        IList<T> iList => iList,
		        var _ => new List<T>(list)
	        };
        }   
    }
}