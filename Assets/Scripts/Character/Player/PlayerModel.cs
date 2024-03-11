using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerModel : MonoBehaviour
{
    public struct InputData
    {
        public Vector2 Move { get; private set; }
        public Vector2 CursorPos { get; private set; }
        public bool IsFire { get; private set; }

        public void SetMove(Vector2 move) => Move = move;
        public void SetCursorPos(Vector2 cursorPos) => CursorPos = cursorPos;
        public void SerIsFire(bool isFire) => IsFire = isFire;
    }

    public bool IsSetupInitialized { get; private set; } = false;
    public void SetIsSetupInitialized(bool isSetupInitialized) => IsSetupInitialized = isSetupInitialized;

    [SerializeField] private int _id = 0;
    public int Id => _id;
    public void SetId(int id) => _id = id;

    public InputData Input { get; private set; } = new InputData();
    public void SetInput(InputData input) => Input = input;

    public Vector2 Move { get; private set; } = Vector2.zero;
    public void SetMove(Vector2 move) => Move = move;

    [SerializeField] private float _moveSpeed = 0.0f;
    public float MoveSpeed => _moveSpeed;
    public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;

    [SerializeField] int _bulletId = 0;
    public int BulletId => _bulletId;
    public void SetBulletId(int bulletId) => _bulletId = bulletId;

    [SerializeField] float _shootInterval = 0.0f;
    public float ShootInterval => _shootInterval;
    public void SetShootInterval(float interval) => _shootInterval = interval;
}
