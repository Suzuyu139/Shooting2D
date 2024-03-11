using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BulletShootPresenter : PresenterBase
{
    [SerializeField] BulletModel _model;
    [SerializeField] BulletShootView _shootView;

    private void Start()
    {
        Initialize().Forget();
    }

    async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => _model.IsSetupInitialized);

        _model.IsShoot.Subscribe(OnShoot).AddTo(gameObject);

        IsInitialized = true;
    }

    void OnShoot(bool isShoot)
    {
        if(!isShoot)
        {
            return;
        }

        _shootView.Shoot(_model.ForceDirection * _model.ForceSpeed);
    }
}
