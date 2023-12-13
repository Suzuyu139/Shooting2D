using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TitlePresenter : PresenterBase
{
    [SerializeField] TitleModel _model;
    [SerializeField] TitleView _view;

    private void Start()
    {
        _view.StartButtonObservable.Subscribe(OnStartButton).AddTo(gameObject);

        IsInitialized = true;
    }

    void OnStartButton(Unit unit)
    {
        if (GameSceneManager.Instance.IsSceneChange)
        {
            return;
        }

        GameSceneManager.Instance.LoadSceneChange("Game");
    }
}
