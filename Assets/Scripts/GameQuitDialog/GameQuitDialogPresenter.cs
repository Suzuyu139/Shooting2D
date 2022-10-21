using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class GameQuitDialogPresenter : MonoBehaviour
{
    [SerializeField] private GameQuitDialogView _view = null;

    private void Start()
    {
        _view.Initialize();

        BindViewEvents();
    }

    private void BindViewEvents()
    {
        _view.OnClickYesButton
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                Debug.Log("Yes");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;    //ゲームプレイ終了
#else
                Application.Quit(); //ゲームプレイ終了
#endif
            });

        _view.OnClickNoButton
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                Debug.Log("No");
                _view.Close();
            });
    }

    public void Open()
    {
        _view.Open();
    }

    public void Close()
    {
        _view.Close();
    }
}
