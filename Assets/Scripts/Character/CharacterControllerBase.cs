using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterControllerBase : MonoBehaviour
{
    [System.Serializable]
    public class CharacterSettings
    {
        [SerializeField] private bool _isPlayer = false;
        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private float _attackInterval = 0.5f;
        [SerializeField] private float _lifeTimer = 2.0f;

        public bool IsPlayer => _isPlayer;
        public float MoveSpeed => _moveSpeed;
        public float AttackInterval => _attackInterval;
        public float LifeTimer => _lifeTimer;
    }

    [SerializeField] protected Rigidbody2D _rigidbody = null;
    [SerializeField] protected int _characterId = 0;
    [SerializeField] protected CharacterSettings _settings;

    protected BulletPool _bulletPool = null;
    protected bool _isPaused = false;
    protected CharacterAssets.CharacterParameter _parameter = null;

    private void Start()
    {
        OnInitialize();
    }

    protected virtual void OnInitialize()
    {
        var manager = InGameManager.Instance;
        if(_settings.IsPlayer)
        {
            _parameter = manager.CharacterAssets.PlayerParameters.Find(x => x.CharacterId == _characterId);
        }
        else
        {
            _parameter = manager.CharacterAssets.EnemyParameters.Find(x => x.CharacterId == _characterId);
        }
        _bulletPool = manager.BulletPool;
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        if (_isPaused)
        {
            return;
        }
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
        if (_settings.IsPlayer)
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
