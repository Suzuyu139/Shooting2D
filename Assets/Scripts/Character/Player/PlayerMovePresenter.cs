using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

public class PlayerMovePresenter : PresenterBase
{
    [SerializeField] PlayerModel _model;
    [SerializeField] PlayerMoveView _moveView;

    Camera _mainCamera;

    private async void Start()
    {
        _mainCamera = Camera.main;

        await Initialize();

        IsInitialized = true;
    }

    async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => _model.IsSetupInitialized);

        this.UpdateAsObservable().Subscribe(OnUpdate).AddTo(gameObject);
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
