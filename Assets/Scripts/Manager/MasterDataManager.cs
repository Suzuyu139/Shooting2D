using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using MasterData;

public class MasterDataManager : MonoBehaviour
{
    public static MasterDataManager Instance { get; private set; }

    public Bullet Bullet { get; private set; } = null;

    public bool IsInitialized { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        IsInitialized = true;
    }

    public async UniTask DownloadSpreadSheetAsync()
    {
        await DownloadSheetAsync();
        await UniTask.CompletedTask;
    }

    async UniTask DownloadSheetAsync()
    {
        foreach (var sheetURL in SpreadSheetURL.SheetURL)
        {
            foreach (var sheet in sheetURL.Value)
            {
                UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + sheetURL.Key + "/gviz/tq?tqx=out:csv&sheet=" + sheet);
                await request.SendWebRequest();

                switch (request.result)
                {
                    case UnityWebRequest.Result.Success:
                        ViewCSV(request.downloadHandler.text);
                        break;

                    default:
                        Debug.LogError(request.error);
                        return;
                }
            }
        }

        await UniTask.CompletedTask;
    }

    void ViewCSV(string text)
    {
        StringReader reader = new StringReader(text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();        // àÍçsÇ∏Ç¬ì«Ç›çûÇ›
            string[] elements = line.Split(',');    // çsÇÃÉZÉãÇÕ,Ç≈ãÊêÿÇÁÇÍÇÈ
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == "\"\"")
                {
                    continue;                       // ãÛîíÇÕèúãé
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
                Debug.Log(elements[i]);
            }
        }
    }
}
