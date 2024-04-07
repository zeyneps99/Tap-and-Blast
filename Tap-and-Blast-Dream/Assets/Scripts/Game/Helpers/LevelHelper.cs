using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public bool IsOutOfMoves()
    {
        return (_level.move_count < 1 && _goalRemaining.Values.Any(value => value > 0));
    }

    public bool IsGoalComplete()
    {

        return (_level.move_count >= 0 && !_goalRemaining.Values.Any(value => value > 0));
    }

    public void UpdateMoves(bool isIncrease)
    {
        _level.move_count += isIncrease ? 1 : -1;
        UIManager.Instance.UpdateMoves(_level.move_count);
    }

    public void UpdateGoal()
    {
        UIManager.Instance.UpdateGoal(_goalRemaining);
    }

    public void HandleBlast(List<BoardEntity> list, Blastable chosenBlastable)
    {
        _board.Enable(false);
        UpdateMoves(false);

        Vector2Int pos = Vector2Int.zero;
        int itemsToAnimate = 0;
        bool spawnTNT = list.Count >= 5 && list.All(item => item is Cube);
        var chosenPosition = _board.GetPositionOfItem(chosenBlastable);
        List<Obstacle> affectedObstacles = new List<Obstacle>();

        foreach (BoardEntity item in list)
        {
            if (item.TryGetComponent(out IAnimatable animatable))
            {
                CheckObstaclesAffected(item, affectedObstacles);

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
                        UpdateGoal();
                    };
                });
            }
            else
            {
                //TODO
                CheckObstaclesAffected(item, affectedObstacles);

                _board.RemoveItem(item);
                itemsToAnimate++;
                if (itemsToAnimate == list.Count)
                {
                    FillEmptySpaces();
                    UpdateGoal();

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


    public void CheckObstaclesAffected(BoardEntity blastedEntity, List<Obstacle> affectedObstacles)
    {
        Vector2Int blastPosition = _board.GetPositionOfItem(blastedEntity);

        List<Vector2Int> neighborPositions = new List<Vector2Int>();

        neighborPositions.Add(blastPosition + Vector2Int.up);
        neighborPositions.Add(blastPosition + Vector2Int.down);
        neighborPositions.Add(blastPosition + Vector2Int.left);
        neighborPositions.Add(blastPosition + Vector2Int.right);

        foreach (Vector2Int position in neighborPositions)
        {
            if (_board.IsIndexOutOfBounds(position))
            {
                continue;
            }
            BoardEntity neighborEntity = _board.Items[position.x, position.y];

            if (neighborEntity != null && neighborEntity.TryGetComponent(out Obstacle obstacle) && !affectedObstacles.Contains(obstacle))
            {
                affectedObstacles.Add(obstacle);

                bool isClear = obstacle.TakeDamage();

                if (isClear)
                {
                    if (obstacle.TryGetComponent(out IAnimatable animatable))
                    {
                        animatable.Animate(() =>
                        {
                            _board.RemoveItem(obstacle);
                            _goalRemaining[obstacle.Type]--;
                        });
                    }
                    else
                    {
                        _board.RemoveItem(obstacle);
                        _goalRemaining[obstacle.Type]--;
                    }
                }
            }

        }
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
