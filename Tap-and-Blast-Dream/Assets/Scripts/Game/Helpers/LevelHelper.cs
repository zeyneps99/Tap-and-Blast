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


   public void GenerateLevel(Level level) {
      LevelInfo levelInfo = new();
      _level = level;
      if (_canvas == null) {
         _canvas = FindObjectOfType<Canvas>();
      }
      if (_board == null && _canvas != null) {
         _board = Resources.Load<Board>(_boardPath + "Board");
         _board = Instantiate(_board, _canvas.transform);
      }
      if (_board != null) {
         _board.Set(level.grid_width, level.grid_height, level.grid);
      }
   }

   public LevelInfo GetLevelInfo() {
      return new LevelInfo(GetGoal(_board), _level.move_count);
   }
   private Goal GetGoal(Board board) {
      Goal goal = new Goal();
      List<Obstacle> ObstacleList = board.GetObstacles();
      for(int i = 0; i < ObstacleList.Count; i++) {
         ObstacleTypes type = ObstacleList[i].Type;
         if (type == ObstacleTypes.Vase) {
            goal.Vase++;
         }
         else if (type == ObstacleTypes.Box) {
            goal.Box++;
         }
         else if (type == ObstacleTypes.Stone) {
            goal.Stone++;
         }
      }
      return goal;
   }




//    public int GetLevelInfo() {



//    }



}
