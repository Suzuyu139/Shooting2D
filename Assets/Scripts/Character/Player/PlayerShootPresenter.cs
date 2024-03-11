using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerShootPresenter : PresenterBase
{
    [SerializeField] PlayerModel _model;
    [SerializeField] PlayerMoveView _moveView;

    float _intervalTimer = 0.0f;

    private void Start()
    {
        Initialize().Forget();
    }

    async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => _model.IsSetupInitialized);

        this.UpdateAsObservable().Subscribe(OnUpdate).AddTo(gameObject);

        IsInitialized = true;
    }

    void OnUpdate(Unit unit)
    {
        if(!_model.Input.IsFire || _model.BulletId <= 0)
        {
            _intervalTimer = 0.0f;
            return;
        }

        if(_intervalTimer <= 0.0f)
        {
            Shoot();
        }

        _intervalTimer += Time.deltaTime;

        if(_model.ShootInterval <= _intervalTimer)
        {
            _intervalTimer = 0.0f;
        }
    }

    void Shoot()
    {
        var bulletObj = InGameManager.Instance.BulletPool.Rent(_model.BulletId, null, _moveView.GetWorldPos());
        bulletObj.GetComponent<BulletModel>().SetIsShoot(true);
    }
}
