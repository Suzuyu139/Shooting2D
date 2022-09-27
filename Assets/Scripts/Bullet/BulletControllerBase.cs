using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody = null;
    [SerializeField] private int _bulletId = 0;
    [SerializeField] protected float _speed = 1.0f;
    [SerializeField] protected float _AliveTimer = 4.0f;

    protected BulletPool _bulletPool = null;

    public int BulletId => _bulletId;
}
