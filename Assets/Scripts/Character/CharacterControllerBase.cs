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
    [SerializeField] private GameObject _characterFailureSmokeObject;
    [SerializeField] private GameObject _characterExplosionObject;
    [SerializeField] private int _hitTimeCount = 5;
    [SerializeField] private float _hitTimeInterval = 0.1f;
    [SerializeField] private float _hitInvincibleTime = 1.0f;
    [SerializeField] private float _deathHitStopTime = 1.0f;
    [SerializeField] private float _deathSlowTimeScale = 0.2f;
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
    private bool _isDeath = false;

    public bool IsDeath => _isDeath;

    private void Start()
    {
        OnInitialize();
    }

    protected virtual void OnInitialize()
    {
        var manager = InGameManager.Instance;
        var character = MasterManager.Instance.Character;
        if(_settings.IsPlayer)
        {
            _parameter = character.PlayerParameters.Find(x => x.CharacterId == _settings.CharacterId);
        }
        else
        {
            _parameter = character.EnemyParameters.Find(x => x.CharacterId == _settings.CharacterId);
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
        if(!_settings.IsPlayer)
        {
            InGameManager.Instance.RemoveEnemies(this);
        }
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
            if(collision.CompareTag(TagName.Player) || collision.CompareTag(TagName.PlayerBullet))
            {
                return;
            }

            _hp--;
            if(_hp > 0)
            {
                HitOperationProcess().Forget();
                if(_hp == 1)
                {
                    var obj = Instantiate(_characterFailureSmokeObject, this.transform.position, Quaternion.identity);
                    obj.transform.parent = this.transform;
                }
            }
            else
            {
                DeathHitStop().Forget();
            }
        }
        else
        {
            if (collision.CompareTag(TagName.Enemy) || collision.CompareTag(TagName.EnemyBullet))
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
                _isDeath = true;
                Destroy(gameObject);
            }
        }
    }

    private async UniTask DeathHitStop()
    {
        _isPaused = true;
        _isInvincible = true;
        _isDeath = true;
        InGameManager.Instance.TimeManager.SetTimeScale(0.0f, _deathHitStopTime);
        await UniTask.WaitWhile(() => InGameManager.Instance.TimeManager.IsStop);

        if (_characterExplosionObject)
        {
            _characterView.SetSpriteRenderersAlpha(0.0f);
            var particle = Instantiate(_characterExplosionObject, this.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            InGameManager.Instance.TimeManager.SetTimeScale(_deathSlowTimeScale, particle.main.duration / _deathSlowTimeScale);
        }

        await UniTask.WaitWhile(() => InGameManager.Instance.TimeManager.IsStop);
        Destroy(gameObject);
    }
}
