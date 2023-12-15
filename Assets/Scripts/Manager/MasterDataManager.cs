using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
                ViewCSV(request.downloadHandler.text);
                break;

            default:
                Debug.LogError(request.error);  
                return;
        }
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
