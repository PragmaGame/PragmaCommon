using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Pragma.Common
{
    public class LerpByPath
    {
        private List<Vector2> _path;
        private float[] _distances;
        private float _totalDistance;

        public IReadOnlyList<Vector2> Path => _path;

        public LerpByPath(IEnumerable<Vector2> path)
        {
            SetPath(path);
        }

        public void SetPath(IEnumerable<Vector2> path)
        {
            _path = path.ToList();
                
            RecalculateDistances();
        }

        public Vector2 Evaluate(float time)
        {
            var currentTimeDistance = _totalDistance * time;
            
            var currentPathIndex = 0;
            
            while(currentTimeDistance - _distances[currentPathIndex] > 0)
            {
                currentTimeDistance -= _distances[currentPathIndex++];
            }

            if (Mathf.Approximately(_distances[currentPathIndex], 0f))
            {
                return _path[currentPathIndex];
            }
   
            return Vector2.Lerp(_path[currentPathIndex], _path[currentPathIndex + 1], currentTimeDistance / _distances[currentPathIndex]);
        }
            
        private void RecalculateDistances()
        {
            _distances = new float[_path.Count - 1];

            for(var i = 0; i < _path.Count - 1; i++)
            {
                _distances[i] = Vector2.Distance(_path[i], _path[i + 1]);
                _totalDistance += _distances[i];
            }
        }
    }
}