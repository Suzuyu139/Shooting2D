using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletAsset : ScriptableObject
{
    [SerializeField] private int _bulletId = 0;
    [SerializeField] private float _aliveTimer = 0.0f;
    [SerializeField] private float _speed = 0.0f;

    public int BulletId => _bulletId;
    public float AliveTimer => _aliveTimer;
    public float Speed => _speed;
}
