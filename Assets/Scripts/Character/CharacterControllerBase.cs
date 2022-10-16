using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;

public class CharacterControllerBase : MonoBehaviour
{
    private static readonly float _invincibleAlpha = 0.5f;

    [System.Serializable]
    public class CharacterSettings
    {
        [SerializeField] protected int _characterId = 0;
        [SerializeField] private bool _isPlayer = false;
        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private float _attackInterval = 0.5f;
        [SerializeField] private float _moveTime = 2.0f;

        public int CharacterId => _characterId;
        public bool IsPlayer => _isPlayer;
        public float MoveSpeed => _moveSpeed;
        public float AttackInterval => _attackInterval;
        public float MoveTime => _moveTime;
    }

    [Header("キャラクター設定（ベース）")]
    [SerializeField] protected Rigidbody2D _rigidbody = null;
    [SerializeField] private CharacterView _characterView = null;
    [SerializeField] private GameObject _characterExplosionObject;
    [SerializeField] private int _hitTimeCount = 5;
    [SerializeField] private float _hitTimeInterval = 0.1f;
    [SerializeField] private float _hitInvincibleTime = 1.0f;
    [SerializeField] protected CharacterSettings _settings;

    protected PoolManager _bulletPool = null;
    protected bool _isPaused = false;
    protected CharacterAssets.CharacterParameter _parameter = null;
    protected Vector2 _direction = Vector2.zero;
    protected float _attackCount = 0.0f;
    protected int _hp = 0;
    protected BulletControllerBase _normalBullet1 = null;
    protected BulletControllerBase _normalBullet2 = null;
    protected BulletControllerBase _normalBullet3 = null;
    protected BulletControllerBase _specialBullet1 = null;
    protected BulletControllerBase _specialBullet2 = null;
    protected BulletControllerBase _specialBullet3 = null;

    private bool _isInvincible = false;

    private void Start()
    {
        OnInitialize();
    }

    protected virtual void OnInitialize()
    {
        var manager = InGameManager.Instance;
        if(_settings.IsPlayer)
        {
            _parameter = manager.CharacterAssets.PlayerParameters.Find(x => x.CharacterId == _settings.CharacterId);
        }
        else
        {
            _parameter = manager.CharacterAssets.EnemyParameters.Find(x => x.CharacterId == _settings.CharacterId);
        }
        _bulletPool = manager.BulletPool;
        _hp = _parameter.Hp;
        _normalBullet1 = _parameter.BulletControllers.Find(x => x.BulletId == _parameter.NormalBulletId1);
    }

    private void Update()
    {
        if (_isPaused)
        {
            return;
        }

        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        
    }

    private void OnDestroy()
    {
        this.transform.DOKill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isInvincible)
        {
            return;
        }

        TriggerEnter(collision);
    }

    private async UniTask HitOperationProcess()
    {
        _isInvincible = true;
        for(int i = 0; i < _hitTimeCount; i++)
        {
            _characterView.SetSpriteRenderersAlpha(_invincibleAlpha);
            await UniTask.Delay(TimeSpan.FromSeconds(_hitTimeInterval));
            _characterView.SetSpriteRenderersAlpha(1.0f);
            await UniTask.Delay(TimeSpan.FromSeconds(_hitTimeInterval));
        }

        _characterView.SetSpriteRenderersAlpha(_invincibleAlpha);
        await UniTask.Delay(TimeSpan.FromSeconds(_hitInvincibleTime));
        _characterView.SetSpriteRenderersAlpha(1.0f);

        _isInvincible = false;
    }

    protected virtual void TriggerEnter(Collider2D collision)
    {
        if (_settings.IsPlayer)
        {
            if(collision.tag == TagName.Player || collision.tag == TagName.PlayerBullet)
            {
                return;
            }

            _hp--;
            if(_hp > 0)
            {
                HitOperationProcess().Forget();
            }
            else
            {
                if (_characterExplosionObject)
                {
                    Instantiate(_characterExplosionObject, this.transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.tag == TagName.Enemy || collision.tag == TagName.EnemyBullet)
            {
                return;
            }

            _hp--;
            if (_hp > 0)
            {
                HitOperationProcess().Forget();
            }
            else
            {
                if(_characterExplosionObject)
                {
                    Instantiate(_characterExplosionObject, this.transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}
