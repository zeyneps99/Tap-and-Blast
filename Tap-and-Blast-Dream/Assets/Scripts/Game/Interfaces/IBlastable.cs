using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IBlastable
{
   public abstract void TryBlast();
   public abstract bool CanBlastNeighbor(Blastable neighbor);
}
