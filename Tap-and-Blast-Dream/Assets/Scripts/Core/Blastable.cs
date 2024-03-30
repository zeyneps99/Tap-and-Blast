using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastable : BoardEntity, IBlastable
{
    public void Blast()
    {
        throw new System.NotImplementedException();
    }

    public virtual bool CanBlastNeighbor(Blastable neighbor)
    {
        return false;
    }
}
