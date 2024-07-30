using System.Collections.Generic;
using UnityEngine;

namespace Pragma.Common
{
    public static partial class Extension
    {
	    public static Vector2 ToVector2XZ(this Vector3 vector3)
		{
			return new Vector2(vector3.x, vector3.z);
		}
		
		public static Vector2 ToVector2XY(this Vector3 vector3)
		{
			return new Vector2(vector3.x, vector3.y);
		}
        
		public static Vector3 ToVector3XZ(this Vector2 vector3, float y = 0f)
		{
			return new Vector3(vector3.x, y,vector3.y);
		}

		public static Vector3 SetX(this Vector3 vector, float x)
		{
			return new Vector3(x, vector.y, vector.z);
		}

		public static Vector2 SetX(this Vector2 vector, float x)
		{
			return new Vector2(x, vector.y);
		}

		public static Vector3 SetY(this Vector3 vector, float y)
		{
			return new Vector3(vector.x, y, vector.z);
		}

		public static Vector2 SetY(this Vector2 vector, float y)
		{
			return new Vector2(vector.x, y);
		}

		public static Vector3 SetZ(this Vector3 vector, float z)
		{
			return new Vector3(vector.x, vector.y, z);
		}

		public static Vector3 SetXY(this Vector3 vector, float x, float y)
		{
			return new Vector3(x, y, vector.z);
		}

		public static Vector3 SetXZ(this Vector3 vector, float x, float z)
		{
			return new Vector3(x, vector.y, z);
		}

		public static Vector3 SetYZ(this Vector3 vector, float y, float z)
		{
			return new Vector3(vector.x, y, z);
		}

		public static Vector3 Offset(this Vector3 vector, Vector2 offset)
		{
			return new Vector3(vector.x + offset.x, vector.y + offset.y, vector.z);
		}

		public static Vector3 OffsetX(this Vector3 vector, float x)
		{
			return new Vector3(vector.x + x, vector.y, vector.z);
		}

		public static Vector2 OffsetX(this Vector2 vector, float x)
		{
			return new Vector2(vector.x + x, vector.y);
		}

		public static Vector2 OffsetY(this Vector2 vector, float y)
		{
			return new Vector2(vector.x, vector.y + y);
		}

		public static Vector3 OffsetY(this Vector3 vector, float y)
		{
			return new Vector3(vector.x, vector.y + y, vector.z);
		}

		public static Vector3 OffsetZ(this Vector3 vector, float z)
		{
			return new Vector3(vector.x, vector.y, vector.z + z);
		}

		public static Vector3 OffsetXY(this Vector3 vector, float x, float y)
		{
			return new Vector3(vector.x + x, vector.y + y, vector.z);
		}

		public static Vector2 OffsetXY(this Vector2 vector, float x, float y)
		{
			return new Vector2(vector.x + x, vector.y + y);
		}

		public static Vector3 OffsetXZ(this Vector3 vector, float x, float z)
		{
			return new Vector3(vector.x + x, vector.y, vector.z + z);
		}

		public static Vector3 OffsetYZ(this Vector3 vector, float y, float z)
		{
			return new Vector3(vector.x, vector.y + y, vector.z + z);
		}

		public static Vector3 ClampX(this Vector3 vector, float min, float max)
		{
			return vector.SetX(Mathf.Clamp(vector.x, min, max));
		}

		public static Vector2 ClampX(this Vector2 vector, float min, float max)
		{
			return vector.SetX(Mathf.Clamp(vector.x, min, max));
		}

		public static Vector3 ClampY(this Vector3 vector, float min, float max)
		{
			return vector.SetY(Mathf.Clamp(vector.x, min, max));
		}

		public static Vector2 ClampY(this Vector2 vector, float min, float max)
		{
			return vector.SetY(Mathf.Clamp(vector.x, min, max));
		}

		public static Vector2 InvertX(this Vector2 vector)
		{
			return new Vector2(-vector.x, vector.y);
		}

		public static Vector2 InvertY(this Vector2 vector)
		{
			return new Vector2(vector.x, -vector.y);
		}
		
		public static Vector3 InvertX(this Vector3 vector)
		{
			return new Vector3(-vector.x, vector.y, vector.z);
		}

