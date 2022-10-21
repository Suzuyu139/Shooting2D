using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogViewBase : MonoBehaviour
{
    [SerializeField] private Canvas _canvas = null;

    public virtual void Initialize()
    {
        _canvas.worldCamera = Camera.main;
        _canvas.sortingLayerName = this.transform.tag;
    }

    public virtual void Open()
    {
        _canvas.gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        _canvas.gameObject.SetActive(false);
    }
}
