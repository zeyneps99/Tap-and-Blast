using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    private static readonly object padlock = new object();

    protected Singleton() { }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                         T[] instances = FindObjectsOfType<T>();

                        // If there are other instances besides this one, destroy them
                        if (instances.Length > 1)
                        {
                            for (int i = 1; i < instances.Length; i++)
                            {
                                Destroy(instances[i].gameObject);
                            }
                        }
                        if (instance == null)
                        {
                            GameObject go = new GameObject();
                            instance = go.AddComponent<T>();
                            go.name = typeof(T).Name;
                            DontDestroyOnLoad(instance.gameObject);
                        }
                      
                    }
                }
            }
            return instance;
        }
    }


}