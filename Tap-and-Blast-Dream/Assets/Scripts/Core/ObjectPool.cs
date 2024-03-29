using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    private GameObject _objectPrefab;
    private Queue<T> _pool = new Queue<T>();
    private Transform _parent;
    public const int _amount = 10;


    public ObjectPool(GameObject go, Transform t)
    {
        if (go != null && go.TryGetComponent(out T _))
        {
            _objectPrefab = go;
            _parent = t;
        }
    }
    public T Get()
    {
        if (_pool.Count == 0)
        {
            Add(_amount);
        }
        var a = _pool.Dequeue();
        a.gameObject.SetActive(true);
        return a;
    }

    public void Put(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    public void Add(int count)
    {
        if (_objectPrefab == null)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(_objectPrefab, _parent);
            go.SetActive(false);
            _pool.Enqueue(go.GetComponent<T>());
        }
    }
}
