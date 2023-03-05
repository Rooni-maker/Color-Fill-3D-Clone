using UnityEngine;
using System.Drawing;
using System.Collections.Generic;

public class GridManager : Singleton<GridManager>
{

    [SerializeField] private int _gridColumns = 10;
    [SerializeField] private int _gridRows    = 10;
    private int _totalCount  = 0;
    private int _trueCount   = 0;
    private float _progress  = 0f;
    [Space]
    bool[,] _grid;

    private const float _percentFalseCount = 0.1f;
    private float _limitToFill = 0f;

    public void InitGrid(int col, int row)
    {
        _gridColumns = col;
        _gridRows    = row;
        _grid        = new bool[_gridColumns, _gridRows];
        _totalCount  = _gridColumns * _gridRows;
        _limitToFill = _totalCount * _percentFalseCount;
    }//InitGrid() end

    public void ChangeValue(float x, float z)
    {
        int col = Mathf.RoundToInt(x + (_gridColumns/2));
        int row = Mathf.Abs(Mathf.RoundToInt(z - (_gridRows/2)));
        _grid[col, row] = true;
        _trueCount++;
    }

    public void ApplyFloodFill()
    {
        if(AudioManager.Instance)
            AudioManager.Instance?.Play("Collect");
        Haptics.Generate(HapticTypes.LightImpact);
        for(int i = 0; i < 2; i++)  
        {                           
            bool[,] gridCopy = new bool[_gridColumns, _gridRows];
            bool[,] gridCopySecond = new bool[_gridColumns, _gridRows];
            gridCopy = (bool[,])_grid.Clone();
            gridCopySecond = (bool[,])_grid.Clone();
            bool[,] gridToPrint = FloodFill(gridCopy, gridCopySecond);
            SetProgressBar(gridToPrint);
            _grid = (bool[,])gridToPrint.Clone();
        }      

        if(_progress >= 1f)
            GameManager.Instance.LevelComplete();
    }

    private void SetProgressBar(bool[,] _grid)
    {
        _trueCount = GetTrueGridCount(_grid);
        _progress = (float)_trueCount / _totalCount;
        UI_Manager.Instance.FillAmount(_progress);
    }

    private int GetTrueGridCount(bool[,] _grid)
    {
        int trueCount = 0;
        for(int col = 0; col < _gridColumns; col++)
        {
            for(int row = 0; row < _gridRows; row++)
            {
                if(_grid[col, row])
                    trueCount++;
            }
        }
        return trueCount;
    }

    private bool[,] FloodFill(bool[,] _grid, bool[,] gridCopySecond)
    {
        Queue<Point> q = new Queue<Point>();       
        int randIndex;
        List<Point> testPointList = new List<Point>();
        bool[,] gridCopy3 = (bool[,])_grid.Clone();
        int falseCountBeforeFill = FindFalseCountPositions(_grid, ref testPointList, gridCopy3);

        if(falseCountBeforeFill < _limitToFill)
        {
            MakeCubes(testPointList);
            return gridCopy3;
        }

        Point randPoint = new Point();
        do
        {
            randIndex = Random.Range(0, testPointList.Count);
            randPoint = testPointList[randIndex];
        } while (_grid[randPoint.X, randPoint.Y]);

        List<Point> firstTryPointList = new List<Point>();
        q.Enqueue(randPoint);
        int trueCount = 0;
        while (q.Count > 0)
        {
            Point n = q.Dequeue();
            if (_grid[n.X, n.Y])
                continue;
            Point w = n, e = new Point(n.X + 1, n.Y);
            while ((w.X >= 0) && !_grid[w.X, w.Y])
            {
                _grid[w.X, w.Y] = true;
                firstTryPointList.Add(new Point(w.X, w.Y));
                trueCount++;
                if ((w.Y > 0) && !_grid[w.X, w.Y - 1])
                    q.Enqueue(new Point(w.X, w.Y - 1));
                if ((w.Y < _gridRows - 1) && !_grid[w.X, w.Y + 1])
                    q.Enqueue(new Point(w.X, w.Y + 1));
                w.X--;
            }
            while ((e.X <= _gridColumns - 1) && !_grid[e.X, e.Y])
            {
                _grid[e.X, e.Y] = true;
                firstTryPointList.Add(new Point(e.X, e.Y));
                trueCount++;
                if ((e.Y > 0) && !_grid[e.X, e.Y - 1])
                    q.Enqueue(new Point(e.X, e.Y - 1));
                if ((e.Y < _gridRows - 1) && !_grid[e.X, e.Y + 1])
                    q.Enqueue(new Point(e.X, e.Y + 1));
                e.X++;
            }
        }
        List<Point> secondTryPointList = new List<Point>();
        int falseCount = FindFalseCountPositions(_grid, ref secondTryPointList, gridCopySecond);

        if(falseCount > trueCount) 
        {
            MakeCubes(firstTryPointList);
            return _grid;
        }

        if(secondTryPointList.Count > 0)
            MakeCubes(secondTryPointList);
        return gridCopySecond;
    }

    private void MakeCubes(List<Point> pointList)
    {
        foreach(Point point in pointList)
        {
            Vector3 repCubePos = FindTransformFromPoint(point);
            Cube cube = CubePool.Instance.GetCube();
            cube.Init(repCubePos, true);
        }
    }

    private Vector3 FindTransformFromPoint(Point point) => new Vector3((float)(point.X - (float)_gridColumns/2f), 0.3f, (float)((float)_gridRows/2f - point.Y));  

    private int FindFalseCountPositions(bool[,] _grid, ref List<Point> pointList, bool[,] gridCopy2) 
    {
        int falseCount = 0;
        for(int col = 0; col < _gridColumns; col++)
        {
            for(int row = 0; row < _gridRows; row++)
            {
                if (_grid[col, row])
                    continue;
                falseCount++;
                gridCopy2[col, row] = true;
                pointList.Add(new Point(col, row));
            }
        }
        return falseCount;
    }
}