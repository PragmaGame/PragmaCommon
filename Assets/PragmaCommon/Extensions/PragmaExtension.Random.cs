using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pragma.Common
{
    //Random
    public static partial class PragmaExtension
    {
        /// <summary>
        /// Gets a random Vector2 of length 1 pointing in a random direction.
        /// </summary>
        public static Vector2 RandomOnUnitCircle
        {
            get
            {
                var angle = Random.Range(0f, Mathf.PI * 2);

                return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            }
        }

        /// <summary>
        /// Returns -1 or 1 with equal change.
        /// </summary>
        public static int RandomSign => (Random.value < 0.5f) ? -1 : 1;

        /// <summary>
        /// Returns true or false with equal chance.
        /// </summary>
        public static bool RandomBool => Random.value < 0.5f;
        
        public static string GetShortGuid()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("/", "_");
        }

        public static int GetRandomByProbability(float[] probability)
        {
            float total = 0;

            foreach (var t in probability)
            {
                total += t;
            }

            var randomPoint = Random.value * total;

            for (var i = 0; i < probability.Length; i++)
            {
                if (randomPoint < probability[i])
                {
                    return i;
                }
		    
                randomPoint -= probability[i];
            }

            return probability.Length - 1;
        }
        
        public static Vector3 RandomInsideRectangle(Vector3 center, Vector3 sizeRectangle)
        {
            var halfX = sizeRectangle.x / 2f;
            var halfY = sizeRectangle.y / 2f;
            var halfZ = sizeRectangle.z / 2f;
            
            var x = Random.Range((center.x - (halfX)), (center.x + (halfX)));
            var y = Random.Range((center.y - (halfY)), (center.y + (halfY)));
            var z = Random.Range((center.z - (halfZ)), (center.z + (halfZ)));

            return new Vector3(x, y, z);
        }
        
        public static Vector2 RandomInsideRectangle(Vector2 center, Vector2 sizeRectangle)
        {
            var halfX = sizeRectangle.x / 2f;
            var halfY = sizeRectangle.y / 2f;

            var x = Random.Range((center.x - (halfX)), (center.x + (halfX)));
            var y = Random.Range((center.y - (halfY)), (center.y + (halfY)));

            return new Vector3(x, y);
        }
        
        public static float RandomRange(Vector2 range)
        {
            return Random.Range(range.x, range.y);
        }
        
        [Description("minInclusive | maxExclusive")]
        public static int RandomRange(Vector2Int range)
        {
            return Random.Range(range.x, range.y);
        }
    }
}