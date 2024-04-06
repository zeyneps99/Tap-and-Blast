using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEntityFactory 
{
    private Transform _parent;
    private ObjectPool<Cube> _cubePool;
    private ObjectPool<TNT> _TNTPool;
   private ObjectPool<Box> _boxPool;
    private ObjectPool<Vase> _vasePool;
    private ObjectPool<Stone> _stonePool;

    private GameObject _cubePrefab;
    private GameObject _TNTprefab;
    private GameObject _boxPrefab;
    private GameObject _vasePrefab;
    private GameObject _stonePrefab;

    private static string _prefabPath = "Prefabs/BoardEntity/";
    
    public BoardEntityFactory(Transform parent){
        _parent = parent;
        GetPrefabs();
        InitializePools();
    }
    
   private void GetPrefabs() {

       Cube cube = Resources.Load<Cube>(_prefabPath + "Cube");
       if (cube != null) {
        _cubePrefab = cube.gameObject;
       }
       TNT tnt = Resources.Load<TNT>(_prefabPath + "TNT");
       if (tnt != null) {
        _TNTprefab = tnt.gameObject;
       }

       Box box = Resources.Load<Box>(_prefabPath + "Box");
       if(box != null) {
        _boxPrefab = box.gameObject;
       }

       Vase vase = Resources.Load<Vase>(_prefabPath + "Vase");
       if (vase != null){
        _vasePrefab = vase.gameObject;
       }

       Stone stone = Resources.Load<Stone>(_prefabPath + "Stone");
       if (stone != null) {
        _stonePrefab = stone.gameObject;
       }
   }

   private void InitializePools() {
    _cubePool = new ObjectPool<Cube>(_cubePrefab, _parent);
    _TNTPool = new ObjectPool<TNT>(_TNTprefab, _parent);   
    _boxPool = new ObjectPool<Box>(_boxPrefab, _parent);
    _vasePool = new ObjectPool<Vase>(_vasePrefab, _parent);
    _stonePool = new ObjectPool<Stone>(_stonePrefab, _parent);

   }

    public Cube GetCube(int cubeType) {
        Cube cube = _cubePool.Get();
        if (cube != null) {
            cube.SetType(cubeType);
        }
        return cube;
    }

    public TNT GetTNT() {
        TNT tnt = _TNTPool.Get();
        return tnt;
    }

    public Stone GetStone() {
        Stone stone = _stonePool.Get();
        if (stone != null) {
            stone.SetType((int) ObstacleTypes.Stone);
        }
        return stone;
    }
    public Box GetBox() {
        Box box = _boxPool.Get();  
        if (box != null) {
            box.SetType((int) ObstacleTypes.Box);
        }
        return box;
    }

   public Vase GetVase() {
        Vase vase = _vasePool.Get();   
        if (vase != null) {
            vase.SetType((int) ObstacleTypes.Vase);
        }
        return vase;
    }

    public void Return(BoardEntity entityToReturn) {
        if (entityToReturn.TryGetComponent(out Cube cube) ) {
            _cubePool.Put(cube);
            cube.transform.SetParent(_parent);
        }
        else if(entityToReturn.TryGetComponent(out TNT tnt) ) {
            _TNTPool.Put(tnt);
            tnt.transform.SetParent(_parent);

        }
        else if(entityToReturn.TryGetComponent(out Box box) ) {
            _boxPool.Put(box);
            box.transform.SetParent(_parent);

        }       
        else if(entityToReturn.TryGetComponent(out Vase vase) ) {
            _vasePool.Put(vase);
            vase.transform.SetParent(_parent);

        }        
        else if(entityToReturn.TryGetComponent(out Stone stone) ) {
            _stonePool.Put(stone);
            stone.transform.SetParent(_parent);
        }

        entityToReturn.transform.localPosition = Vector3.zero;
        entityToReturn.transform.rotation = Quaternion.identity;
        entityToReturn.transform.localScale = Vector3.one;
        entityToReturn.name = "";
        
    }

}
