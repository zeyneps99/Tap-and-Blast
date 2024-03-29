using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHelper : MonoBehaviour
{
   public Board Board;
   public Goal Goal;
   private Level _level;
   private static string _boardPath = "Prefabs/Board/";
   private Canvas _canvas;


   public void SetLevel(Level level) {
    _level = level;
   }

   public void GenerateBoardForLevel(Level level) {

      if (_canvas == null) {
         _canvas = FindObjectOfType<Canvas>();
      }
      if (Board == null && _canvas != null) {
         Board = Resources.Load<Board>(_boardPath + "Board");
         if (Board != null) {
            Board = Instantiate(Board, _canvas.transform);
         }
      }
         Board?.Set(level.grid_width, level.grid_height, level.grid);

      
   }

    



//    public int GetLevelInfo() {



//    }



}
