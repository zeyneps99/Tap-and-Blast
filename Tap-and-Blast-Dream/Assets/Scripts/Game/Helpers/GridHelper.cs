using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridHelper
{
    private int _width;
    private int _height;
    private GameObject _sampleObject;
    private GridLayoutGroup _layoutGroup;

    private RectTransform _grid;
    private Dictionary<Vector2Int, Vector3> _gridToWorldPos;

    private Vector2 _cellSize;


    public GridHelper(int width, int height, GameObject sampleObject, GridLayoutGroup layoutGroup, RectTransform grid)
    {
        _width = width;
        _height = height;
        _sampleObject = sampleObject;
        _layoutGroup = layoutGroup;
        _grid = grid;
        _gridToWorldPos = new Dictionary<Vector2Int, Vector3>();
    }

    public void SetLayout()
    {
        _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        _layoutGroup.constraintCount = _height;

        if (_sampleObject.TryGetComponent<RectTransform>(out var cubeRT))
        {
            float _itemWidth = cubeRT.rect.width;
            _layoutGroup.cellSize = new Vector2(.75f, .75f) * _itemWidth;
            _cellSize = _layoutGroup.cellSize;
            GenerateGrid(_layoutGroup.cellSize.x);
        }
    }



    private void GenerateGrid(float cellSize)
    {
        if (_grid != null)
        {
            _grid.sizeDelta = new Vector2(_width + .25f, _height + .25f) * cellSize;
        }
    }

    public void AddWorldPosition(Vector2Int boardPosition, Vector3 worldPosition)
    {
        if (_gridToWorldPos != null && !_gridToWorldPos.ContainsKeySafe(boardPosition))
        {
            _gridToWorldPos.Add(boardPosition, worldPosition);
        }
    }

    public Vector3 GetWorldPosition(Vector2Int boardPosition)
    {
        if (_gridToWorldPos != null && _gridToWorldPos.ContainsKeySafe(boardPosition))
        {
            return _gridToWorldPos[boardPosition];
        }
        else
        {
            return Vector3.negativeInfinity;
        }
    }

    public Vector2 GetCellSize()
    {
        return _cellSize;
    }


 





}
