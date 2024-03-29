using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : BoardEntity, IAnimatable, IFallible, IBlastable
{
    public CubeTypes Type {private set; get;}

    public void SetType(int cubeType) {

        Type = (CubeTypes) cubeType;

    }

    //TODO: set sprite according to type
    private void SetSprite() {

    }


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


}
