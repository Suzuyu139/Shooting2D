using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MasterDataManager : MonoBehaviour
{
    public static MasterDataManager Instance { get; private set; }

    public bool IsInitialized { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        IsInitialized = true;
    }

    public async UniTask DownloadSheetAsync()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SpreadSheetURL.Bullet + "/gviz/tq?tqx=out:csv&sheet=" + "Bullet");
        await request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.Success:
                Debug.Log(request.downloadHandler.text);
                break;

            default:
                Debug.LogError(request.error);
                return;
        }
    }
}
