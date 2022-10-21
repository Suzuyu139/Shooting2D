using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class MasterText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private string _textKey;

    // Start is called before the first frame update
    void Start()
    {
        Initialize().Forget();
    }

    private async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => MasterManager.Instance != null);

        _tmp.text = MasterManager.Instance.Text.GetString(_textKey);
    }
}
