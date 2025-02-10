using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    
    private Dictionary<Type, object> pools = new Dictionary<Type, object>();

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    public void CreatePool<T>(T prefab, int size) where T : MonoBehaviour 
    {
        var type = typeof(T);
        if (!pools.ContainsKey(type))
        {
            pools[type] = new GenericObjectPool<T>(prefab, size, transform);
        }
    }

    public T GetObject<T>() where T : MonoBehaviour {
        if (pools.TryGetValue(typeof(T), out object pool))
        {
            return ((GenericObjectPool<T>)pool).GetObject();
        }
        Debug.LogWarning($"No Pool for {typeof(T)}");
        return null;
    }

    public void ReturnObject<T>(T obj) where T : MonoBehaviour {
        if (pools.TryGetValue(typeof(T), out object pool))
        {
            ((GenericObjectPool<T>)pool).ReturnObject(obj);
        }
        else
        {
            Debug.LogWarning($"No Pool for {typeof(T)}");
        }
    }
}

public class GenericObjectPool<T> where T : MonoBehaviour {
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    private Transform parent;

    public GenericObjectPool(T prefab, int size, Transform parent) {
        this.prefab = prefab;
        this.parent = parent;
        for (int i = 0; i < size; i++) {
            T obj = GameObject.Instantiate(prefab);
            ReturnObject(obj);
        }
    }

    public T GetObject() {
        T obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = GameObject.Instantiate(prefab);
        }
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnObject(T obj) {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(parent);
        obj.transform.localScale = Vector3.one;
        pool.Enqueue(obj);
    }
}