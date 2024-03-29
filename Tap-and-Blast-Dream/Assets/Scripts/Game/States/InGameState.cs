using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : IGameState
{
    public void Enter()
    {
        GameManager.Instance.StartGame();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}
