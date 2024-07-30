using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pragma.Common
{
    public static partial class Extension
    {
        /// <summary>
        /// Maps a value from [sourceFrom..sourceTo] to [targetFrom..targetTo] with clamping.
        /// 
        /// This is basically Mathf.Lerp(targetFrom, targetTo, Mathf.InverseLerp(sourceFrom, sourceTo, sourceValue)).
        /// </summary>
        /// <param name="sourceValue">The value in the range of [sourceFrom..sourceTo]. Will be clamped if not in that range.</param>
        /// <param name="sourceFrom">The lower end of the source range.</param>
        /// <param name="sourceTo">The higher end of the source range.</param>
        /// <param name="targetFrom">The lower end of the target range.</param>
        /// <param name="targetTo">The higher end of the target range.</param>
        /// <returns>The mapped value.</returns>
        public static float MapClamped(float sourceValue, float sourceFrom, float sourceTo, float targetFrom,
            float targetTo)
        {
            var sourceRange = sourceTo - sourceFrom;
            var targetRange = targetTo - targetFrom;
            var percent = Mathf.Clamp01((sourceValue - sourceFrom) / sourceRange);
            return targetFrom + targetRange * percent;
        }

        /// <summary>
        /// Applies a deadzone [-deadzone..deadzone] in which the value will be set to 0.
        /// </summary>
        /// <param name="value">The joystick value.</param>
        /// <param name="deadzone">A value between for which all results [-deadzone..deadzone] will be set to 0.</param>
        /// <param name="fullRangeBetweenDeadzoneAndOne">If this is true, the values between [-1..-deadzone] and [deadzone..1] will be mapped to [-1..0] and [0..1] respectively.</param>
        /// <returns>The result value between [-1..1].</returns>
        public static float ApplyJoystickDeadzone(float value, float deadzone,
            bool fullRangeBetweenDeadzoneAndOne = false)
        {
            if (Mathf.Abs(value) <= deadzone)
                return 0;

            if (fullRangeBetweenDeadzoneAndOne && (deadzone > 0f))
            {
                if (value < 0)
                {
                    return MapClamped(value, -1f, -deadzone, -1f, 0f);
                }
                else
                {
                    return MapClamped(value, deadzone, 1f, 0f, 1f);
                }
            }

            return value;
        }

        /// <summary>
        /// Maps a joystick input from [sourceFrom..sourceTo] to [-1..1] with clamping.
        /// Applies a deadzone [-deadzone..deadzone] in which the value will be set to 0.
        /// </summary>
        /// <param name="sourceValue">The value in the range of [sourceFrom..sourceTo]. Will be clamped if not in that range.</param>
        /// <param name="sourceFrom">The lower end of the source range.</param>
        /// <param name="sourceTo">The higher end of the source range.</param>
        /// <param name="deadzone">A value between 0 and 1 for which all results [-deadzone..deadzone] will be set to 0.</param>
        /// <param name="fullRangeBetweenDeadzoneAndOne">If this is true, the values between [-1..-deadzone] and [deadzone..1] will be mapped to [-1..0] and [0..1] respectively.</param>
        /// <returns>The result value between [-1..1].</returns>
        public static float MapClampedJoystick(float sourceValue, float sourceFrom, float sourceTo, float deadzone = 0f,
            bool fullRangeBetweenDeadzoneAndOne = false)
        {
            var percent = MapClamped(sourceValue, sourceFrom, sourceTo, -1, 1);

            if (deadzone > 0)
                percent = ApplyJoystickDeadzone(percent, deadzone, fullRangeBetweenDeadzoneAndOne);

            return percent;
        }
        
        /// <summary>
        /// Returns the closer center between two angles.
        /// </summary>
        /// <param name="angle1">The first angle.</param>
        /// <param name="angle2">The second angle.</param>
        /// <returns>The closer center.</returns>
        public static float GetCenterAngleDeg(float angle1, float angle2)
        {
            return angle1 + Mathf.DeltaAngle(angle1, angle2) / 2f;
        }

        /// <summary>
        /// Normalizes an angle between 0 (inclusive) and 360 (exclusive).
        /// </summary>
        /// <param name="angle">The input angle.</param>
        /// <returns>The result angle.</returns>
        public static float NormalizeAngleDeg360(float angle)
        {
            while (angle < 0)
                angle += 360;

            if (angle >= 360)
                angle %= 360;

            return angle;
        }

        /// <summary>
        /// Normalizes an angle between -180 (inclusive) and 180 (exclusive).
        /// </summary>
        /// <param name="angle">The input angle.</param>
        /// <returns>The result angle.</returns>
        public static float NormalizeAngleDeg180(float angle)
        {
            while (angle < -180)
                angle += 360;

            while (angle >= 180)
                angle -= 360;

            return angle;
        }
        
        /// <summary>
        /// Provides a framerate-independent t for lerping towards a target.
        /// 
        /// Example:
        /// 
        ///     currentValue = Mathf.Lerp(currentValue, 1f, MathHelper.EasedLerpFactor(0.75f);
        /// 
        /// will cover 75% of the remaining distance between currentValue and 1 each second.
        /// 
        /// There are essentially two ways of lerping a value over time: linear (constant speed) or
        /// eased (e.g. getting slower the closer you are to the target, see http://easings.net.)
        /// 
        /// For linear lerping (and most of the easing functions), you need to track the start and end
        /// positions and the time that elapsed.
        /// 
        /// Calling something like
        /// 
        ///     currentValue = Mathf.Lerp(currentValue, 1f, 0.95f);
        /// 
        /// every frame provides an easy way of eased lerping without tracking elapsed time or the
        /// starting value, but since it's called every frame, the actual traversed distance per
        /// second changes the higher the framerate is.
        /// 
        /// This function replaces the lerp T to make it framerate-independent and easier to estimate.
        /// 
        /// For more info, see https://www.scirra.com/blog/ashley/17/using-lerp-with-delta-time.
        /// </summary>
        /// <param name="factor">How much % the lerp should cover per second.</param>
        /// <param name="deltaTime">How much time passed since the last call.</param>
        /// <returns>The framerate-independent lerp t.</returns>
        public static float EasedLerpFactor(float factor, float deltaTime = 0f)
        {
            if (deltaTime == 0f)
                deltaTime = Time.deltaTime;

            return 1 - Mathf.Pow(1 - factor, deltaTime);
        }

        /// <summary>
        /// Framerate-independent eased lerping to a target value, slowing down the closer it is.
        /// 
        /// If you call
        /// 
        ///     currentValue = MathHelper.EasedLerp(currentValue, 1f, 0.75f);
        /// 
        /// each frame (e.g. in Update()), starting with a currentValue of 0, then after 1 second
        /// it will be approximately 0.75 - which is 75% of the way between 0 and 1.
        /// 
        /// Adjusting the target or the percentPerSecond between calls is also possible.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="target">The target value.</param>
        /// <param name="percentPerSecond">How much of the distance between current and target should be covered per second?</param>
        /// <param name="deltaTime">How much time passed since the last call.</param>
        /// <returns>The interpolated value from current to target.</returns>
        public static float EasedLerp(float current, float target, float percentPerSecond, float deltaTime = 0f)
        {
            var t = EasedLerpFactor(percentPerSecond, deltaTime);
            return Mathf.Lerp(current, target, t);
        }
        
        /// <summary>
        /// Swap two reference values
        /// </summary>
        public static void Swap<T>(ref T a, ref T b)
        {
            (a, b) = (b, a);
        }

        /// <summary>
        /// Snap to grid of "round" size
        /// </summary>
        public static float Snap(this float val, float round)
        {
            return round * Mathf.Round(val / round);
        }

        /// <summary>
        /// Shortcut for Mathf.Approximately
        /// </summary>
        public static bool Approximately(this float value, float compare)
        {
            return Mathf.Approximately(value, compare);
        }

        /// <summary>
        /// Clamp value to less than min or more than max
        /// </summary>
        public static float NotInRange(this float num, float min, float max)
        {
            if (min > max)
            {
                (min, max) = (max, min);
            }

            if (num < min || num > max)
            {
                return num;
            }

            var mid = (max - min) * 0.5f;

            if (num > min)
            {
                return num + mid < max ? min : max;
            }

            return num - mid > min ? max : min;
        }

        /// <summary>
        /// Clamp value to less than min or more than max
        /// </summary>
        public static int NotInRange(this int num, int min, int max)
        {
            return (int) ((float) num).NotInRange(min, max);
        }

        /// <summary>
        /// Return point A or B, closest to num
        /// </summary>
        public static float ClosestPoint(this float num, float pointA, float pointB)
        {
            if (pointA > pointB)
            {
                (pointA, pointB) = (pointB, pointA);
            }

            var middle = (pointB - pointA) * 0.5f;
            var withOffset = num.NotInRange(pointA, pointB) + middle;
			
            return (withOffset >= pointB) ? pointB : pointA;
        }

        /// <summary>
        /// Check if pointA closer to num than pointB
        /// </summary>
        public static bool ClosestPointIsA(this float num, float pointA, float pointB)
        {
            var closestPoint = num.ClosestPoint(pointA, pointB);
            return Mathf.Approximately(closestPoint, pointA);
        }
        
        public static void BubbleSort<T>(IList<T> arr, Func<T, T, bool> compare)
        {
            if (arr == null || arr.Count == 0 || arr.Count == 1)
            {
                return;
            }
            
            for (var i = 0; i < arr.Count; i++)
            {
                for (var j = 0; j < arr.Count - 1; j++)
                {
                    if (compare(arr[j], arr[j + 1]))
                    {
                        (arr[j + 1], arr[j]) = (arr[j], arr[j + 1]);
                    }
                }
            }
        }
        
        public static int FindNearestPoint(this Vector2[] points, Vector2 anchor)
        {
            var minDistance = float.MaxValue;
            var result = -1;

            for (var i = 0; i < points.Length; i++)
            {
                var distance = Vector2.Distance(points[0], anchor);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = i;
                }
            }
            
            return result;
        }
        
        /// <summary>
        /// Checks if a point is in a 2D triangle. "s" is point, and triangle is "a,b,c" returns true if the point is inside the triangle.
        /// </summary>
        public static bool IsPointInsideTriangle(Vector2 s, Vector2 a, Vector2 b, Vector2 c)
        {
            var asX = s.x - a.x;
            var asY = s.y - a.y;

            var sAb = (b.x-a.x)*asY-(b.y-a.y)*asX > 0;

            if ((c.x - a.x) * asY - (c.y - a.y) * asX > 0 == sAb)
            {
                return false;
            }

            if ((c.x - b.x) * (s.y - b.y) - (c.y - b.y) * (s.x - b.x) > 0 != sAb)
            {
                return false;
            }

            return true;
        }
    }
}