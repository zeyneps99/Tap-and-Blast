using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    private GameObject _objectPrefab;
    private Queue<T> _pool;
    private Transform _parent;
    public const int _amount = 5;


    public ObjectPool(GameObject go, Transform t)
    {
        if (go != null && go.TryGetComponent(out T _))
        {
            _objectPrefab = go;
            _parent = t;
            _pool = new Queue<T>();
        }
    }
    public T Get()
    {
        if (_pool.Count == 0)
        {
            Add(_amount);
        }
        if (_pool.Count > 0)
        {
            var obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning(typeof(T) + " pool empty");
            return null;
        }
    }

    public void Put(T obj)
    {
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

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
