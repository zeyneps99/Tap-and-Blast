using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private LevelInfo _levelInfo;
    [SerializeField] private TextMeshProUGUI _moveCount;
    public void Init(LevelInfo levelInfo) {
        _levelInfo = levelInfo;
        SetMoves(levelInfo.MoveCount);
    }

    private void SetMoves(int moveCount) {
        if(_moveCount != null) {
            _moveCount.text = moveCount.ToString();
        }
    }

   
}
