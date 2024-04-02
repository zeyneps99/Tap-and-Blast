using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHelper : MonoBehaviour
{
    private Board _board;
    private Level _level;
    private LevelInfo _levelInfo;

    private static string _boardPath = "Prefabs/Board/";
    private Canvas _canvas;

    private Dictionary<ObstacleTypes, int> _goalRemaining;

    public void GenerateLevel(Level level)
    {
        _level = level;
        _goalRemaining = new Dictionary<ObstacleTypes, int>
      {
          { ObstacleTypes.Box, 0 },
          { ObstacleTypes.Vase, 0 },
          { ObstacleTypes.Stone, 0 }
      };
        if (_canvas == null)
        {
            _canvas = FindObjectOfType<Canvas>();
        }
        if (_board == null && _canvas != null)
        {
            _board = Resources.Load<Board>(_boardPath + "Board");
            _board = Instantiate(_board, _canvas.transform);
        }
        if (_board != null)
        {
            _board.Set(level.grid_width, level.grid_height, level.grid);
            //_board.Enable(true);
        }
    }

    public LevelInfo GetLevelInfo()
    {
        return new LevelInfo(GetGoal(_board), _level.move_count);
    }
    private Dictionary<ObstacleTypes, int> GetGoal(Board board)
    {
        List<Obstacle> ObstacleList = board.GetObstacles();
        for (int i = 0; i < ObstacleList.Count; i++)
        {
            ObstacleTypes type = ObstacleList[i].Type;
            if (type != ObstacleTypes.None)
            {
                _goalRemaining[type]++;
            }
        }
        return _goalRemaining;
    }

    public Board GetBoard()
    {
        return _board;
    }
    public bool IsLevelOver()
    {
        return (_level.move_count < 1 || _goalRemaining.Count < 1);
    }

    //TODO
    public void HandleBlast(List<Blastable> list)
    {
        //call event to change move count
        _board.Enable(false);
        Vector2Int pos = Vector2Int.zero;
        int itemsToAnimate = 0;
        foreach (Blastable item in list)
        {
            if (item.TryGetComponent(out IAnimatable animatable))
            {
                pos = _board.GetPositionOfItem(item);
                animatable.Animate(() =>
                {
                    _board.RemoveItem(item);
                    itemsToAnimate++;
                    if (itemsToAnimate == list.Count)
                    {
                        FillEmptySpaces();
                    };
                });
            }
            else
            {
                _board.RemoveItem(item);
                itemsToAnimate++;
                if (itemsToAnimate == list.Count)
                {
                    FillEmptySpaces();
                }
            }

        }
    }

    public void FillEmptySpaces()
    {
        _board.ReplaceItemsAfterBlast();
    }

}
