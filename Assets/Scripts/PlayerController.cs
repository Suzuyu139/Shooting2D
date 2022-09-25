using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterControllerBase
{
    private static readonly float _posLimitX = 8.45f;
    private static readonly float _posLimitY = 4.45f;

    [SerializeField] private Transform _bulletTransform = null;

    private Vector2 _direction = Vector2.zero;
    private float _attackCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPaused)
        {
            return;
        }

        Move();

        MouseRotation();

        Attack();
    }

    /// <summary>
    /// プレイヤー移動処理
    /// </summary>
    private void Move()
    {
        var move = Vector3.zero;
        var pos = this.transform.position;

        // 移動処理
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        pos += move * _settings.MoveSpeed * Time.deltaTime;

        // 画面外に出ないようにする
        pos = new Vector3(Mathf.Clamp(pos.x, -_posLimitX, _posLimitX), Mathf.Clamp(pos.y, -_posLimitY, _posLimitY), pos.z);

        this.transform.position = pos;
    }

    /// <summary>
    /// プレイヤー回転処理
    /// </summary>
    private void MouseRotation()
    {
        // マウスの位置をもとに回転させる
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z -= Camera.main.transform.position.z;
        _direction = (mousePos - this.transform.position).normalized;

        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
    }

    /// <summary>
    /// 攻撃処理
    /// </summary>
    private void Attack()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (_attackCount <= 0.0f)
            {
                var bullet = _parameter.BulletControllers.Find(x => x.BulletId == _parameter.NormalBulletId1);
                if(!bullet)
                {
                    Debug.LogError($"弾が見つかりませんでした。 : {nameof(_parameter.NormalBulletId1)}");
                    return;
                }
                var bulletController = _bulletPool.GetBulletComponent<PlayerBulletStraightController>(bullet.gameObject, _bulletTransform.position, this.transform.rotation);
                bulletController.Shot(_bulletPool, _direction);
                _attackCount += Time.deltaTime;
            }
        }

        if(_attackCount > 0.0f)
        {
            _attackCount += Time.deltaTime;
            if (_attackCount >= _settings.AttackInterval)
            {
                _attackCount = 0.0f;
            }
        }
    }
}
