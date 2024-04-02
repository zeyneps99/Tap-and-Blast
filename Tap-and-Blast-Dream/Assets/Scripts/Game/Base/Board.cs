using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Board : MonoBehaviour
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    [SerializeField] private GameObject _container;
    [SerializeField] private RectTransform _grid;

    public BoardEntity[,] Items;
    private BoardEntityFactory _factory;

    private bool _isEnabled = false;

    private Vector2 _cellSize;

    private Dictionary<Vector2Int, Vector2> _gridToWorldPos;


    private void Awake()
    {
        _factory = new BoardEntityFactory(transform);
    }

    public void Set(int width, int height, string[] grid)
    {
        Width = width;
        Height = height;
        Items = new BoardEntity[Width, Height];
        _gridToWorldPos = new Dictionary<Vector2Int, Vector2>();
        SetBoard(grid);
    }

    private void SetBoard(string[] grid)
    {

        BoardEntity[] entityArr = GetGridItems(grid);

        if (_container == null)
        {
            Debug.LogWarning("Container of board is empty")
; return;
        }

        if (entityArr == null || entityArr.Length <= 0)
        {
            return;
        }
        SetGridLayout(_container);

        int count = 0;

        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                if (count < entityArr.Length)
                {
                    BoardEntity entity = entityArr[count];
                    entity.name = "Entity - " + i + " , " + j;
                    entity.transform.SetParent(_container.transform, true);
                    entity.transform.localScale = Vector2.one;
                    StartCoroutine(CoWaitForPosition(entity));
                    Items[i, j] = entity;
                    count++;
                }
                else
                {
                    Debug.LogWarning("Error in SetBoard: count excedes grid length");
                }

            }
        }

    }


    private IEnumerator CoWaitForPosition(BoardEntity entity)
    {
        if (entity == null)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        _gridToWorldPos.Add(GetPositionOfItem(entity), entity.transform.position);
    }

    private void SetGridLayout(GameObject container)
    {
        if (container.TryGetComponent<GridLayoutGroup>(out var layout))
        {
            layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            layout.constraintCount = Height;

            var sampleCube = _factory.GetCube(1);
            sampleCube.name = "sampleCube";
            if (sampleCube != null && sampleCube.gameObject != null)
            {
                if (sampleCube.TryGetComponent<RectTransform>(out var cubeRT))
                {
                    float _itemWidth = cubeRT.rect.width;
                    _cellSize = new Vector2(.75f, .75f) * _itemWidth;
                    layout.cellSize = _cellSize;
                    GenerateGrid(layout.cellSize.x);
                }
            }
            _factory.Return(sampleCube);
        }
    }



    private void GenerateGrid(float cellSize)
    {
        if (_grid != null)
        {
            _grid.sizeDelta = new Vector2(Width + .25f, Height + .25f) * cellSize;
        }
    }

    private BoardEntity[] GetGridItems(string[] grid)
    {

        BoardEntity[] arr = new BoardEntity[grid.Length];

        if (grid == null || grid.Length == 0)
        {
            return arr;
        }
        for (int i = 0; i < grid.Length; i++)
        {
            BoardEntity entity = StringToEntity(grid[i]);
            if (entity != null)
            {
                arr[i] = entity;
            }
        }
        return arr;
    }

    private BoardEntity StringToEntity(string str)
    {

        if (_factory == null)
        {
            Debug.LogWarning("Factory is null");
            return null;
        }
        if (str.Equals("r"))
        {
            return _factory.GetCube((int)CubeTypes.Red);
        }
        if (str.Equals("y"))
        {
            return _factory.GetCube((int)CubeTypes.Yellow);
        }
        if (str.Equals("b"))
        {
            return _factory.GetCube((int)CubeTypes.Blue);
        }
        if (str.Equals("g"))
        {
            return _factory.GetCube((int)CubeTypes.Green);
        }
        if (str.Equals("rand"))
        {
            return _factory.GetCube(Random.Range(1, 5));
        }
        if (str.Equals("tnt"))
        {
            return _factory.GetTNT();
        }
        if (str.Equals("bo"))
        {
            return _factory.GetBox();
        }
        if (str.Equals("s"))
        {
            return _factory.GetStone();
        }
        if (str.Equals("v"))
        {
            return _factory.GetVase();
        }
        else
        {
            return _factory.GetCube(Random.Range(1, 5));
        }

    }

    public List<Obstacle> GetObstacles()
    {

        List<Obstacle> obstacles = new List<Obstacle>();
        foreach (BoardEntity entity in Items)
        {
            if (entity.TryGetComponent(out Obstacle obstacle))
            {
                obstacles.Add(obstacle);
            }
        }
        return obstacles;
    }

    public void Enable(bool isEnable)
    {
        _isEnabled = isEnable;
        foreach (BoardEntity item in Items)
        {
            if (item != null && item.TryGetComponent(out Blastable blastable))
            {
                blastable.Enable(isEnable);
            }
        }
    }

    public bool IsEnabled()
    {
        return _isEnabled;
    }

    public void RemoveItem(BoardEntity entity)
    {
        if (entity == null)
        {
            return;
        }
        Vector2Int pos = GetPositionOfItem(entity);
        if (Items[pos.x, pos.y] == entity)
        {
            Items[pos.x, pos.y] = null;
            entity.Clear();
            _factory.Return(entity);

        }
    }

    public void ReplaceItemsAfterBlast()
    {
      MakeFalliblesFall();
    }


    private void MakeFalliblesFall()
    {
        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                BoardEntity entity = Items[i, j];
                if (entity == null)
                {
                    continue;
                }
                if (entity.TryGetComponent(out IFallible fallible))
                {
                    Vector2Int bottomNeighborPos = new Vector2Int(i, j - 1);
                    while (!IsIndexOutOfBounds(bottomNeighborPos)
                    && Items[bottomNeighborPos.x, bottomNeighborPos.y] == null)
                    {
                        bottomNeighborPos = new Vector2Int(bottomNeighborPos.x, bottomNeighborPos.y - 1);
                    }
                    Vector2Int destinationPos = new Vector2Int(bottomNeighborPos.x, bottomNeighborPos.y + 1);
                    Items[i, j] = null;
                    Items[destinationPos.x, destinationPos.y] = entity;
                    entity.name = "Entity - " + destinationPos.x + " , " + destinationPos.y;
                    fallible.Fall(GetWorldPosition(destinationPos));
                }
            }
        }
    }

    private void ReplaceFallibles()
    {

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                BoardEntity entity = Items[i, j];

            }
        }

    }


    private bool IsIndexOutOfBounds(Vector2Int pos)
    {
        return (pos.x < 0 || pos.x >= Width || pos.y < 0 || pos.y >= Height);
    }


    public Vector2 GetWorldPosition(Vector2Int boardPos)
    {
        if (_gridToWorldPos.ContainsKeySafe(boardPos))
        {
            return _gridToWorldPos[boardPos];
        }
        else
        {
            return Vector2.negativeInfinity;
        }
    }

    public Vector2Int GetPositionOfItem(BoardEntity entity)
    {

        if (Items == null || Items.Length == 0)
        {
            return Vector2Int.down;
        }
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                BoardEntity item = Items[i, j];
                if (item != null && item == entity)
                {
                    return new Vector2Int(i, j);
                }
            }
        }
        return Vector2Int.down;
    }


}