		/// <summary>
		/// Snap to grid of snapValue
		/// </summary>
		public static Vector3 SnapValue(this Vector3 val, float snapValue)
		{
			return new Vector3(val.x.Snap(snapValue),
			                   val.y.Snap(snapValue),
			                   val.z.Snap(snapValue));
		}

		/// <summary>
		/// Snap to grid of snapValue
		/// </summary>
		public static Vector2 SnapValue(this Vector2 val, float snapValue)
		{
			return new Vector2(val.x.Snap(snapValue),
			                   val.y.Snap(snapValue));
		}

		/// <summary>
		/// Snap to one unit grid
		/// </summary>
		public static Vector2 SnapToOne(this Vector2 vector)
		{
			return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
		}

		/// <summary>
		/// Snap to one unit grid
		/// </summary>
		public static Vector3 SnapToOne(this Vector3 vector)
		{
			return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
		}

		public static Vector3 AverageVector(this Vector3[] vectors)
		{
			if (vectors.IsNullOrEmpty())
			{
				return Vector3.zero;
			}

			float x = 0f, y = 0f, z = 0f;

			for (var i = 0; i < vectors.Length; i++)
			{
				x += vectors[i].x;
				y += vectors[i].y;
				z += vectors[i].z;
			}

			return new Vector3(x / vectors.Length, y / vectors.Length, z / vectors.Length);
		}

		public static Vector2 AverageVector(this Vector2[] vectors)
		{
			if (vectors.IsNullOrEmpty()) return Vector2.zero;

			float x = 0f, y = 0f;

			for (var i = 0; i < vectors.Length; i++)
			{
				x += vectors[i].x;
				y += vectors[i].y;
			}

			return new Vector2(x / vectors.Length, y / vectors.Length);
		}

		/// <summary>
		/// Finds the position closest to the given one.
		/// </summary>
		/// <param name="position">World position.</param>
		/// <param name="otherPositions">Other world positions.</param>
		/// <returns>Closest position.</returns>
		public static Vector3 GetClosest(this Vector3 position, IEnumerable<Vector3> otherPositions)
		{
			var closest = Vector3.zero;
			var shortestDistance = Mathf.Infinity;

			foreach (var otherPosition in otherPositions)
			{
				var distance = (position - otherPosition).sqrMagnitude;

				if (distance < shortestDistance)
				{
					closest = otherPosition;
					shortestDistance = distance;
				}
			}

			return closest;
		}

		public static Vector3 GetClosest(this IEnumerable<Vector3> positions, Vector3 position)
		{
			return position.GetClosest(positions);
		}
		
		public static bool ApproximatelyVector2(Vector2 value, float epsilon)
        {
            return Mathf.Approximately(value.x, epsilon) && Mathf.Approximately(value.y, epsilon);
        }

        public static Vector2 LerpMultiEqualWeight(List<Vector2> points, float time)
        {
            time = Mathf.Clamp01(time);

            if(points.Count == 1)
            {
                return points[0];
            }
            
            if(Mathf.Approximately(time, 0f))
            {
                return points[0];
            }
   
            if(Mathf.Approximately(time, 1f))
            {
                return points[^1];
            }

            var pointsTime = time * (points.Count - 1);
            var currentPoint = (int)Mathf.Floor(pointsTime);
            pointsTime -= currentPoint;
            
            return Vector2.Lerp(points[currentPoint], points[currentPoint + 1], pointsTime);
        }
        
        public static Vector3 LerpMulti(List<Vector2> points, float time)
        {
            time = Mathf.Clamp01(time);

            if(points.Count == 1)
            {
                return points[0];
            }
            
            if(Mathf.Approximately(time, 0f))
            {
                return points[0];
            }
   
            if(Mathf.Approximately(time, 1f))
            {
                return points[^1];
            }
   
            var distances = new float[points.Count - 1];
            float total = 0;
            
            for(var i = 0; i < points.Count - 1; i++)
            {
                distances[i] = Vector2.Distance(points[i], points[i + 1]);
                total += distances[i];
            }
   
            var current = total * time;
            
            var p = 0;
            
            while(current - distances[p] > 0)
            {
                current -= distances[p++];
            }

            if (distances[p] == 0)
            {
                return points[p];
            }
   
            return Vector3.Lerp(points[p], points[p + 1], current / distances[p]);
        }
    }
}