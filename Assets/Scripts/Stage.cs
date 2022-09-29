using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform = null;
    public Transform PlayerTransform => _playerTransform;
}
