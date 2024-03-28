using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IGameState
{
    public void Enter()
    {
        GameLogic.Instance.StartMainScene();
    }

    public void Exit()
    {
        //throw new System.NotImplementedException();
    }

    public void Update()
    {
        //throw new System.NotImplementedException();
    }
}
