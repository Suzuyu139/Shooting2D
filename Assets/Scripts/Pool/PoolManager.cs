using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    protected ObjectPool<GameObject> _pool;
    //protected ListPool<GameObject> _pool;

    protected GameObject _prefab;

    // Start is called before the first frame update
    void Start()
    {
        _pool = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject OnCreatePooledObject()
    {
        var obj = Instantiate(_prefab);
        obj.transform.parent = this.transform;
        return obj;
    }

    void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        _prefab = prefab;
        GameObject obj = _pool.Get();
        Transform tf = obj.transform;
        tf.position = position;
        tf.rotation = rotation;
        return obj;
    }

    public void ReleaseGameObject(GameObject obj)
    {
        _pool.Release(obj);
    }
}
