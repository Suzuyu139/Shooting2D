using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas = null;
    [SerializeField] private TextMeshProUGUI _fpsTmp = null;

    public void Initialize()
    {
        _canvas.worldCamera = Camera.main;
        _canvas.sortingLayerName = this.transform.tag;
    }

    public void SetFps(float fps)
    {
        _fpsTmp.text = $"FPS : {fps}";
    }
}
