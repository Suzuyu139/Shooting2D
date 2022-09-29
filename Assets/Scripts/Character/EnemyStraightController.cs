using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyStraightController : CharacterControllerBase
{
    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _endPosition = Vector3.zero;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        _startPosition = this.transform.position;
        _endPosition = this.transform.position;
        if (_startPosition.x < InGameManager.ScreenLimitPosX && _startPosition.x > -InGameManager.ScreenLimitPosX)
        {
            _endPosition.y *= -1.0f;
        }
        else if (_startPosition.y < InGameManager.ScreenLimitPosY && _startPosition.y > -InGameManager.ScreenLimitPosY)
        {
            _endPosition.x *= -1.0f;
        }
        else
        {
            _endPosition *= -1.0f;
        }
        Move();
        Destroy(gameObject, _settings.MoveTime);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        Attack();
    }

    private void Move()
    {
        this.transform.DOMove(_endPosition, _settings.MoveTime).SetEase(Ease.Linear);
    }

    private void Attack()
    {
        _attackCount += Time.deltaTime;
        if(_attackCount >= _settings.AttackInterval)
        {
            if (!_normalBullet1)
            {
                Debug.LogError($"íeÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÇ≈ÇµÇΩÅB : {nameof(_parameter.NormalBulletId1)}");
                return;
            }
            var bulletController = _bulletPool.GetBulletComponent<BulletStraightController>(_normalBullet1.gameObject, this.transform.position, this.transform.rotation);
            _direction = (InGameManager.Instance.Player.transform.position - this.transform.position).normalized;
            bulletController.Shot(_bulletPool, _direction);
            _attackCount = 0.0f;
        }
    }
}
