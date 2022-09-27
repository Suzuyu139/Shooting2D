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
        Destroy(gameObject, _settings.LifeTimer);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    private void Move()
    {
        this.transform.DOMove(_endPosition, _settings.LifeTimer).SetEase(Ease.Linear);
    }
}
