using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

using DG.Tweening;

public class ButtonController : Button
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        this.transform.DOScale(0.9f, 0.2f);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        this.transform.DOScale(1.0f, 0.2f);
    }
}

[CustomEditor(typeof(ButtonController))]
public sealed class CustomButtonScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //DrawHeader();
    }
}
