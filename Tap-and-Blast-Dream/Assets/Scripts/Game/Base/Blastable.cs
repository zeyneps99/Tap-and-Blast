using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class Blastable : BoardEntity, IBlastable
{
    public void Blast()
    {
        Enable(false);
        if(TryGetComponent(out LayoutElement lo)) {
            lo.ignoreLayout = true;
        }
      
    }

    public virtual bool CanBlastNeighbor(Blastable neighbor)
    {
        return false;
    }

    public void Enable(bool isEnable) {
        GetComponent<EventTrigger>().enabled = isEnable;
    }
}
