using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System.Linq;
using Cysharp.Threading.Tasks;
using System;

public class PoolManager : MonoBehaviour
{
    protected Dictionary<int, ObjectPool<GameObject>> _poolDictionary = new Dictionary<int, ObjectPool<GameObject>>();

    protected GameObject _prefab;

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

    public T GetGameObject<T>(GameObject prefab, int id, Vector3 position, Quaternion rotation)
    {
        _prefab = prefab;
        var pool = _poolDictionary.Where(x => x.Key == id).Select(x => x.Value).FirstOrDefault();
        if (pool == null)
        {
            _poolDictionary.Add(id, new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject));
            pool = _poolDictionary.Where(x => x.Key == id).Select(x => x.Value).First();
        }
        GameObject obj = pool.Get();
        Transform tf = obj.transform;
        tf.position = position;
        tf.rotation = rotation;
        return obj.GetComponent<T>();
    }

    public void ReleaseGameObject(int id, GameObject obj)
    {
        var pool = _poolDictionary.Where(x => x.Key == id).Select(x => x.Value).FirstOrDefault();
        if(pool == null)
        {
            Debug.LogError($"ÉvÅ[ÉãÇéÊìæÇ≈Ç´Ç‹ÇπÇÒÇ≈ÇµÇΩÅB\nid : {id}\nObject : {obj}");
            return;
        }
        pool.Release(obj);
    }

    public void ReleaseTimer(int id, GameObject obj, float timer)
    {
        ReleaseAsync(id, obj, timer).Forget();
    }

    private async UniTask ReleaseAsync(int id, GameObject obj, float timer)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(timer));
        ReleaseGameObject(id, obj);
    }
}
