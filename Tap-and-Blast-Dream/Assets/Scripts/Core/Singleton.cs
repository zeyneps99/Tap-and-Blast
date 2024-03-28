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
                        if (instance == null)
                        {
                            GameObject go = new();
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