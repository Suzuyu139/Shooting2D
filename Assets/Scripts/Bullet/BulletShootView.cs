using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootView : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;

    public void Shoot(Vector2 force)
    {
        _rb.AddForce(force);
    }
}
