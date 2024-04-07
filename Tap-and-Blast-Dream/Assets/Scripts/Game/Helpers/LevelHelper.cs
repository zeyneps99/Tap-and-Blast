using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHelper : MonoBehaviour
{
    private Board _board;
    private Board _boardPrefab;
    private Level _level;

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
        if (_boardPrefab == null)
        {
            _boardPrefab = Resources.Load<Board>(_boardPath + "Board");
        }
        if (_board == null && _canvas != null)
        {
            _board = Instantiate(_boardPrefab, _canvas.transform);
        }
        if (_board != null)
        {
            _board.gameObject.SetActive(true);
            _board.Set(level.grid_width, level.grid_height, level.grid);
            _board.Enable(true);
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

    public void UpdateMoves(bool isIncrease)
    {
        _level.move_count += isIncrease ? 1 : -1;
        UIManager.Instance.UpdateMoves(_level.move_count);
    }

    public void HandleBlast(List<BoardEntity> list, Blastable chosenBlastable)
    {
        _board.Enable(false);
        UpdateMoves(false);
        Vector2Int pos = Vector2Int.zero;
        int itemsToAnimate = 0;
        bool spawnTNT = list.Count >= 5 && list.All(item => item is Cube);
        var chosenPosition = _board.GetPositionOfItem(chosenBlastable);

        foreach (BoardEntity item in list)
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
                        if (spawnTNT)
                        {
                            _board.SpawnTNT(chosenPosition);
                        }
                        FillEmptySpaces();
                    };
                });
            }
            else
            {
                //TODO
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
        _board.MakeFalliblesFall();
        _board.ReplaceItemsAfterBlast();
        _board.Enable(true);
    }

    public void EndLevel()
    {
        if (_board != null)
        {
            _board.Enable(false);
            _board.Reset();
            _board.gameObject.SetActive(false);
        }
    }

}
