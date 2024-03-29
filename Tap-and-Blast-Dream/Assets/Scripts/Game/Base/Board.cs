using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public int Width {get; private set;}
    public int Height {get; private set;}

    [SerializeField] private GameObject _container;

    private BoardEntity[,] _items;
    private BoardEntityFactory _factory;


    private void Start() {
        _factory = new BoardEntityFactory(transform);
    }
   public void Set(int width, int height, string[] grid) {
    Width = width;
    Height = height;
    _items = new BoardEntity[Width, Height];
    SetBoard(grid);
   }

    private void SetBoard(string[] grid) {

        BoardEntity[] entityArr = GetGridItems(grid);
        
      if (_container == null || entityArr == null)
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
                    entity.name =  "Entity - " + i + " , " + j;
                    entity.transform.SetParent(_container.transform);
                    entity.transform.localScale = Vector2.one;
                    entity.Position = new Vector2Int(i, j);
                    _items[i, j] = entity;
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
            if (sampleCube != null && sampleCube.gameObject != null) {
                if (sampleCube.TryGetComponent<RectTransform>(out var cubeRT))
                {
                    float _itemWidth = cubeRT.rect.width;
                    layout.cellSize = Vector2.one * _itemWidth;
                }
            }
        }
    }

    private BoardEntity[] GetGridItems(string[] grid) {

      if (grid == null || grid.Length == 0) {
        return null;
      }
        BoardEntity[] arr = new BoardEntity[grid.Length];
        // for(int i = 0; i < grid.Length; i++) {

        //    BoardEntity entity = StringToEntity(grid[i]);
        //    if (entity != null) {
        //     arr[i] = entity;
        //    }
        // }
        return arr;
    }

    private BoardEntity StringToEntity(string str) {

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
      else {
        return null;
      }
        
    }


    }

    


