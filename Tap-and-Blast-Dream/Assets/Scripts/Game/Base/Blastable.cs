using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class Blastable : BoardEntity, IBlastable
{
    public ParticleSystem Particles {get; set;}

    public void TryBlast()
    {
        TapCommand tapCommand = new TapCommand(this);
        _commander.ExecuteCommand(tapCommand);
    }

    public virtual bool CanBlastNeighbor(Blastable neighbor)
    {
        return false;
    }

    public void Enable(bool isEnable) {
        GetComponent<EventTrigger>().enabled = isEnable;
    }

}
    
