using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEntityFactory 
{
    private Transform _parent;
    private ObjectPool<Cube> _cubePool;
    private ObjectPool<TNT> _TNTPool;

    private GameObject _cubePrefab;
    private GameObject _TNTprefab;

    private static string _prefabPath = "Prefabs/BoardEntity/";
    
    public BoardEntityFactory(Transform parent){
        _parent = parent;
        GetPrefabs();
        InitializePools();
    }
    
   private void GetPrefabs() {

       Cube cube = Resources.Load<Cube>(_prefabPath + "Cube");
       Debug.Log("cube " + cube);
       if (cube != null) {
        _cubePrefab = cube.gameObject;
       }
       TNT tnt = Resources.Load<TNT>(_prefabPath + "TNT");
       if (tnt != null) {
        _TNTprefab = tnt.gameObject;
       }
   }

   private void InitializePools() {
    _cubePool = new ObjectPool<Cube>(_cubePrefab, _parent);
    _TNTPool = new ObjectPool<TNT>(_TNTprefab, _parent);
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

    public void Return(BoardEntity entityToReturn) {
        if (entityToReturn.TryGetComponent(out Cube cube)) {
            _cubePool.Put(cube);
        }

        else if(entityToReturn.TryGetComponent(out TNT tnt)) {
            _TNTPool.Put(tnt);
        }
    }

}
