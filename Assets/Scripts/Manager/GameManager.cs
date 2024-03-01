using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsInitialized { get; private set; } = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Instance = this;

        Initialize().Forget();
    }

    async UniTask Initialize()
    {
        Application.targetFrameRate = 60;

        await UniTask.WaitUntil(() =>
        {
            if (MasterDataManager.Instance == null)
            {
                return false;
            }

            if (!MasterDataManager.Instance.IsInitialized)
            {
                return false;
            }

            return true;
        });

        IsInitialized = true;

        await UniTask.CompletedTask;
    }

    private void OnDestroy()
    {
        AddressablesUtility.ReleaseAll();
    }
}
