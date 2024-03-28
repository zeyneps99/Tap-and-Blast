using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IGameState
{
    public void Enter()
    {
        SceneManager.Instance.LoadScene((int) SceneTypes.MainScene);
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
