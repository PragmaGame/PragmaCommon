using System.Collections.Generic;
using UnityEngine;

namespace Pragma.Common
{
    public static partial class Extension
    {
        private const float MAX_TIME = 1f;
        
        public static bool PointOnLine(this Vector3 p, Vector3 a, Vector3 b, float t = 1E-07f)
        {
            var zero = (p.y - a.y) * (b.x - a.x) - (p.x - a.x) * (b.y - a.y);
            if (zero > t || zero < -t) return false;

            return true;
        }
        
        public static void GetQuadraticBezierPoints(Vector3 p0, Vector3 p1, Vector3 p2, List<Vector3> points, float delta = 0.05f)
        {
            var time = 0f;

            while (time <= MAX_TIME)
            {
                points.Add(GetQuadraticBezierPoint(p0, p1, p2, time));
                time += delta;
            }
            
            points.Add(GetQuadraticBezierPoint(p0, p1, p2, MAX_TIME));
        }
        
        public static List<Vector3> GetQuadraticBezierPoints(Vector3 p0, Vector3 p1, Vector3 p2, float delta = 0.05f)
        {
            var points = new List<Vector3>();

            GetQuadraticBezierPoints(p0, p1, p2, points, delta);

            return points;
        }
        
        public static Vector3 GetQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            t = Mathf.Clamp01(t);
            var oneMinusT = MAX_TIME - t;

            return
                oneMinusT * oneMinusT * p0 +
                oneMinusT * t * p1 +
                t * t * p2;
        }
        
        public static void GetCubicBezierPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, List<Vector3> points, float delta = 0.05f)
        {
            var time = 0f;

            while (time <= MAX_TIME)
            {
                points.Add(GetCubicBezierPoint(p0, p1, p2, p3, time));
                time += delta;
            }
            
            points.Add(GetCubicBezierPoint(p0, p1, p2, p3, MAX_TIME));
        }
        
        public static void GetCubicBezierPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector3[] points)
        {
            float steps = points.Length - 1;

            for (var i = 0; i <= steps; i++)
            {
                points[i] = GetCubicBezierPoint(p0, p1, p2, p3, i / steps);
            }
        }
        
        public static List<Vector3> GetCubicBezierPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float delta = 0.05f)
        {
            var points = new List<Vector3>();

            GetCubicBezierPoints(p0, p1, p2, p3, points, delta);

            return points;
        }
        
        public static Vector3 GetCubicBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            var oneMinusT = MAX_TIME - t;

            return oneMinusT * oneMinusT * oneMinusT * p0 + 3f * oneMinusT * oneMinusT * t * p1 + 3f * oneMinusT * t * t * p2 + t * t * t * p3;
        }
    }
}