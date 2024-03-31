using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public int Width {get; private set;}
    public int Height {get; private set;}

    [SerializeField] private GameObject _container;
    [SerializeField] private RectTransform _grid;

    public BoardEntity[,] Items;
    private BoardEntityFactory _factory;

    private bool _isEnabled = false;


    private void Awake() {
      _factory = new BoardEntityFactory(transform);
    }

   public void Set(int width, int height, string[] grid) {
    Width = width;
    Height = height;
    Items = new BoardEntity[Width, Height];
    SetBoard(grid);
   }

    private void SetBoard(string[] grid) {

        BoardEntity[] entityArr = GetGridItems(grid);
        
      if (_container == null)
        {
            Debug.LogWarning("Container of board is empty")
;           return;
        }

      if(entityArr == null || entityArr.Length <= 0) {
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
                    entity.name =  "Entity - " + i + " , " + j;
                    entity.transform.SetParent(_container.transform);
                    entity.transform.localScale = Vector2.one;
                    entity.Set(new Vector2Int(i, j), this);
                    Items[i, j] = entity;
                    count++;
                } else
                {
                    Debug.LogWarning("Error in SetBoard: count excedes grid length");
                }
               
            }

        }}

    private void SetGridLayout(GameObject container)
    {
        if (container.TryGetComponent<GridLayoutGroup>(out var layout))
        {
            layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            layout.constraintCount = Height;

            var sampleCube =_factory.GetCube(1);
            sampleCube.name = "sampleCube";
            if (sampleCube != null && sampleCube.gameObject != null) {
                if (sampleCube.TryGetComponent<RectTransform>(out var cubeRT))
                {
                    float _itemWidth = cubeRT.rect.width;
                    layout.cellSize = new Vector2(.75f, .75f)* _itemWidth;
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

    private BoardEntity[] GetGridItems(string[] grid) {
      
      BoardEntity[] arr = new BoardEntity[grid.Length];

      if (grid == null || grid.Length == 0) {
        return arr;
      }
        for(int i = 0; i < grid.Length; i++) {
           BoardEntity entity = StringToEntity(grid[i]);
           if (entity != null) {
            arr[i] = entity;
           }
        }
        return arr;
    }

    private BoardEntity StringToEntity(string str) {

      if (_factory == null) {
        Debug.LogWarning("Factory is null");
        return null;
      }
      if(str.Equals("r")) {
        return _factory.GetCube((int) CubeTypes.Red);
      }   
      if(str.Equals("y")) {
        return _factory.GetCube((int) CubeTypes.Yellow);
      }      
      if(str.Equals("b")) {
        return _factory.GetCube((int) CubeTypes.Blue);
      }      
      if(str.Equals("g")) {
        return _factory.GetCube((int) CubeTypes.Green);
      }
      if(str.Equals("rand")) {
        return _factory.GetCube(Random.Range(1, 5));
      }
      if(str.Equals("tnt")) {
        return _factory.GetTNT();
      }
      if (str.Equals("bo")) {
        return _factory.GetBox();
      }
      if(str.Equals("s")){
        return _factory.GetStone();
      }
      if(str.Equals("v")){
        return _factory.GetVase();
      }
      else {
        return _factory.GetCube(Random.Range(1, 5));
      }
        
    }

    public List<Obstacle> GetObstacles() {

      List<Obstacle> obstacles = new List<Obstacle>();
      foreach(BoardEntity entity in Items) {
        if (entity.TryGetComponent(out Obstacle obstacle)) {
          obstacles.Add(obstacle);
        }
      }
      return obstacles;
    }

    public void Enable(bool isEnable) {
      if (isEnable != IsEnabled()) {
      _isEnabled = isEnable;
      foreach(BoardEntity item in Items) {
        if (item!= null && item.TryGetComponent(out Blastable blastable)) {
          blastable.Enable(isEnable);
        }
      }
       if (_container != null &&_container.TryGetComponent<GridLayoutGroup>(out var layout))
        {
          layout.enabled = isEnable;
        }
      }
    }

    public bool IsEnabled() {
      return _isEnabled;
    }

    public void RemoveItem(BoardEntity entity) {
      if (entity == null) {
        return;
      }
      if (entity.GetBoard() == this) {
        Vector2Int pos = entity.GetPosition();
        if(Items[pos.x, pos.y] == entity) {
          Items[pos.x, pos.y] = null;
          entity.Clear();
          _factory.Return(entity);
        }
      }
    }

  

    

    }

    


