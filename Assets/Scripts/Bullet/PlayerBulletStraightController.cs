using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletStraightController : BulletControllerBase
{
    public void Shot(BulletPool pool, Vector2 direction)
    {
        if(_bulletPool == null)
        {
            _bulletPool = pool;
        }

        _rigidbody.AddForce(direction * _speed);
        _bulletPool.Release(this.gameObject, _AliveTimer);
    }
}
