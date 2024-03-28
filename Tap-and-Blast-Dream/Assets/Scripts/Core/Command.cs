using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command 
{
    protected IEntity _entity;

    public abstract void Execute();
    public abstract void Undo();

    public Command (IEntity entity)
    {
        _entity = entity;
    }

   


}

