using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public struct InputData
    {
        public Vector2 Move { get; private set; }

        public void SetMove(Vector2 move) => Move = move;
    }

    public InputData Input { get; private set; } = new InputData();
    public void SetInput(InputData input) => Input = input;

    public Vector2 Move { get; private set; } = Vector2.zero;
    public void SetMove(Vector2 move) => Move = move;

    [SerializeField] private float _moveSpeed = 0.0f;
    public float MoveSpeed => _moveSpeed;
    public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;
}