using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SpawnerBase : MonoBehaviour
{
    protected bool _isSpawnerEnd = false;

    public bool IsSpawnerEnd => _isSpawnerEnd;

    protected virtual async UniTask EnemySpawner()
    {
        await UniTask.WaitUntil(() => !SceneLoadManager.Instance.IsFade);
    }
}
