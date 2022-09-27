using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterControllerBase : MonoBehaviour
{
    [System.Serializable]
    public class CharacterSettings
    {
        /// <summary> ˆÚ“®‘¬“x </summary>
        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private float _attackInterval = 0.5f;
        [SerializeField] private float _lifeTimer = 2.0f;

        public float MoveSpeed => _moveSpeed;
        public float AttackInterval => _attackInterval;
        public float LifeTimer => _lifeTimer;
    }

    [SerializeField] protected Rigidbody2D _rigidbody = null;

    [SerializeField] protected CharacterSettings _settings;

    protected BulletPool _bulletPool = null;
    protected bool _isPaused = false;
    protected CharacterAssets.CharacterParameter _parameter = null;
    protected bool _isPlayer = false;

    public void Initialize(CharacterAssets.CharacterParameter parameter, BulletPool pool, bool isPlayer)
    {
        _parameter = parameter;
        _bulletPool = pool;
        _isPlayer = isPlayer;
    }

    private void OnDestroy()
    {
        this.transform.DOKill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter(collision);
    }

    protected virtual void TriggerEnter(Collider2D collision)
    {
        if (_isPlayer)
        {

        }
        else
        {
            if (collision.tag == "Enemy" || collision.tag == "EnemyBullet")
            {
                return;
            }

            Destroy(gameObject);
        }
    }
}
