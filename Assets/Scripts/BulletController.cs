using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody = null;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _Timer = 4.0f;

    private BulletPool _bulletPool = null; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot(BulletPool pool, Vector2 direction)
    {
        if(_bulletPool == null)
        {
            _bulletPool = pool;
        }
        _rigidbody.AddForce(direction * _speed);
        _bulletPool.Release(this.gameObject, _Timer);
    }
}
