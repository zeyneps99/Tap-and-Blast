using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Cube : BoardEntity, IAnimatable, IFallible, IBlastable
{
    public CubeTypes Type {private set; get;}  
    private Image _image; 

    private string _spritePath = "Sprites/Cube/DefaultState/";
    
    public void SetType(int cubeType) {
        Type = (CubeTypes) cubeType;
        SetSprite(cubeType);
    }

    private void SetSprite(int type) {
        if ((CubeTypes) type!= CubeTypes.None) {
            Sprite sprite = Resources.Load<Sprite>(_spritePath + GetSpriteName(type));
            _image = GetComponent<Image>();
            if (sprite != null && _image != null) {
                _image.sprite = sprite;
            }
        }
    }

    private string GetSpriteName(int type) {
        switch((CubeTypes) type) {
            case CubeTypes.Yellow:
                return "yellow";
            case CubeTypes.Red:
                return "red";
            case CubeTypes.Blue:
                return "blue";
            case CubeTypes.Green:
                return "green";
            default:
                return "yellow";    
        }
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
