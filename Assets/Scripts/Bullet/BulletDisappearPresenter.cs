using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BulletDisappearPresenter : PresenterBase
{
    [SerializeField] BulletModel _model;

    float _disappearTimer = 0.0f;

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

        Disappear().Forget();
    }

    async UniTask Disappear()
    {
        await UniTask.WaitUntil(() =>
        {
            _disappearTimer += Time.deltaTime;
            if (_model.DisappearTime <= _disappearTimer)
            {
                _disappearTimer = 0.0f;
                return true;
            }
            return false;
        });

        _model.SetIsShoot(false);
        InGameManager.Instance.BulletPool.Return(_model.Id);
    }
}
