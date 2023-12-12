using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerMovePresenter : PresenterBase
{
    [SerializeField] PlayerModel _model;
    [SerializeField] PlayerMoveView _moveView;

    private void Start()
    {
        this.UpdateAsObservable().Subscribe(OnUpdate).AddTo(gameObject);

        IsInitialized = true;
    }

    void OnUpdate(Unit unit)
    {
        _model.SetMove(_model.Input.Move);
        _moveView.Move(_model.Move * _model.MoveSpeed * Time.deltaTime);
    }
}
