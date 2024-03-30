using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : Blastable, IAnimatable, IFallible
{
    public void Animate()
    {
        throw new System.NotImplementedException();
    }

    public void Blast()
    {
        throw new System.NotImplementedException();
    }

    public void Fall()
    {
        throw new System.NotImplementedException();
    }

    // TODO
    public override bool CanBlastNeighbor(Blastable neighbor)
    {
        return (neighbor.TryGetComponent(out TNT _) || neighbor.TryGetComponent(out Cube _));
    }
}
