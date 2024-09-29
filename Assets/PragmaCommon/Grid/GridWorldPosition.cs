using System;
using UnityEngine;

namespace Pragma.Grid
{
    public class GridWorldPosition<TCell> : Grid<TCell>
    {
        private float _cellSize;
        private Vector3 _anchor;

        public float CellSize => _cellSize;
        
        public GridWorldPosition(float cellSize, Vector3 anchor, Vector2Int size, ICellFactory<TCell> cellFactory = null)
            : base(size, cellFactory)
        {
            _cellSize = cellSize;
            _anchor = anchor;
        }

        public Vector3 GetCellWorldPosition(Vector2Int coordinate)
        {
            return new Vector3(coordinate.x, coordinate.y) * _cellSize + _anchor;
        }

        public Vector2Int GetCellCoordinate(Vector3 position)
        {
            position -= _anchor;
            
            var x = Mathf.FloorToInt(position.x / _cellSize);
            var y = Mathf.FloorToInt(position.y / _cellSize);

            return new Vector2Int(x, y);
        }
        
        public void SetCell(Vector3 worldPosition, TCell value)
        {
            var xy = GetCellCoordinate(worldPosition);

            SetCell(xy, value);
        }
        
        public TCell GetCell(Vector3 worldPosition)
        {
            var xy = GetCellCoordinate(worldPosition);

            return GetCell(xy);
        }
    }
}