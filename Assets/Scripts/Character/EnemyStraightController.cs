using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyStraightController : CharacterControllerBase
{
    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _endPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = this.transform.position;
        _endPosition = this.transform.position;
        if(_startPosition.x < InGameManager.ScreenLimitPosX && _startPosition.x > -InGameManager.ScreenLimitPosX)
        {
            _endPosition.y *= -1.0f;
        }
        else if(_startPosition.y < InGameManager.ScreenLimitPosY && _startPosition.y > -InGameManager.ScreenLimitPosY)
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

    // Update is called once per frame
    void Update()
    {
        if(_isPaused)
        {
            return;
        }

    }

    private void Move()
    {
        this.transform.DOMove(_endPosition, _settings.LifeTimer).SetEase(Ease.Linear);
    }
}
