using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _numberText;
    [SerializeField] Image _image;

    private static string _imagePath = "Sprites/Obstacles/";

    public ObstacleTypes Type {private set; get;}
    
    public void Set(int type, int count) {
        Type = (ObstacleTypes) type;
        SetImage(Type);
        if (_numberText != null) {
            _numberText.text = count.ToString();
        }
    }

    private void SetImage(ObstacleTypes type) {

        string pathSuffix = "";
        switch(type) {
            case ObstacleTypes.Box:
                pathSuffix = "Box/box";
                break;
            case ObstacleTypes.Stone:
                pathSuffix = "Stone/stone";
                break;
            case ObstacleTypes.Vase:
                pathSuffix = "Vase/vase_01";
                break;
            default:
                pathSuffix = "Box/box";
                break;
        }

        Sprite sprite = Resources.Load<Sprite>(_imagePath + pathSuffix);
        if (sprite != null && _image != null) {
            _image.sprite = sprite;
        }

    }
    
}
