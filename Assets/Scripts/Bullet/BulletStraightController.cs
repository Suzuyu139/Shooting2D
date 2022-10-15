using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStraightController : BulletControllerBase
{
    public void Shot(PoolManager pool, Vector2 direction)
    {
        if(_bulletPool == null)
        {
            _bulletPool = pool;
        }

        _rigidbody.AddForce(direction * _speed);
        _bulletPool.ReleaseTimer(this.BulletId, this.gameObject, _AliveTimer);
    }
}
