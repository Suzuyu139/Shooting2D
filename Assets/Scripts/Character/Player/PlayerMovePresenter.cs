using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerMovePresenter : PresenterBase
{
    [SerializeField] PlayerModel _model;
    [SerializeField] PlayerMoveView _moveView;

    Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;

        this.UpdateAsObservable().Subscribe(OnUpdate).AddTo(gameObject);

        IsInitialized = true;
    }

    void OnUpdate(Unit unit)
    {
        _model.SetMove(_model.Input.Move);
        _moveView.Move(_model.Move * _model.MoveSpeed * Time.deltaTime);

        var pos = _mainCamera.WorldToScreenPoint(_moveView.GetLocalPos());
        var rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(_model.Input.CursorPos.x, _model.Input.CursorPos.y, 0.0f) - pos);
        _moveView.Rotation(rotation);
    }
}
