using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveView : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;

    public void Move(Vector2 move)
    {
        _characterController.Move(move);
    }
}
