using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody = null;

    private BulletPool _bulletPool = null; 
    private float _speed = 1.0f;
    private float _Timer = 4.0f;

    public void Shot(BulletAsset bulletAsset, BulletPool pool, Vector2 direction)
    {
        if(_bulletPool == null)
        {
            _bulletPool = pool;
        }

        _Timer = bulletAsset.AliveTimer;
        _speed = bulletAsset.Speed;

        _rigidbody.AddForce(direction * _speed);
        _bulletPool.Release(this.gameObject, _Timer);
    }
}
