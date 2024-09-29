using System;
using UnityEngine;

namespace Pragma.Grid
{
    public class Grid<TCell>
    {
        private Vector2Int _size;
        private ICellFactory<TCell> _cellFactory;
        private TCell[,] _matrix;

        public event Action<Vector2Int, TCell> GridCellChangedEvent;

        public Vector2Int Size => _size;
        public TCell[,] Matrix => _matrix;
        
        public Grid(Vector2Int size, ICellFactory<TCell> cellFactory = null)
        {
            _size = size;
            _cellFactory = cellFactory;
            
            _matrix = new TCell[_size.x, _size.y];

            if (cellFactory == null)
            {
                return;
            }
            
            for (var x = 0; x < _matrix.GetLength(0); x++)
            {
                for (var y = 0; y < _matrix.GetLength(1); y++)
                {
                    _matrix[x, y] = cellFactory.Create(this, new Vector2Int(x, y));
                }
            }
        }

        public void SetCell(Vector2Int coordinate, TCell value)
        {
            if (!IsValidCoordinate(coordinate))
            {
                return;
            }
            
            _matrix[coordinate.x, coordinate.y] = value;
            GridCellChangedEvent?.Invoke(coordinate, value);
        }

        public TCell GetCell(Vector2Int coordinate)
        {
            if(!IsValidCoordinate(coordinate))
            {
                return default;
            }

            return _matrix[coordinate.x, coordinate.y];
        }

        public void SetDirtyCell(Vector2Int coordinate)
        {
            GridCellChangedEvent?.Invoke(coordinate, GetCell(coordinate));
        }

        public bool IsValidCoordinate(Vector2Int coordinate)
        {
            return coordinate.x >= 0 && coordinate.y >= 0 && coordinate.x < _size.x && coordinate.y < _size.y;
        }

        public void TraversalCells(Action<Vector2Int,TCell> action)
        {
            for (var x = 0; x < _matrix.GetLength(0); x++)
            {
                for (var y = 0; y < _matrix.GetLength(1); y++)
                {
                    action.Invoke(new Vector2Int(x, y), _matrix[x, y]);
                }
            }
        }
        
        public void TraversalCells(Func<Vector2Int,TCell, bool> action)
        {
            for (var x = 0; x < _matrix.GetLength(0); x++)
            {
                for (var y = 0; y < _matrix.GetLength(1); y++)
                {
                    var isContinue = action.Invoke(new Vector2Int(x, y), _matrix[x, y]);

                    if (!isContinue)
                    { 
                        return;
                    }
                }
            }
        }
    }
}
