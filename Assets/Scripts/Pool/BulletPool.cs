using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;

public class BulletPool : PoolManager
{
    public T GetBulletComponent<T>(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        _prefab = prefab;
        var obj = _pool.Get();
        Transform tf = obj.transform;
        tf.position = position;
        tf.rotation = rotation;
        return obj.GetComponent<T>();
    }

    public void Release(GameObject obj, float time)
    {
        ReleaseTask(obj, time).Forget();
    }

    private async UniTask ReleaseTask(GameObject obj, float time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));

        ReleaseGameObject(obj);
    }
}
