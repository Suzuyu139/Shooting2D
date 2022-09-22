using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class BulletPool : PoolManager
{
    public BulletController GetBulletController(Vector3 position, Quaternion rotation)
    {
        var obj = _pool.Get();
        Transform tf = obj.transform;
        tf.position = position;
        tf.rotation = rotation;
        return obj.GetComponent<BulletController>();
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
