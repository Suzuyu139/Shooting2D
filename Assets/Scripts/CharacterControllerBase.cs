using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Initialize(CharacterAssets.CharacterParameter parameter, BulletPool pool)
    {
        _parameter = parameter;
        _bulletPool = pool;
    }
}
