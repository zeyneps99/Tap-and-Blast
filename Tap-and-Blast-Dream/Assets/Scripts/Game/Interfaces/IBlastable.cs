using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IBlastable
{
   public abstract void Blast();
   public abstract bool CanBlastNeighbor(Blastable neighbor);
}
