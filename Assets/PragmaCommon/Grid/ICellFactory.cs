using UnityEngine;

namespace Pragma.Grid
{
    public interface ICellFactory<TCell>
    {
        public TCell Create(Grid<TCell> grid, Vector2Int coordinate);
    }
}