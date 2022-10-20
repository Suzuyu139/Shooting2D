using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject[] _initObjects;

    public bool IsInitialized = false;

    private void Awake()
    {
        Initilize().Forget();
    }

    private async UniTask Initilize()
    {
        for (int i = 0; i < _initObjects.Length; i++)
        {
            var obj = Instantiate(_initObjects[i]);
            DontDestroyOnLoad(obj);
        }

        await UniTask.WaitUntil(() => MasterManager.Instance != null && SceneLoadManager.Instance != null);
        IsInitialized = true;
    }
}
