using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveView : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] Transform _playerTransform;

    public void Move(Vector2 move)
    {
        _characterController.Move(move);
    }

    public void Rotation(Quaternion rot)
    {
        _playerTransform.localRotation = rot;
    }

    public Vector3 GetLocalPos()
    {
        return _playerTransform.localPosition;
    }
}
