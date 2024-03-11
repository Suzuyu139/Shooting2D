using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance { get; private set; }

    [SerializeField] PoolPresenter _bulletPool;
    public PoolPresenter BulletPool => _bulletPool;

    public bool IsInitialized { get; private set; } = false;

    private void Start()
    {
        Instance = this;

        Initialize().Forget();
    }

    async UniTask Initialize()
    {
        Cursor.lockState = CursorLockMode.Confined;

        IsInitialized = true;
        GameSceneManager.Instance.SetIsLoadComplete(true);

        await UniTask.CompletedTask;
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
