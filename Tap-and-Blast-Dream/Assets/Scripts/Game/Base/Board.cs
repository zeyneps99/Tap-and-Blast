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

    private GridHelper _gridHelper;

    private void Awake()
    {
        _factory = new BoardEntityFactory(transform);
    }

    public void Set(int width, int height, string[] grid)
    {
        Width = width;
        Height = height;
        Items = new BoardEntity[Width, Height];
        SetBoard(grid);
    }



    private void SetBoard(string[] grid)
    {

        BoardEntity[] entityArr = GetGridItems(grid);

        if (_container == null)
        {
            Debug.LogWarning("Container of board is empty"); 
            return;
        }

        if (entityArr == null || entityArr.Length <= 0)
        {
            return;
        }
        IntializeGrid(_container);

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


    private void IntializeGrid(GameObject container)
    {
        if (container.TryGetComponent(out GridLayoutGroup layout))
        {
            var sampleCube = _factory.GetCube(1);
            sampleCube.name = "sample cube";
            _gridHelper = new GridHelper(Width, Height, sampleCube.gameObject, layout, _grid);
            _gridHelper.SetLayout();
            _factory.Return(sampleCube);
        }
    }

    private IEnumerator CoWaitForPosition(BoardEntity entity)
    {
        if (entity == null)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        Vector2Int pos = GetPositionOfItem(entity);
        _gridHelper.AddWorldPosition(pos, entity.transform.position);
        if (pos.x == Width - 1 && pos.y == Height - 1){
          if (_container.TryGetComponent(out GridLayoutGroup layoutGroup)) {
            layoutGroup.enabled = false;
          }
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
        StartCoroutine(MakeFalliblesFall());
        List<Cube> replacementCubes = ReplaceFallibles();
        if (replacementCubes != null&& replacementCubes.Count > 0) {
          for(int i=0; i < replacementCubes.Count; i++) {
            Cube cube = replacementCubes[i];
            Vector3 fallPos = _gridHelper.GetWorldPosition(GetPositionOfItem(cube));
            cube.Fall(fallPos);
          }
        }
    }

    private IEnumerator MakeFalliblesFall()
    {
        int fallCount = 0;
        int completedFallCount = 0;
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
                    fallCount++;
                    fallible.Fall(_gridHelper.GetWorldPosition(destinationPos), () =>
                    {
                        completedFallCount++;
                    });
                }
            }
        }

        yield return new WaitUntil(() => completedFallCount == fallCount);
        
    }

    private List<Cube> ReplaceFallibles()
    {
      List<Cube> newCubes = new List<Cube>();
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                BoardEntity entity = Items[i, j];
                if (entity == null)
                {
                  Cube newCube = _factory.GetCube(Random.Range(1,5));
                  Vector3 topPosition = _gridHelper.GetWorldPosition(new Vector2Int(i,j));
                  
                  newCube.transform.SetParent(_container.transform);
                  newCube.transform.position = topPosition;
                  newCube.transform.localScale = Vector3.one;
                  Vector2 cubeSize = _gridHelper.GetCellSize();
                  if (newCube.TryGetComponent(out RectTransform rt)){
                    rt.sizeDelta = cubeSize;
                    newCube.transform.localPosition += new Vector3(0, rt.rect.height, 0);
                  }
                  newCube.name = "Entity - " + i + " , " + j;
                  Items[i,j] = newCube;
                  newCubes.Add(newCube);
                }
            }
        }
        return newCubes;


    }

    private bool IsIndexOutOfBounds(Vector2Int pos)
    {
        return (pos.x < 0 || pos.x >= Width || pos.y < 0 || pos.y >= Height);
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

    public void Reset() {
        Enable(false);
        foreach(BoardEntity entity in Items) {
            RemoveItem(entity);
        }
    }


}




